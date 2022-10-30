using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.RequestBodies
{
    public class MediaItem
    {
        public enum mediaType
        {
            picture,
            video
        }
        public mediaType type { get; set; }
        public string imageUrl;
        public string url;
    }
}