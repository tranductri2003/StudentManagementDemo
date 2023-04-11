using Microsoft.AspNetCore.Mvc;
using StudentManagementDemo.Models.Entities;

namespace StudentManagementDemo.Controllers
{
    public class DetailController : Controller
    {
       
        public IActionResult LoginFail()
        {
            return View();
        }
        public IActionResult LoginSuccess(string StudentID)
        {
            StudentManagementDemoContext db=new StudentManagementDemoContext();
            if (StudentID == "Admin")
            {
                var s = db.Students.Where(p => p.StudentId != StudentID).Select(p => p).ToList();
                return View(s);
            }
            else
            {
                var s = db.Students.Where(p => p.StudentId == StudentID).Select(p => p).ToList();
                return View(s);
            }
        }
        public IActionResult Edit(string StudentId)
        {
            StudentManagementDemoContext db = new StudentManagementDemoContext();
            var s = db.Students.Where(p => p.StudentId == StudentId).Select(p => p).ToList();
            return View(s);
            
        }
    }
}
