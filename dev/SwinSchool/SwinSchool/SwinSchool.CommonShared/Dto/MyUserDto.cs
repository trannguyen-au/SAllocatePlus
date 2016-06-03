using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SwinSchool.CommonShared.Dto
{
    [Serializable]
    [DataContract]
    public class MyUserDto
    {
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Tel { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Role { get; set; }
    }
}
