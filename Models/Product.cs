using CursoInfoeste.Models.Base;

namespace CursoInfoeste.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }
    }
}
