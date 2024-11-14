namespace LayoutSpecificationApi;

public interface ISpecificationRepository
{
    Task<Result> GrantAccessAsync(Guid layeroutSpecificationId, CancellationToken cancellationToken = default);
    Task<LayoutSpecification> GetByIdAsync(Guid layeroutSpecificationId, CancellationToken cancellationToken = default);
    Task AddAccessAsync(SpecificationAccess access, CancellationToken cancellationToken);
    Task<SpecificationAccess> GetAccessAsync(Guid specificationId, Guid userId, CancellationToken cancellationToken);
    Task UpdateAccessAsync(SpecificationAccess access, CancellationToken cancellationToken);
}

public class SpecificationRepository : ISpecificationRepository {
    public Task<Result> GrantAccessAsync(Guid layeroutSpecificationId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<LayoutSpecification> GetByIdAsync(Guid layeroutSpecificationId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new LayoutSpecification());
    }

    public Task AddAccessAsync(SpecificationAccess access, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SpecificationAccess> GetAccessAsync(Guid specificationId, Guid userId, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SpecificationAccess() { AccessType =  SpecificationAccessType.Read });
    }

    public Task UpdateAccessAsync(SpecificationAccess access, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}