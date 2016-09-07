using System.Runtime.Serialization;

namespace WebAPI.SupportClasses
{
    [DataContract]
    public class IqsmsMessage
    {
        [DataMember]
        public int clientId { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string text { get; set; }
    }
}
