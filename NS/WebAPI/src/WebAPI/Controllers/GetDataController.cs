using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{

    public class GetDataController : Controller
    {
        DSTUContext db;
        public GetDataController(DSTUContext context)
        {
            db = context;
        }

        public JsonResult GetFaculties()
        {                  
            var f = db.Faculties.ToArray();
            string[] s = new string[f.Length];

            int l = s.Length;
            for (int i = 0; i < l; i++)
            {
                s[i] = f[i].Name;
            }
            return new JsonResult(s);
        }
        public JsonResult GetDepartments()
        {
            var d = db.Departments.ToArray();
            string[] s = new string[d.Length];

            int l = s.Length;
            for(int i = 0; i < l; i++){
                s[i] = d[i].Name;
            }
            return new JsonResult(s);
        }
        public JsonResult GetGroups()
        {
            var g = db.Groups.ToArray();
            string[] s = new string[g.Length];

            int l = s.Length;
            for (int i = 0; i < l; i++)
            {
                s[i] = g[i].Name;
            }
            return new JsonResult(s);
        }
        public JsonResult GetDepartmentsFromFaculty(string FacultyName)
        {
            var d = db.Departments.Where(p => p.Faculty.Name == FacultyName).ToArray();
            string[] s = new string[d.Length];
            for(int i=0; i < s.Length; i++)
            {
                s[i] = d[i].Name;
            }
            return new JsonResult(s);           
        }
        public JsonResult GetGroupsFromDepartment(string DepartmentName)
        {
            var g = db.Groups.Where(p => p.Department.Name == DepartmentName).ToArray();
            string[] s = new string[g.Length];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = g[i].Name;
            }
            return new JsonResult(s);
        }
        public JsonResult GetTeacherFaculty(string Login)
        {
            db.Faculties.ToArray();
            db.Departments.ToArray();
            db.Teachers.ToArray();
            var t = db.Teachers.Where(p => p.Login == Login).ToArray();
            string f = t[0].Department.Faculty.Name;
            return new JsonResult(f);
        }
              
    }
}
