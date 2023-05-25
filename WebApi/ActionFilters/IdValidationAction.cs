using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Controllers;

namespace WebApi.ActionFilters;

public class IdValidationAction : IActionFilter
{
    private readonly ILogger<StringsController> _logger;

    public IdValidationAction(ILogger<StringsController> logger)
    {
        _logger = logger;
    }

    void IActionFilter.OnActionExecuted(ActionExecutedContext context)
    {
    }

    void IActionFilter.OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue("id", out var id) ||
            id is not int intId ||
            intId == 0)
        {
            context.Result = new BadRequestObjectResult("Wrong parameter: Id");
        }
    }
}
