namespace Nop.Plugin.Product.VideoUploader.Routing
{
    public static class Routes
    {
        public const string VideoUrlPath = "/videos/{0}";

        public static string GetVideoPath(string videoShortLink)
        {
            return string.Format(VideoUrlPath, videoShortLink);
        }
    }
}