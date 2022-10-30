using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class ContentAbstract
    {
        public int id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string authorId { get; set; }
        public string authorName { get; set; }
        public long viewCount { get; set; }
    }
}