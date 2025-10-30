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
            var tenantId = context.Request.Headers["TenantId"].ToString();
            persistencia.TenantId = string.IsNullOrEmpty(tenantId) ? 0 : int.Parse(tenantId);
            //Antes do endpoint
            await _next(context);

            //Depois do endpoint
        }
    }
}
