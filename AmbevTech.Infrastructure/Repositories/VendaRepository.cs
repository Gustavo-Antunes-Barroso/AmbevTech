using AmbevTech.Domain.Interfaces;
using AmbevTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tingle.EventBus;

namespace AmbevTech.Infrastructure.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly VendasContext _context;

        public VendaRepository(VendasContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task<Venda> GetByIdAsync(int id)
        {
            return await _context.Vendas.Include(v => v.Itens).FirstOrDefaultAsync(v => v.NumeroVenda == id);
        }

        public async Task<ItemVenda> GetItemByIdAsync(int id)
        {
            return await _context.ItensVenda.FindAsync(id);
        }

        public async Task UpdateItemAsync(ItemVenda item)
        {
            _context.ItensVenda.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
