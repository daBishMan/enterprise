using Enterprise.Dotnet.API.Errors;
using Enterprise.Dotnet.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;


namespace Enterprise.Server.DotnetApi.Controllers;
public class BuggyController : BaseApiController
{
  private readonly StoreContext context;

  public BuggyController(StoreContext context)
  {
    this.context = context;
  }


  [HttpGet("notfound")]
  public ActionResult GetNotFoundRequest()
  {
    var thing = this.context.Products.Find(42);
    if (thing == null)
    {
      return NotFound(new ApiResponse(404));
    }
    return Ok(thing);
  }

  [HttpGet("servererror")]
  public ActionResult GetServerError()
  {
    var thing = this.context.Products.Find(42);
    var thingsToReturn = thing.ToString();

    return Ok(thing);
  }

  [HttpGet("badrequest")]
  public ActionResult GetBadRequest()
  {
    return BadRequest();
  }

  [HttpGet("badrequest/{id}")]
  public ActionResult GetBadRequest(int id)
  {
    return Ok();
  }
}
