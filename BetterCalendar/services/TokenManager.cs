using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.services
{
    public class TokenManager
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IMemoryCache _cache;


        public TokenManager(IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public bool IsTokenActive()
        {
            var entry = _cache.Get<string>(GetCurrentToken());

            // if there is entry in cache that means that token is cancelled
            // otherwise token is not cancelled
            return entry == null ? true : false;
        }

        public void CancelToken()
        {
            // if token was added to cache it means that token is cancelled

            var token = GetCurrentToken();

            if (!String.IsNullOrEmpty(token))
            {
                _cache.Set<string>(token, " ", absoluteExpirationRelativeToNow: TimeSpan.FromDays(1));
            }
        }



        // Gets token string from HttpContext authorization header
        private string GetCurrentToken()
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

            if (authHeader == StringValues.Empty)
            {
                return string.Empty;
            }
            else
            {
                return authHeader.Single().Split(" ").Last();
            }
        }
    }
}
