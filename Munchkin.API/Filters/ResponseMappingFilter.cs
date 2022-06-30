using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Munchkin.API.Extensions;
using Munchkin.Domain;

namespace Munchkin.API.Filters
{
    public class ResponseMappingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult
                && objectResult.Value is CqrsResponse cqrsResponse
                && !cqrsResponse.StatusCode.IsSuccessful())
            {
                context.Result = new ObjectResult(new { cqrsResponse.ErrorMessage })
                {
                    StatusCode = (int)cqrsResponse.StatusCode
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
