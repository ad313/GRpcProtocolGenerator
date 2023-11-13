namespace Mic.Playwrights
{
    public class InstanceConfig
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string CodeUrl { get; set; }

        public string GetKey() => $"{User} | {Url}";
    }
}