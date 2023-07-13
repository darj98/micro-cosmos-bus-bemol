namespace DesafioBemol.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!IsValidToken(context.HttpContext.Request.Headers["Authorization"]))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool IsValidToken(string token)
        {
            string authenticationKey = "minhachave123";
            return token == authenticationKey;
        }
    }
}
