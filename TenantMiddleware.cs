using CursoInfoeste.Services;

namespace CursoInfoeste
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Persistencia persistencia)
        {
            var tenantId = context.Request.Headers["TenantId"].ToString();
            if (!string.IsNullOrEmpty(tenantId))
            {
                persistencia.TenantId = int.Parse(tenantId);
            }
            
            Console.WriteLine("➡️  Antes do próximo middleware");

            await _next(context);

            Console.WriteLine("⬅️  Depois do próximo middleware");
        }
    }
}
