using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Interfaces;
using CashControl.Models;
using Microsoft.EntityFrameworkCore;

namespace CashControl.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CashControlContext _context;

        public UserRepository(CashControlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetById(string id)
        {
            return await _context.Users.Where(u => u.Login == id).FirstOrDefaultAsync();
        }

        public async Task Create(Users item)
        {
            await _context.Users.AddAsync(item);
            await Save();
        }

        public async Task Update(Users item)
        {
            var result = await GetById(item.Login);
            result = item;
            await Save();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            var result = await GetById(id);
            _context.Users.Remove(result);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
