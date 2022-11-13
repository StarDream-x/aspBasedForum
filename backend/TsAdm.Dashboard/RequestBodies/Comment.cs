using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class Comment
    {
        public long id { get; set; }
        public UserInfo user { get; set; }
        public string content { get; set; }
        public long likeCount { get; set; }
        public long publishTime { get; set; }
        public bool like { get; set; }
    }
}