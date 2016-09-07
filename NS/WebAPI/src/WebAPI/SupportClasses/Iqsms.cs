using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebAPI.SupportClasses
{
    [DataContract]
    public class Iqsms
    {
        [DataMember]
        public string login { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public IqsmsMessage[] messages { get; set; }

        public Iqsms(int length)
        {
            messages = new IqsmsMessage[length];
            IqsmsMessage temp = new IqsmsMessage();
            temp.clientId = 0;
            temp.phone = "";
            temp.text = "";
            int l= messages.Length;
            for (int i = 0; i < l; i++)
            {
                messages[i] = temp;
            }
        }
    }
}
