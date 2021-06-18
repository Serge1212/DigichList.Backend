using DigichList.Backend.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace DigichList.Backend.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(HttpResponseException)
            {
                throw;
            }
            catch(Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json; charset=utf-8";

                var description = new DescriptionResponseApiModel(error.Message);
                if (error is FormatException) description.Message = "Invalid data format";
                else if (error is ArgumentNullException) description.Message = "Some of the fields cannot be empty";
                var details = new ErrorDetails
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    RequestPath = context.Request.Path.Value,
                    EndpointPath = context.GetEndpoint()?.ToString(),
                    TimeStamp = DateTime.Now,
                    Message = description.Message
                };

                switch (error)
                {
                    case DirectoryNotFoundException _:
                    case FileNotFoundException _:
                    case InvalidOperationException _:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case KeyNotFoundException _:
                    case NotFoundException _:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentNullException _:
                    case ArgumentOutOfRangeException _:
                    case BadImageFormatException _:
                    case ArgumentException _:
                    case FormatException _:
                    case BadRequestException _:
                    case SecurityTokenException _:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var jsonOptions = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };
                var result = response.StatusCode == 500
                    ? JsonConvert.SerializeObject(details, jsonOptions)
                    : JsonConvert.SerializeObject(description, jsonOptions);
                await response.WriteAsync(result);
            }
        }
    }
}
