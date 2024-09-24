using AmbevTech.Domain.Events.Base;

namespace AmbevTech.Domain.Events
{
    public class ItemCancelado : DomainEvent
    {
        public int ItemId { get; private set; }
        public ItemCancelado(int itemId) => ItemId = itemId;
    }
}
