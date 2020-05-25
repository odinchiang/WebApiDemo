using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiDemo.Filters
{
    public class CtmActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("OnActionExecuting");
        }
    }
}
