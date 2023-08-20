using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthSystem.Exceptions
{
    public class AllowOnlyPublisherRolesAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.IsInRole("Publisher"))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
