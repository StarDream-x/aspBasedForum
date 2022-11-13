using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class Content
    {
        public long id { get; set; }
        public string title { get; set; }
        public string authorId { get; set; }
        public long viewCount { get; set; }
        public long likeCount { get; set; }
        public long favoriteCount { get; set; }
        public long publishTime { get; set; }
        public string body { get; set; }
        public MediaItem[] media { get; set; }
        public Comment[] comments { get; set; }
    }
}