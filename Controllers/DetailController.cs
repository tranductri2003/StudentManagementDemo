using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Delete(string StudentId)
        {
            StudentManagementDemoContext db = new StudentManagementDemoContext();
            var s = db.Accounts.Where(p => p.Account1 == StudentId).Select(p => p).ToList();
            foreach (var item in s)
            {
                db.Accounts.Remove(item);
            }
            var ss = db.Students.Where(p => p.StudentId == StudentId).Select(p => p).ToList();
            foreach (var item in ss)
            {
                db.Students.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("LoginSuccess", "Detail", new { StudentID = "Admin" });
        }
        public IActionResult SaveAdd(string StudentID, string Name, string Address, string DateOfBirth, bool Gender, double Score, string password)
        {


            StudentManagementDemoContext db = new StudentManagementDemoContext();
            using (db)
            {
                // Lấy đối tượng bản ghi cần cập nhật từ cơ sở dữ liệu
                var student = db.Students.FirstOrDefault(s => s.StudentId == StudentID);
                var account = db.Accounts.FirstOrDefault(s => s.Account1 == StudentID);
                if (student == null)
                {
                    // Cập nhật giá trị của trường trong đối tượng bản ghi
                    student.StudentId = StudentID;
                    if (password != null)
                    {
                        account.Password = password;
                    }
                    student.Name = Name;
                    student.Address = Address;
                    student.DateOfBirth = DateOfBirth;
                    student.Gender = Gender;
                    student.Score = Score;

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
                else
                {
                    //return RedirectToAction("ExistedAccount", "Detail");
                    return View();
                }
            }
            return RedirectToAction("LoginSuccess", "Detail", new { StudentID = "Admin" });
        }
        public IActionResult SaveEdit(string StudentID, string Name, string Address, string DateOfBirth, bool Gender, double Score, string password)
        {


            StudentManagementDemoContext db = new StudentManagementDemoContext();
            using (db)
            {
                // Lấy đối tượng bản ghi cần cập nhật từ cơ sở dữ liệu
                var student = db.Students.FirstOrDefault(s => s.StudentId == StudentID);
                var account = db.Accounts.FirstOrDefault(s=> s.Account1 == StudentID);
                if (student != null)
                {
                    // Cập nhật giá trị của trường trong đối tượng bản ghi
                    student.StudentId = StudentID;
                    if (password !=null)
                    {
                        account.Password = password;
                    }
                    student.Name = Name;
                    student.Address = Address;
                    student.DateOfBirth = DateOfBirth;
                    student.Gender = Gender;
                    student.Score= Score;

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
            }
            return RedirectToAction("LoginSuccess", "Detail", new { StudentID = "Admin" });
        }
    }
}
