using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebAPI.Models
{
    [DataContract]
    public class Department
    {
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        //настройка связи с факультетом
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        //одна кафедра, но несколько групп и несколько учителей
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

        public Department()
        {
            Groups = new List<Group>();
            Teachers = new List<Teacher>();
        }
    }
}
