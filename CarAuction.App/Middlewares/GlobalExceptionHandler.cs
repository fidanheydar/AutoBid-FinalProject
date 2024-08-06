using CarAuction.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace CarAuction.App.Middlewares
{
    public static class GlobalExceptionHandler
    {
        public static void CustomExceptionHadler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    string message = "Internal error";

                    int statuscode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var exception = contextFeature.Error;

                    if (exception is ItemNotFoundException)
                    {
                        statuscode = 404;
                        message = exception.Message;
                    }
                    else if (exception is ItemAlreadyExistsException)
                    {
                        statuscode = 400;
                        message = exception.Message;
                    }
                    context.Response.StatusCode = statuscode;
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(

                            new { statuscode = statuscode, Message = contextFeature.Error.Message }
                        ));
                    }
                });
            });
        }
    }
}