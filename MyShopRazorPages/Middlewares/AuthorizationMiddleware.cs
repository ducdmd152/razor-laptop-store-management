using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using MyShopManagementBO;
using MyShopRazorPages.Data;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace MyShopRazorPages.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userString = context.Session.GetString("CREDENTIAL");
            var user = userString != null ? JsonConvert.DeserializeObject<User>(userString) : null;
            bool isAuthenticated = user != null;
            if (isAuthenticated)
            {
                
                if (AllowForRole(context, user.RoleId))
                {
                    await _next(context);
                    return;
                }
                else
                {
                    context.Response.Redirect("/");
                    return;
                }
            }
            else if (!AllowAnonymous(context))
			{

				context.Response.Redirect("/Login");
				return;
			}

			await _next(context);
		}

        private bool AllowAnonymous(HttpContext context)
        {
            string path = context.Request.Path.ToString().ToLower();
            return path == "" || path == "/"
                || path.StartsWith("/home")
                || path.StartsWith("/shop")
                || path.StartsWith("/login");
        }

        private bool AllowForRole(HttpContext context, int roleId)
        {
            string path = context.Request.Path.ToString().ToLower();
            switch (roleId)
            {
                case (int)Roles.ADMIN:
                    return path == "" || path == "/"
                        || path.StartsWith("/accounts")
                        || path.StartsWith("/profile")
                        || path.StartsWith("/logout");
                case (int)Roles.MANAGER:
                    return path == "" || path == "/"
                        || path.StartsWith("/products")
                        || path.StartsWith("/profile")
                        || path.StartsWith("/logout");
                case (int)Roles.CUSTOMER:
                    return path == "" || path == "/"
                        || path.StartsWith("/shop")
                        || path.StartsWith("/profile")
                        || path.StartsWith("/logout");
            }

            return false;
        }
    }

    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
