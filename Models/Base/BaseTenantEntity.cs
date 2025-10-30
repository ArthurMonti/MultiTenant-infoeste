namespace CursoInfoeste.Models.Base
{
    public abstract class BaseTenantEntity : BaseEntity
    {
        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }
    }
}
