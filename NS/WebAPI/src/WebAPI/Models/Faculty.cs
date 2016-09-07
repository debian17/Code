using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebAPI.Models
{
    [DataContract]
    public class Faculty
    {
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public Faculty()
        {
            Departments = new List<Department>();
        }
    }
}
