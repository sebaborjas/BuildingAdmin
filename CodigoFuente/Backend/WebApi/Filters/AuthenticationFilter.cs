﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IServices;
using System.Data;

namespace WebApi.Filters
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {

        public string Role { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"];

            if (String.IsNullOrEmpty(token))
            {
                context.Result = new JsonResult("Empty authorization header") { StatusCode = 401 };
            }
            else if (!Guid.TryParse(token, out Guid parsedToken))
            {
                context.Result = new JsonResult("Invalid token format") { StatusCode = 400 };
            }
            else
            {
                try
                {
                    var currentUser = GetSessionService(context).GetCurrentUser(parsedToken);

                    if (currentUser == null)
                    {
                        context.Result = new JsonResult("Sign in") { StatusCode = 401 };
                    }
                    else
                    {
                        var roles = Role.Split(',').Select(r => r.Trim()).ToList();

                        if (!roles.Contains(currentUser.GetType().Name))
                        {
                            context.Result = new JsonResult("You do not have enough permissions") { StatusCode = 401 };
                        }
                    }

                }
                catch (Exception ex)
                {
                    context.Result = new JsonResult("Internal server error") { StatusCode = 500 };
                }
            }
        }

        private ISessionService GetSessionService(AuthorizationFilterContext context)
        {
            var sessionServiceInjected = context.HttpContext.RequestServices.GetService(typeof(ISessionService));
            var sessionService = sessionServiceInjected as ISessionService;
            return sessionService;
        }
    }
}
