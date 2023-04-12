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
        public IActionResult Delete(string StudentId)
        {
            //using (var context = new StudentManagementDemoContext())
            //{
            //    // Tìm sinh viên cần xóa
            //    var student = context.Students.Find(StudentId);

            //    // Xóa các tài khoản liên quan đến sinh viên
            //    foreach (var account in student.Accounts.ToList())
            //    {
            //        context.Accounts.Remove(account);
            //    }

            //    // Xóa sinh viên
            //    context.Students.Remove(student);

            //    // Lưu thay đổi vào cơ sở dữ liệu
            //    context.SaveChanges();
            //}
            return RedirectToAction("LoginSuccess", "Detail", new { StudentID = "Admin" });
        }
        public IActionResult Save(string StudentID, string Name, string Address, string DateOfBirth, bool Gender, double Score)
        {

            //System.Diagnostics.Debug.WriteLine(StudentID+Name+ Address);
            StudentManagementDemoContext db = new StudentManagementDemoContext();
            using (db)
            {
                // Lấy đối tượng bản ghi cần cập nhật từ cơ sở dữ liệu
                var student = db.Students.FirstOrDefault(s => s.StudentId == StudentID);

                if (student != null)
                {
                    // Cập nhật giá trị của trường trong đối tượng bản ghi
                    student.StudentId = StudentID;
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
