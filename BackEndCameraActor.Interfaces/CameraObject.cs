using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace BackEndCameraActor.Common
{
    [DataContract]
    public class CameraObject
    {
        public CameraObject(string location, int status)
        {
            this.Location = location;
            this.Status = status;
        }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public int Status { get; set; }
    }
}

