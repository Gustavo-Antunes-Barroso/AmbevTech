using AmbevTech.Domain.Events.Base;
using AmbevTech.Domain.Models;

namespace AmbevTech.Domain.Events
{
    public class CompraAlterada : DomainEvent
    {
        public Venda Venda { get; private set; }
        public CompraAlterada(Venda venda) => Venda = venda;
    }
}
