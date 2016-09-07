using System.Runtime.Serialization;

namespace WebAPI.Models
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public string Theme { get; set; }
        [DataMember]
        public string TextMessage { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public bool IsWatched { get; set; }

        public bool IsSended { get; set; }

        //связь со студентом
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
