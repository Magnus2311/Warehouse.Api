using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Warehouse.Api.Services.Connections;
using Warehouse.Database.Helpers;

namespace Warehouse.Api.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SSOAttribute : Attribute, IAuthorizationFilter
    {
        private AuthorizationFilterContext _context;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                _context = context;
                var ssoConnectionService = _context.HttpContext.RequestServices.GetService<SsoConnectionService>();
                var appSettings = _context.HttpContext.RequestServices.GetService<AppSettings>();
                var httpType = _context.HttpContext.Request.Method.ToUpperInvariant();
                var accessToken = string.Empty;

                if (httpType == "POST" || httpType == "PUT" || httpType == "PATCH")
                {
                    using (StreamReader reader
                      = new StreamReader(_context.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        var bodyStr = reader.ReadToEndAsync().GetAwaiter().GetResult();
                        accessToken = JsonSerializer.Deserialize<JsonElement>(bodyStr).GetProperty("accessToken").GetString();
                        _context.HttpContext.Request.Body.Position = 0;
                    }
                }

                if (httpType == "GET")
                {
                    accessToken = _context.HttpContext.Request.Query["accessToken"];
                }

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var userId = ssoConnectionService.ValidateToken(accessToken).GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(userId))
                    {
                        appSettings.UserId = new ObjectId(userId);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                _context.HttpContext.Response.StatusCode = new UnauthorizedResult().StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}