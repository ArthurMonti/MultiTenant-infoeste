using CursoInfoeste.Models.Base;

namespace CursoInfoeste.Models
{
    public class Order : BaseTenantEntity
    {
        public decimal Value { get; set; }
    }
}
