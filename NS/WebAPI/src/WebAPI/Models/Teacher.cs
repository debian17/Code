namespace WebAPI.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }

        //настройка связи с кафедрой
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
