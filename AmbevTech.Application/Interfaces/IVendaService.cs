using AmbevTech.Domain.Models;

namespace AmbevTech.Application.Interfaces
{
    public interface IVendaService
    {
        Task<Venda> CreateVendaAsync(Venda venda);
        Task<Venda> UpdateVendaAsync(Venda venda);
        Task CancelVendaAsync(int numeroVenda);
        Task CancelItemAsync(int numeroVenda, int itemId);
    }
}
