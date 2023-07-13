namespace DesafioBemol.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Net;

    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new { error = "Ocorreu um erro ao processar a requisição." })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
