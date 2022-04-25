using Microsoft.AspNetCore.Mvc;
using Enterprise.Dotnet.API.Errors;

namespace Enterprise.Server.DotnetApi.Controllers;

[Route("errors/{errorCode}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{
  public IActionResult Error(int errorCode)
  {
    return new ObjectResult(new ApiResponse(errorCode));
  }
}
