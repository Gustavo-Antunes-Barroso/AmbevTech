﻿using AmbevTech.Domain.Models;

namespace AmbevTech.Domain.Interfaces
{
    public interface IVendaRepository
    {
        Task AddAsync(Venda venda);
        Task UpdateAsync(Venda venda);
        Task<Venda> GetByIdAsync(int id);
    }
}
