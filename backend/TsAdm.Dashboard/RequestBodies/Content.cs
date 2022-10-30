using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class Content
    {
        public int id { get; set; }
        public string title { get; set; }
        public string authorId { get; set; }
        public int viewCount { get; set; }
        public int likeCount { get; set; }
        public int favoriteCount { get; set; }
        public int publishTime { get; set; }
        public string body { get; set; }
        public MediaItem[] media { get; set; }
        public Comment[] comments { get; set; }
    }
}