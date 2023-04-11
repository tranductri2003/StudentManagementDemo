using Microsoft.AspNetCore.Mvc;
using StudentManagementDemo.Models;
using StudentManagementDemo.Models.Entities;
using System.Diagnostics;

namespace StudentManagementDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult checkingAccount(string StudentID,string Password)
        {
            StudentManagementDemoContext db = new StudentManagementDemoContext();
            var s=db.Accounts.Where(p=>p.Account1==StudentID && p.Password==Password).Select(p=>p).FirstOrDefault();
            if (s != null)
            {
                return RedirectToAction("LoginSuccess", "Detail", new { StudentID = StudentID });
            }
            else
            {
                return RedirectToAction("LoginFail", "Detail");
            }
        }
    }
}