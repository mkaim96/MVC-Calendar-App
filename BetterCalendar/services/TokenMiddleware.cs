using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BetterCalendar.services
{
    public class TokenMiddleware : IMiddleware
    {
        private TokenManager _tokenManager;

        public TokenMiddleware(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_tokenManager.IsTokenActive())
            {
                await next(context);

                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
