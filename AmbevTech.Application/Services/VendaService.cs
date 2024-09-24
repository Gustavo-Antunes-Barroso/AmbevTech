using AmbevTech.Application.Interfaces;
using AmbevTech.Domain.Events;
using AmbevTech.Domain.Interfaces;
using AmbevTech.Domain.Models;

namespace AmbevTech.Application.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IEventBus _eventBus;

        public VendaService(IVendaRepository vendaRepository, IEventBus eventBus)
        {
            _vendaRepository = vendaRepository;
            _eventBus = eventBus;
        }

        public async Task<Venda> CreateVendaAsync(Venda venda)
        {
            await _vendaRepository.AddAsync(venda);
            await _eventBus.PublishAsync(new CompraCriada(venda));
            return venda;
        }

        public async Task<Venda> UpdateVendaAsync(Venda venda)
        {
            await _vendaRepository.UpdateAsync(venda);
            await _eventBus.PublishAsync(new CompraAlterada(venda));
            return venda;
        }

        public async Task CancelVendaAsync(int numeroVenda)
        {
            var venda = await _vendaRepository.GetByIdAsync(numeroVenda);
            venda.Cancelado = true;
            await _vendaRepository.UpdateAsync(venda);
            await _eventBus.PublishAsync(new CompraCancelada(numeroVenda));
        }

        public async Task CancelItemAsync(int itemId)
        {
            var item = await _vendaRepository.GetItemByIdAsync(itemId);
            item.Cancelado = true;
            await _vendaRepository.UpdateItemAsync(item);
            await _eventBus.PublishAsync(new ItemCancelado(itemId));
        }
    }

}
