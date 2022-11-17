namespace TsAdm.Dashboard.RequestBodies
{
    public class ContentPost
    {
        public string title { get; set; }
        public string body { get; set; }
        public string[] media { get; set; }
    }
}