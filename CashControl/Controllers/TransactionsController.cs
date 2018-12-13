using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly ITransactionTypesRepository _transactionTypes;

        public TransactionsController(ITransactionsRepository transactionsRepository,
                                      ICurrenciesRepository currenciesRepository,
                                      ICategoriesRepository categoriesRepository,
                                      ITransactionTypesRepository transactionTypesRepository)
        {
            _transactions = transactionsRepository;
            _currencies = currenciesRepository;
            _categories = categoriesRepository;
            _transactionTypes = transactionTypesRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            await FillRequiredViewBagData();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Transactions transaction, string[] Categories)
        {
            transaction.Date = DateTime.Now;
            transaction.UserLogin = User.Identity.Name;
            transaction.Categories = string.Join(", ", Categories);

            if (ModelState.IsValid)
            {
                await _transactions.Create(transaction);
            }

            await FillRequiredViewBagData();

            return View("Index", transaction);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            await _transactions.Delete(id);

            await FillRequiredViewBagData();

            return View("Index");
        }

        private async Task FillRequiredViewBagData()
        {
            ViewBag.Transactions = await _transactions.GetByRange(User.Identity.Name, DateTime.Today, DateTime.Today.AddDays(1));
            ViewBag.Currencies = await _currencies.GetAll();
            ViewBag.Categories = await _categories.GetAll();
            ViewBag.TransactionTypes = await _transactionTypes.GetAll();
        }
    }
}
