using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class RegisterBody
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}