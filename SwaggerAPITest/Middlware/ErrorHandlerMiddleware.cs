﻿using SwaggerAPITest.Extensions;
using System.Net;
using System.Text.Json;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ATM.Api.Helpers;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            await context.Response
                .WithStatusCode(Status422UnprocessableEntity)
                .WithJsonContent(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await context.Response
                .WithStatusCode(Status401Unauthorized)
                .WithJsonContent(ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await context.Response
                .WithStatusCode(Status416RangeNotSatisfiable)
                .WithJsonContent(ex.Message);
        }
        catch (Exception ex)
        {
            await context.Response
                .WithStatusCode(Status400BadRequest)
                .WithJsonContent(ex.Message);
        }
    }
}