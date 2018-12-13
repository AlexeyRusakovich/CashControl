using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CashControl.Helpers;
using CashControl.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CashControl.Api
{
    public class UserController : Controller
    {
        private readonly CashControlContext db;
        public UserController(CashControlContext cashControlContext)
        {
            db = cashControlContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users user)
        {
            if (ModelState.IsValid)
            {
                Users _user = await db.Users.FirstOrDefaultAsync(u => u.Login == user.Login && EncryptHelper.VerifyHashedPassword(u.Password, user.Password));
                if (_user != null)
                {
                    await Authenticate(user.Login); // аутентификация

                    return RedirectToAction("Index", "Transactions");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {
            if (ModelState.IsValid)
            {
                Users _user = await db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
                if (_user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new Users { Login = user.Login, Password = EncryptHelper.Encrypt(user.Password) });
                    await db.SaveChangesAsync();

                    await Authenticate(user.Login); // аутентификация

                    return RedirectToAction("Index", "Transactions");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(user);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}
