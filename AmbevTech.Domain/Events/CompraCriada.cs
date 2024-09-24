using AmbevTech.Domain.Events.Base;
using AmbevTech.Domain.Models;

namespace AmbevTech.Domain.Events
{
    public class CompraCriada : DomainEvent
    {
        public Venda Venda { get; private set; }
        public CompraCriada(Venda venda) => Venda = venda;
    }

}
