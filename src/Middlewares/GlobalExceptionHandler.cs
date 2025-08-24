namespace CodeBattles_Backend.Middlewares;

public class GlobalExceptionHandler : IMiddleware
{
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      context.Response.StatusCode = 500;
      await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
    }
  }
}