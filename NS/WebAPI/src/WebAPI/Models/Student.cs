using System.Collections.Generic;

namespace WebAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string BookNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }       
        public int Courseumber { get; set; }

        //настройка связи с группой
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public Student()
        {
            Messages = new List<Message>();
        }

    }
}
