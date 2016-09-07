namespace WebAPI.Support
{
    public class SentOptions
    {
        public string Theme { get; set; }
        public string Text { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string[] Groups { get; set; }
        public SentOptions(int length)
        {
            Groups = new string[length];
        }
    }
}
