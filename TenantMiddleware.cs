using CursoInfoeste.Models;
using System.Diagnostics;

namespace CursoInfoeste
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Persistencia persistencia)
        {
            string tenantId = string.Empty;
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                var claim = context.User.Claims?.FirstOrDefault(x => x.Type == "TenantId");
                tenantId = claim.Value.ToString();

            }else
            {
                tenantId = context.Request.Headers["TenantId"].ToString();
                
            }
            persistencia.TenantId = string.IsNullOrEmpty(tenantId) ? 0 : int.Parse(tenantId);
            //Antes do endpoint
            await _next(context);

            //Depois do endpoint
        }
    }
}
