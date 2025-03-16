namespace ScrumPoker.Dto
{
    public class EmailSettingsModelDto
    {
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpServer { get; set; }
        public bool SmtpEnableSSL { get; set; }
    }
}
