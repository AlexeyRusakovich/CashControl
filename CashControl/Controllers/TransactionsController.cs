using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Api;
using CashControl.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CashControl.Models;
using Microsoft.AspNetCore.Authorization;

namespace CashControl.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _transactions;
        private readonly ICurrenciesRepository _currencies;
        private readonly ICategoriesRepository _categories;

        public TransactionsController(ITransactionsRepository transactionsRepository,
                                      ICurrenciesRepository currenciesRepository,
                                      ICategoriesRepository categoriesRepository)
        {
            _transactions = transactionsRepository;
            _currencies = currenciesRepository;
            _categories = categoriesRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Transactions = await _transactions.GetByRange(User.Identity.Name, DateTime.Today, DateTime.Today.AddDays(1));
            ViewBag.Currencies = await _currencies.GetAll();
            ViewBag.Categories = await _categories.GetAll();

            return View();
        }
    }
}
