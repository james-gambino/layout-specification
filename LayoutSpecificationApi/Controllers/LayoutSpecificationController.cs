using Microsoft.AspNetCore.Mvc;

namespace LayoutSpecificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LayoutSpecificationController : ControllerBase
{
    [HttpGet("{specificationId}")]
    [SpecificationAuthorization(SpecificationAccessType.Read)]
    public async Task<IActionResult> Get(Guid specificationId)
    {
        return Ok();
    }

    [HttpPut("{specificationId}")]
    [SpecificationAuthorization(SpecificationAccessType.Update)]
    public async Task<IActionResult> Update(Guid specificationId)
    {
        return Ok();
    }

    [HttpDelete("{specificationId}")]
    [SpecificationAuthorization(SpecificationAccessType.Delete)]
    public async Task<IActionResult> Delete(Guid specificationId)
    {
        return Ok();
    }
}