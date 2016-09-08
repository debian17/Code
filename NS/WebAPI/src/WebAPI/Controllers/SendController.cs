using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Newtonsoft.Json;
using WebAPI.Support;

namespace WebAPI.Controllers
{
    public class SendController : Controller
    {
        DSTUContext db;
        public SendController(DSTUContext context)
        {
            db = context;
        }

        private string  GetFinallyMessage(string Theme, string Text, string Login)
        {
            string message = String.Empty;
            string Surname = db.Teachers.FirstOrDefault(p => p.Login == Login).Surname;
            string Name = db.Teachers.FirstOrDefault(p => p.Login == Login).Name;
            string Patronymic = db.Teachers.FirstOrDefault(p => p.Login == Login).Patronymic;
            message += "Тема: " + Theme + ". " + Text + " Отправитель: " + Surname + " " + Name[0] + ". " + Patronymic[0] + ".";
            return message;
        }

        private string GetSender(string Login)
        {
            string res = String.Empty;
            string Surname = db.Teachers.FirstOrDefault(p => p.Login == Login).Surname;
            string Name = db.Teachers.FirstOrDefault(p => p.Login == Login).Name;
            string Patronymic = db.Teachers.FirstOrDefault(p => p.Login == Login).Patronymic;
            return Surname + " " + Name[0] + ". " + Patronymic[0] + ".";
        }

        public async Task<int> SendAllFacultyAsync(string Theme, string Text, string Login, string Password)
        {
            if ((db.Teachers.FirstOrDefault(p => p.Login == Login) != null) && (db.Teachers.FirstOrDefault(p => p.Password == Password)) != null)
            {
                db.Faculties.ToArray();
                db.Departments.ToArray();
                var t = db.Teachers.FirstOrDefault(p => p.Login == Login);
                string FacultyName = t.Department.Faculty.Name;
                var students = db.Students.Where(p => p.Group.Department.Faculty.Name == FacultyName).ToArray();
                string message = GetFinallyMessage(Theme, Text, Login);
                var service = db.DeliveryDatas.FirstOrDefault(p => p.ServiceName == "SMS");
                int l = students.Length;
                string date = DateTime.Now.ToString();
                for (int i = 0; i < l; i++)
                {                    
                    Message mes = new Message { Sender = GetSender(Login),
                        TextMessage = Text,
                        IsSended = false,
                        IsWatched = false,
                        Student = students[i],
                        StudentId = students[i].Id,
                        Theme = Theme,
                        Date = date };
                    db.Messages.Add(mes);
                    students[i].Messages.Add(mes);
                }
                db.SaveChanges();
                try
                {
                    for (int i = 0; i < l; i++)
                    {
                        await Server.GetResponseAsync("https://smsc.ru/sys/send.php?login=" + service.Login + "&" +
                        "psw=" + service.Password + "&" +
                        "phones=" + students[i].PhoneNumber + "&" +
                        "mes=" + message + "&" +
                        "charset=utf-8");
                    }
                }
                catch (System.Net.WebException)
                {
                    return -2;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> SendAllDepartmentAsync(string Theme, string Text, string DepartmentName, string Login, string Password)
        {
            if ((db.Teachers.FirstOrDefault(p => p.Login == Login) != null) && (db.Teachers.FirstOrDefault(p => p.Password == Password)) != null)
            {
                db.Faculties.ToArray();
                db.Departments.ToArray();
                db.Groups.ToArray();       
                var students = db.Students.Where(p => p.Group.Department.Name==DepartmentName).ToArray();
                string message = GetFinallyMessage(Theme, Text, Login);
                var service = db.DeliveryDatas.FirstOrDefault(p => p.ServiceName == "SMS");
                int l = students.Length;
                string date = DateTime.Now.ToString();
                for (int i = 0; i < l; i++)
                {                   
                    Message mes = new Message { Sender = GetSender(Login),
                        TextMessage = Text,
                        IsSended = false,
                        IsWatched = false,
                        Student = students[i],
                        StudentId = students[i].Id,
                        Theme = Theme,
                        Date = date };
                    db.Messages.Add(mes);
                    students[i].Messages.Add(mes);
                }
                db.SaveChanges();
                try
                {
                    for(int i = 0; i < l; i++)
                    {
                        await Server.GetResponseAsync("https://smsc.ru/sys/send.php?login=" + service.Login + "&" +
                        "psw=" + service.Password + "&" +
                        "phones=" + students[i].PhoneNumber + "&" +
                        "mes=" + message + "&" +
                        "charset=utf-8");
                    }
                }
                catch (System.Net.WebException)
                {
                    return -2;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> SendGroupsSet(string options)
        {
            var Params = JsonConvert.DeserializeObject<SentOptions>(options);
            if ((db.Teachers.FirstOrDefault(p => p.Login == Params.Login) != null) && (db.Teachers.FirstOrDefault(p => p.Password == Params.Password)) != null)
            {
                Student[] S = new Student[0];
                for(int i = 0; i < Params.Groups.Length; i++)
                {
                   Student[] a= db.Students.Where(p => p.Group.Name == Params.Groups[i]).ToArray();
                   S = a.Concat(S).ToArray();
                }
                string message = GetFinallyMessage(Params.Theme, Params.Text, Params.Login);
                var service = db.DeliveryDatas.FirstOrDefault(p => p.ServiceName == "SMS");
                int l = S.Length;
                string date = DateTime.Now.ToString();
                for (int i = 0; i < l; i++)
                {                   
                    Message mes = new Message { Sender = GetSender(Params.Login),
                        TextMessage = Params.Text,
                        IsWatched = false,
                        IsSended = false,
                        Student = S[i],
                        StudentId = S[i].Id,
                        Theme = Params.Theme,
                        Date = date };
                    db.Messages.Add(mes);
                    S[i].Messages.Add(mes);
                }
                db.SaveChanges();
                try
                {
                    for (int i = 0; i < l; i++)
                    {
                        await Server.GetResponseAsync("https://smsc.ru/sys/send.php?login=" + service.Login + "&" +
                        "psw=" + service.Password + "&" +
                        "phones=" + S[i].PhoneNumber + "&" +
                        "mes=" + message + "&" +
                        "charset=utf-8");
                    }
                }
                catch (System.Net.WebException)
                {
                    return -2;
                }           
                return 1;
            }
            else
            {
                return -1;
            }           
        }

        public string GetMessages(string Login, string Password, int New)
        {
            if ((db.Students.FirstOrDefault(p => p.BookNumber == Login) != null) && (db.Students.FirstOrDefault(p => p.Password == Password)) != null)
            {
                var all_m = db.Messages.Where(p => p.Student.BookNumber == Login).ToArray();
                if (New == 0)
                {                   
                    return JsonConvert.SerializeObject(all_m);
                }
                else
                {
                    all_m = all_m.Where(p => p.IsSended == false).ToArray();
                    if (all_m.Length==0)
                    {
                        return null;
                    }                                    
                    return JsonConvert.SerializeObject(all_m);
                }                             
            }
            else
            {
                return null;
            }
        }

        public int VerifyMessages(string mesId, string Login, string Password)
        {
            if ((db.Students.FirstOrDefault(p => p.BookNumber == Login) != null) && (db.Students.FirstOrDefault(p => p.Password == Password)) != null)
            {
                var Id_arr = JsonConvert.DeserializeObject<int[]>(mesId);
                int l = Id_arr.Length;
                for (int i = 0; i < l; i++)
                {
                    var im = db.Messages.FirstOrDefault(p => p.Id == Id_arr[i]);
                    im.IsSended = true;
                }
                db.SaveChanges();
                return 1;
            }
            return -1;
        }

        public int MarkMessage(string IdMessage, string Login, string Password)
        {
            if ((db.Students.FirstOrDefault(p => p.BookNumber == Login) != null) && (db.Students.FirstOrDefault(p => p.Password == Password)) != null)
            {
                var m = JsonConvert.DeserializeObject<int[]>(IdMessage);
                int l = m.Length;
                for(int i = 0; i < l; i++)
                {
                    var im = db.Messages.FirstOrDefault(p => p.Id == m[i]);                    
                    im.IsWatched = true;                  
                }
                db.SaveChanges();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public string get()
        {
            return "qwerwqwqr";
        }

    }
}
