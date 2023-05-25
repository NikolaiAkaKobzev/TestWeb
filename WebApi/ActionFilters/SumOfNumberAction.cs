using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Controllers;
using WebApi.Services;

namespace WebApi.ActionFilters;

public class SumOfNumberAction : IActionFilter
{
    private INumbersInStringService _numbersInStringService;
    private readonly ILogger<StringsController> _logger;

    public SumOfNumberAction(INumbersInStringService numbersInStringService,
                             ILogger<StringsController> logger)
    {
        _numbersInStringService = numbersInStringService;
        _logger = logger;
    }

    void IActionFilter.OnActionExecuted(ActionExecutedContext context)
    {
    }

    void IActionFilter.OnActionExecuting(ActionExecutingContext context)
    {
        var value = context.ActionArguments.FirstOrDefault(arg => arg.Value is string);
        if (value.Value is not string stringValue)
        {
            _logger.LogWarning("Something went wrong");
            return;
        }

        var sum = _numbersInStringService.GetNumbersSum(stringValue);
        if (sum > double.Epsilon)
        {
            _logger.LogInformation($"The sum of all digits in string is {sum}");
            return;
        }

        _logger.LogInformation("The string doesn't contain digits");
    }
}
