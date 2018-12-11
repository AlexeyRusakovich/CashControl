using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Interfaces;
using CashControl.Models;
using Microsoft.EntityFrameworkCore;

namespace CashControl.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly CashControlContext _context;

        public CategoriesRepository(CashControlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Category item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Category item)
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
