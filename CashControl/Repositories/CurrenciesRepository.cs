using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Interfaces;
using CashControl.Models;
using Microsoft.EntityFrameworkCore;

namespace CashControl.Repositories
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private readonly CashControlContext _context;

        public CurrenciesRepository(CashControlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAll()
        {
            return await _context.Currency.ToListAsync();
        }

        public Task<Currency> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Currency> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Currency item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Currency item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
