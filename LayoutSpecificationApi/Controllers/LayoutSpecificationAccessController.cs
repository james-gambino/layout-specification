using Microsoft.AspNetCore.Mvc;

namespace LayoutSpecificationApi.Controllers;

// Контроллер
[ApiController]
[Route("api/[controller]")]
public class LayoutSpecificationAccessController : ControllerBase
{
    private readonly ISpecificationAccessManager _accessManager;
    private readonly ILogger<LayoutSpecificationAccessController> _logger;

    public LayoutSpecificationAccessController(ISpecificationAccessManager accessManager, ILogger<LayoutSpecificationAccessController> logger)
    {
        _accessManager = accessManager;
        _logger = logger;
    }

    [HttpPost("{specificationId}/access")]
    public async Task<IActionResult> GrantAccess(Guid specificationId, [FromBody] GrantAccessRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _accessManager.GrantAccessAsync(
            specificationId,
            request.UserId,
            request.AccessType,
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpDelete("{specificationId}/access/{userId}")]
    public async Task<IActionResult> RevokeAccess(Guid specificationId, Guid userId, CancellationToken cancellationToken)
    {
        var result = await _accessManager.RevokeAccessAsync(
            specificationId,
            userId,
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}