using AmbevTech.Application.Interfaces;
using AmbevTech.Domain.Events;
using AmbevTech.Domain.Exception;
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
            Venda vendaDb = await _vendaRepository.GetByIdAsync(venda.NumeroVenda);
            if (vendaDb is not null)
                throw new BusinessException($"Venda {venda.NumeroVenda} já existente!");

            await _vendaRepository.AddAsync(venda);
            await _eventBus.PublishAsync(new CompraCriada(venda));

            return venda;
        }

        public async Task<Venda> UpdateVendaAsync(Venda venda)
        {
            var vendaDb = await ValidarVendaExistente(venda.NumeroVenda);
            await _vendaRepository.UpdateAsync(venda);
            await _eventBus.PublishAsync(new CompraAlterada(venda));

            return venda;
        }

        public async Task CancelVendaAsync(int numeroVenda)
        {
            var venda = await ValidarVendaExistente(numeroVenda);
            venda.Cancelado = true;
            await _vendaRepository.UpdateAsync(venda);
            await _eventBus.PublishAsync(new CompraCancelada(numeroVenda));
        }

        public async Task CancelItemAsync(int numeroVenda, int itemId)
        {
            var venda = await ValidarVendaExistente(numeroVenda);
            bool itemExiste = venda.Itens.Any(i => i.Id == itemId);

            if (!itemExiste)
                throw new BusinessException($"Item {itemId} não existe na venda {numeroVenda}");

            venda.Itens.FirstOrDefault(x => x.Id == itemId).Cancelado = true;
            await _vendaRepository.UpdateAsync(venda);
            await _eventBus.PublishAsync(new ItemCancelado(itemId));
        }

        private async Task<Venda> ValidarVendaExistente(int vendaId)
        {
            Venda venda = await _vendaRepository.GetByIdAsync(vendaId);

            if (venda is null)
                throw new BusinessException($"Venda {vendaId} não existe!");

            return venda;
        }
    }
}
