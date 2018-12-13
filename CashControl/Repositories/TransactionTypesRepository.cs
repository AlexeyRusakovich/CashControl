using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Interfaces;
using CashControl.Models;
using Microsoft.EntityFrameworkCore;

namespace CashControl.Repositories
{
    public class TransactionTypesRepository : ITransactionTypesRepository
    {
        private readonly CashControlContext _context;

        public TransactionTypesRepository(CashControlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionType>> GetAll()
        {
            return await _context.TransactionType.ToListAsync();
        }

        public Task<TransactionType> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionType> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(TransactionType item)
        {
            throw new NotImplementedException();
        }

        public Task Update(TransactionType item)
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
