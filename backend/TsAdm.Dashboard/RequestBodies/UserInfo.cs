namespace TsAdm.Dashboard.RequestBodies
{
    public class UserInfo
    {
        public string id { get; set; }
        public string avatarUrl { get; set; }
        public string username { get; set; }
        public string introduction { get; set; }
        public long followingCount { get; set; }
        public long followerCount { get; set; }
        public long favoriteCount { get; set; }
        public bool following { get; set; }
    }
}