namespace LayoutSpecificationApi;

public class SpecificationAccessManager : ISpecificationAccessManager
{
    private readonly ISpecificationRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<SpecificationAccessManager> _logger;

    public SpecificationAccessManager(
        ISpecificationRepository repository,
        ICurrentUserService currentUserService,
        ILogger<SpecificationAccessManager> logger)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result> GrantAccessAsync(
        Guid layoutSpecificationId,
        Guid userId,
        SpecificationAccessType accessType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var currentUserId = _currentUserService.UserId();
            var specification = await _repository.GetByIdAsync(layoutSpecificationId, cancellationToken);

            if (specification == null)
                return Result.Failure("Specification not found");

            if (specification.Owner.UserId != currentUserId)
                return Result.Failure("Only owner can grant access");

            var access = SpecificationAccess.Grant(layoutSpecificationId, userId, accessType, currentUserId);

            await _repository.AddAccessAsync(access, cancellationToken);
            return Result.Success("Access granted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error granting access to specification {SpecificationId}", layoutSpecificationId);
            return Result.Failure("Error granting access");
        }
    }

    public async Task<Result> RevokeAccessAsync(
        Guid specificationId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var currentUserId = _currentUserService.UserId();
            var specification = await _repository.GetByIdAsync(specificationId, cancellationToken);

            if (specification == null)
                return Result.Failure("Specification not found");

            if (specification.Owner.UserId != currentUserId)
                return Result.Failure("Only owner can revoke access");

            var access = await _repository.GetAccessAsync(specificationId, userId, cancellationToken);
            if (access == null)
                return Result.Failure("Access not found");

            access.Revoke();
            await _repository.UpdateAccessAsync(access, cancellationToken);
            return Result.Success("");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error revoking access from specification {SpecificationId}", specificationId);
            return Result.Failure("Error revoking access");
        }
    }

    public async Task<Result> HasAccessAsync(Guid specificationId, Guid userId, SpecificationAccessType requiredAccess, CancellationToken cancellationToken = default)
    {
        try
        {
            var specification = await _repository.GetByIdAsync(specificationId, cancellationToken);
            if (specification == null)
                return Result.Failure("Specification not found");

            // Владелец имеет полный доступ
            if (specification.Owner.UserId == userId)
                return Result.Success("");

            var access = await _repository.GetAccessAsync(specificationId, userId, cancellationToken);
            if (access == null || access.RevokedAt.HasValue)
                return Result.Success("");

            return Result.Success(access.AccessType >= requiredAccess);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking access for specification {SpecificationId}", specificationId);
            return Result.Failure("Error checking access");
        }
    }
}


// Сервис управления доступом
public interface ISpecificationAccessManager
{
    Task<Result> GrantAccessAsync(
        Guid layoutSpecificationId,
        Guid userId,
        SpecificationAccessType accessType,
        CancellationToken cancellationToken = default);

    Task<Result> RevokeAccessAsync(
        Guid layoutSpecificationId,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<Result> HasAccessAsync(
        Guid layoutSpecificationId,
        Guid userId,
        SpecificationAccessType requiredAccess,
        CancellationToken cancellationToken = default);
}