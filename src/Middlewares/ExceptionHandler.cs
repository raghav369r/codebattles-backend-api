using Microsoft.AspNetCore.Diagnostics;

namespace CodeBattles_Backend.Middlewares;

public class ExceptionHandler : IExceptionHandler
{
  public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}