namespace Scot.Web
{
    /// <summary>
    /// Static Data class
    /// </summary>
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
