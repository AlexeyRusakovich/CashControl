using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Api;
using CashControl.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CashControl.Models;

namespace CashControl.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository transactions;

        public TransactionsController(ITransactionsRepository transactionsRepository)
        {
            transactions = transactionsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
