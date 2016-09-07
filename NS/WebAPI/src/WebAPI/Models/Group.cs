using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebAPI.Models
{
    [DataContract]
    public class Group
    {
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        //связь с кафедрой
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Group()
        {
            Students = new List<Student>();
        }
    }
}
