using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StringsController : ControllerBase
{
    private const string BAD_REQUEST_MASK = "Where ara no string with such id={0}";

    private readonly ILogger<StringsController> _logger;
    private readonly IStringStorage _stringStorage;

    public StringsController(ILogger<StringsController> logger,
                             IStringStorage stringStorage)
    {
        _logger = logger;
        _stringStorage = stringStorage;
    }

    [HttpGet(Name = "GetById")]
    [ServiceFilter(typeof(IdValidationAction))]
    public IActionResult GetById([FromHeader]int id)
    {
        var str = _stringStorage.GetString(id);
        if (str == null)
        {
            return BadRequest(string.Format(BAD_REQUEST_MASK, id));
        }

        return Ok(str);
    }

    [HttpPost(Name = "Add")]
    [ServiceFilter(typeof (SumOfNumberAction))]
    public IActionResult Add(string value)
    {
        var newStringId = _stringStorage.AddString(value);

        return Ok(newStringId);
    }

    [HttpPut(Name = "Update")]
    [ServiceFilter(typeof(IdValidationAction))]
    [ServiceFilter(typeof(SumOfNumberAction))]
    public IActionResult Update([FromHeader] int id, string value)
    {
        if (!_stringStorage.UpdateString(id, value))
        {
            return BadRequest(string.Format(BAD_REQUEST_MASK, id));
        }

        return Ok(id);
    }

    [HttpDelete(Name = "Remove")]
    [ServiceFilter(typeof(IdValidationAction))]
    [ServiceFilter(typeof(SumOfNumberAction))]
    public IActionResult Delete([FromHeader] int id)
    {
        if (!_stringStorage.DeleteString(id))
        {
            return BadRequest(string.Format(BAD_REQUEST_MASK, id));
        }

        return Ok(id);
    }
}
