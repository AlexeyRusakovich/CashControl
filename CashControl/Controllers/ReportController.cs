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
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
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
            var currentCurrency = currencies.FirstOrDefault(c => c.Id == currencyId)?.Abbreviation;

            var dateRange = startDate.ListAllDates(endDate);
            ViewBag.Labels = dateRange.Select(d => d.ToShortDateString()).ToArray();
            var incomeTransactions = transactions.Where(t => t.TransactionTypeId == 1);
            var outcomeTransactions = transactions.Where(t => t.TransactionTypeId == 2);

            List<double> incomeDataSet  = new List<double>();
            List<double> outcomeDataSet = new List<double>();

            foreach (var date in dateRange)
            {
                var incomeTransaction = incomeTransactions.Where(t => t.Date == date);
                var outcomeTransaction = outcomeTransactions.Where(t => t.Date == date);
                if (incomeTransaction.Any())
                    incomeDataSet.Add(decimal.ToDouble(incomeTransaction.Sum(t => t.Amount)));
                else
                    incomeDataSet.Add(0);

                if(outcomeTransaction.Any())
                    outcomeDataSet.Add(decimal.ToDouble(outcomeTransaction.Sum(t => t.Amount)));
                else
                    outcomeDataSet.Add(0);
            }

            ViewBag.IncomeDataSet = incomeDataSet;
            ViewBag.OutcomeDataSet = outcomeDataSet;

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
            ViewBag.TotalIncome = incomeDataSet.Sum(x => x);
            ViewBag.TotalOutCome = outcomeDataSet.Sum(x => x);
            return View();
        }

        [Authorize]
        public async Task<IActionResult> GetReport()
        {
            return View("Index");
        }
    }
}