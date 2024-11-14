using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LayoutSpecificationApi;

public class SpecificationAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly ISpecificationAccessManager _accessManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly SpecificationAccessType _requiredAccess;

    public SpecificationAuthorizationFilter(ISpecificationAccessManager accessManager, ICurrentUserService currentUserService, SpecificationAccessType requiredAccess)
    {
        _accessManager = accessManager;
        _currentUserService = currentUserService;
        _requiredAccess = requiredAccess;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var specificationId = Guid.Parse(context.RouteData.Values["specificationId"].ToString());
        var userId = _currentUserService.UserId();

        var result = await _accessManager.HasAccessAsync(
            specificationId,
            userId,
            _requiredAccess);

        if (result.IsFailure || !result.Value)
        {
            context.Result = new ForbidResult();
        }
    }
}