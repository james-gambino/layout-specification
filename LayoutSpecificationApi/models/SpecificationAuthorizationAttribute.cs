using Microsoft.AspNetCore.Mvc;

namespace LayoutSpecificationApi;

// Атрибут авторизации
public class SpecificationAuthorizationAttribute : TypeFilterAttribute
{
    public SpecificationAuthorizationAttribute(SpecificationAccessType requiredAccess) : base(typeof(SpecificationAuthorizationFilter))
    {
        Arguments = new object[] { requiredAccess };
    }
}