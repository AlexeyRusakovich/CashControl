using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Models;

namespace CashControl.Interfaces
{
    public interface ITransactionsRepository : IRepository<Transactions>
    {
        Task<IEnumerable<Transactions>> GetByRange(string UserLogin, DateTime start, DateTime end);
    }
}
