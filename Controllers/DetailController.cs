using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StudentManagementDemo.Models.Entities;
using System.Diagnostics;

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
        public IActionResult ChangePassword(string StudentId)
        {

            ViewBag.MyString = StudentId; // Truyền chuỗi vào ViewBags
            return View();
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
                    Student newStudent = new Student();
                    Account newAccount = new Account();

                    
                    newStudent.StudentId = StudentID;
                    newStudent.Name = Name;
                    newStudent.Address = Address;
                    newStudent.DateOfBirth = DateOfBirth;
                    newStudent.Gender = Gender;
                    newStudent.Score = Score;
                    newAccount.Account1 = StudentID;
                    newAccount.Password = password;

                    db.Students.Add(newStudent);
                    db.Accounts.Add(newAccount);
                    // Lưu các thay đổi vào cơ sở dữ liệu

                    db.SaveChanges();
                }
                else
                {
                    Response.WriteAsync("<script>alert('Existed Student ID!!!');</script>");
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
        public IActionResult SaveChangePassword(string StudentId, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            StudentManagementDemoContext db = new StudentManagementDemoContext();
            var account = db.Accounts.FirstOrDefault(s => s.Account1 == StudentId);

            if (account.Password!=OldPassword)
            {
                return RedirectToAction("WrongOldPassword", "Detail");
            }
            else
            {
                if (NewPassword!=ConfirmPassword)
                {
                    return RedirectToAction("WrongConfirmPassword", "Detail");
                }
                else
                {
                  
                    account.Password= NewPassword;
                    db.SaveChanges();
                    ViewBag.Message = "Change Password Successfully!!!";
                    return RedirectToAction("LoginSuccess", "Detail", new { StudentID = StudentId });
                }
            }
        }
        public IActionResult WrongOldPassword()
        {
            return View();
        }
        public IActionResult WrongConfirmPassword()
        {
            return View();
        }
    }
}
