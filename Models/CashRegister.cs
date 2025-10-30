using CursoInfoeste.Models.Base;

namespace CursoInfoeste.Models
{
    public class CashRegister : BaseTenantEntity
    {
        public int Number { get; set; }
        public bool IsOpen { get; set; }
    }
}
