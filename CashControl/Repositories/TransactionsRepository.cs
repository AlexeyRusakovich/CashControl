 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using CashControl.Interfaces;
 using CashControl.Models;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;

namespace CashControl.Api
{
    public class TransactionsRepository: ITransactionsRepository
    {
        private readonly CashControlContext _context;
        public TransactionsRepository(CashControlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transactions>> GetAll()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transactions> GetById(int id)
        {
            var result = await _context.Transactions.Where(t => t.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public Task<Transactions> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Create(Transactions item)
        {
            await _context.Transactions.AddAsync(item);
            await Save();
        }

        public async Task Update(Transactions item)
        {
            var result = await GetById(item.Id);
            result = item;
            await Save();
        }

        public async Task Delete(int id)
        {
            var deletedEntity = await GetById(id);
            var result = _context.Transactions.Remove(deletedEntity);
            await Save();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}