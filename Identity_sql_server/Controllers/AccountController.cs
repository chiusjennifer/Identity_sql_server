using Identity_sql_server.Data;
using Identity_sql_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity_sql_server.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        private async Task<IActionResult> Register(Customers viewModel)
        {
            // if (ModelState.IsValid)
            var customers = new Customers
            {
                username = viewModel.username,
                password = viewModel.password,
                realname = viewModel.realname,
                email = viewModel.email,
                phone = viewModel.phone
            };
              _context.customers.Add(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login","Account");
           // return View();
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.customers.FirstOrDefault(u => u.username == username && u.password == password);
            if (user != null)
            {
                return RedirectToAction("Index", "Home"); //登入成功，跳轉至首頁
            }
            ModelState.AddModelError("", "無效的帳號或密碼");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListCust()
        {
            var users = await _context.customers.ToListAsync();
            return View(users);
        }
    }
}
