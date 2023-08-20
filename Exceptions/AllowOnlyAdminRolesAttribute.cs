using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthSystem.Exceptions
{
    public class AllowOnlyAdminRolesAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.IsInRole("Admin"))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
