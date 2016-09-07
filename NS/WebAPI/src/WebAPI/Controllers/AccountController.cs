using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Newtonsoft.Json;
using WebAPI.Support;

namespace WebAPI.Controllers
{
    public class AccountController : Controller
    {
        DSTUContext db;
        public AccountController(DSTUContext context)
        {
            db = context;
        }

        public int AddServiceAccount(string Login, string Password, string ServiceName)
        {
            DeliveryData Service = new DeliveryData { Login = Login, Password = Password, ServiceName = ServiceName };
            db.DeliveryDatas.Add(Service);
            db.SaveChanges();
            return 1;
        }

        public int AddStudent(string Student)
        {
            StudentJson student = JsonConvert.DeserializeObject<StudentJson>(Student);
            db.Faculties.ToArray();
            db.Departments.ToArray();
            db.Groups.ToArray();
            var g = db.Groups.Where(p => p.Name == student.Group).ToArray();
            Student NewS = new Student
            {
                BookNumber = student.BookNumber,
                PhoneNumber = student.PhoneNumber,
                EmailAddress = student.EmailAddress,
                Password = student.Password,
                Courseumber = Convert.ToInt32(student.Group.ElementAt(student.Group.Length - 2).ToString()),
                Group = g[0],
                GroupId = g[0].Id
            };
            if (((db.Students.FirstOrDefault(p => p.BookNumber == NewS.BookNumber)) == null))
            {
                if (db.Students.FirstOrDefault(p => p.EmailAddress == NewS.EmailAddress) == null)
                {
                    if (db.Students.FirstOrDefault(p => p.PhoneNumber == NewS.PhoneNumber) == null)
                    {
                        db.Students.Add(NewS);
                        g[0].Students.Add(NewS);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }

        public int AddTeacher(string Teacher)
        {
            TeacherJson teacher = JsonConvert.DeserializeObject<TeacherJson>(Teacher);
            db.Faculties.ToArray();
            db.Departments.ToArray();
            db.Groups.ToArray();
            var d = db.Departments.Where(p => p.Name == teacher.Department).ToArray();
            Teacher NewT = new Teacher
            {
                Login = teacher.Login,
                Password = teacher.Password,
                Surname = teacher.Surname,
                Name = teacher.Name,
                Patronymic = teacher.Patronymic,
                Department = d[0],
                DepartmentId = d[0].Id
            };
            if (db.Teachers.FirstOrDefault(p => p.Login == NewT.Login) == null)
            {
                db.Teachers.Add(NewT);
                d[0].Teachers.Add(NewT);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int SignInTeacher(string Login, string Password)
        {
            var t = db.Teachers.FirstOrDefault(p => p.Login == Login);
            if ((t == null)||(t.Password != Password))
            {
                return -1;
            }
            return 1;
        }

        public int SignInStudent(string Login, string Password)
        {
            var s = db.Students.FirstOrDefault(p => p.BookNumber == Login);
            if ((s == null) || (s.Password != Password))
            {
                return -1;
            }
            return 1;
        }     

    }
}
