using AmbevTech.Domain.Events.Base;

namespace AmbevTech.Domain.Events
{
    public class CompraCancelada : DomainEvent
    {
        public int NumeroVenda { get; private set; }
        public CompraCancelada(int numeroVenda) => NumeroVenda = numeroVenda;
    }
}
