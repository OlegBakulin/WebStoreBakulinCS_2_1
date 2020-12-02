using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreCoreApplication.Controllers.Infrastructure
{
    public class TokenMiddleware
    {
        //private const string correctToken = "qwerty123";

        private readonly RequestDelegate Next;

        public TokenMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await Next(context);
            /*
            var token = context.Request.Query["referenceToken"];

            if (string.IsNullOrEmpty(token))
            {
                await Next.Invoke(context);
                return;
            }
            if (token == correctToken)
            {
                await Next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("Token is incorrect");
            }
            return;
            */
        }
    }
}
