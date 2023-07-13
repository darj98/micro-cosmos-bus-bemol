namespace DesafioBemol.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Diagnostics;

    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["Stopwatch"] = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var stopwatch = (Stopwatch)context.HttpContext.Items["Stopwatch"];
            stopwatch.Stop();
            var executionTime = stopwatch.Elapsed;

            Console.WriteLine($"Duração da execução: {executionTime}");
        }
    }
}
