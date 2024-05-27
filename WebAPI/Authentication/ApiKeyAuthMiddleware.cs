using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyAuthMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Query[AuthConstants.ApiKeyHeaderName].IsNullOrEmpty())
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key is missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            var extractedKey = context.Request.Query[AuthConstants.ApiKeyHeaderName];

            if (!apiKey.Equals(extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key");
                return;
            }

            await _next(context);

        }
    }
}
