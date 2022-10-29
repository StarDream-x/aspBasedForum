using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class Comment
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string content { get; set; }
        public int likeCount { get; set; }
        public int publishTime { get; set; }
        public bool like { get; set; }
    }
}