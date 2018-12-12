using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashControl.Controllers
{
    public class ReportController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}