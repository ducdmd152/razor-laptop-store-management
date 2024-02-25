using Microsoft.AspNetCore.Http.Extensions;

namespace MyShopRazorPages.Middlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
		private readonly List<string> _unAuthEndpoints;

		public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
			_unAuthEndpoints = new List<string>
		    {
                "/index",
			    "/home",
			    "/login",
			    "/shop"
            };
		}

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.GetEncodedUrl().ToLower();
            Console.WriteLine(path);
			bool isUnauthenticatedEndpoint = path == "/" || _unAuthEndpoints.Any(endpoint => path.StartsWith(endpoint.ToLower()));
			if (isUnauthenticatedEndpoint == false)
            {
                var userString = httpContext.Session.GetString("CREDENTIAL");
                if (userString == null)
                {
                    httpContext.Response.Redirect("/Login");
					return;
                }
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
