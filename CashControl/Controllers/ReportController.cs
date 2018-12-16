using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Helpers;
using CashControl.Interfaces;
using CashControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashControl.Controllers
{
    public class ReportController : Controller
    {
        private readonly ITransactionsRepository _transactions;
        private readonly ICurrenciesRepository _currencies;
        private readonly ICategoriesRepository _categories;
        private readonly ITransactionTypesRepository _transactionTypes;

        public ReportController(ITransactionsRepository transactionsRepository,
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
        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate, int currencyId)
        {
            if (startDate == default(DateTime) || endDate == default(DateTime) || currencyId == default(int))
            {
                startDate = DateTime.Today;
                endDate = DateTime.Today.AddDays(30);
                currencyId = (int)Enums.Currency.BYN;
            }

            ViewBag.Currencies = await _currencies.GetAll();


            if (endDate < startDate ||
                endDate > startDate.AddDays(30))
            {
                ViewBag.HasRangeError = true;
                return View("Index");
            }
            else
                ViewBag.HasRangeError = false;

            ViewBag.Transactions = await _transactions.GetByRange(User.Identity.Name, startDate, endDate);
            
            var transactions = (IEnumerable<Transactions>)ViewBag.Transactions;
            var currencies = (IEnumerable<Currency>)ViewBag.Currencies;
            var currentCurrency = currencies.Where(c => c.Id == currencyId).FirstOrDefault().Abbreviation;
            ViewBag.Transactions = transactions.Select(t => {
                return new Transactions
                {
                    Amount = (decimal)CurrencyConvertingHelper.Convert(t.Currency.Abbreviation.ToCurrency(), currentCurrency.ToCurrency(), (double)t.Amount),
                    Categories = t.Categories,
                    CurrencyId = currencyId,
                    Description = t.Description,
                    TransactionTypeId = t.TransactionTypeId,
                    TransactionType = t.TransactionType,
                    Date = t.Date,
                    Id = t.Id                    
                };
            }).OrderBy(tr => tr.Date).ToList();
            ViewBag.CurrentCurrency = currentCurrency;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> GetReport()
        {
            return View("Index");
        }
    }
}