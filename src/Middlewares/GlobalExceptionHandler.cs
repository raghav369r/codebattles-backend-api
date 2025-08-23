
using Microsoft.AspNetCore.Mvc;

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
      Console.WriteLine("Catched an Exception!!\n\n");
      context.Response.StatusCode = 500;
      await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
    }
  }
}