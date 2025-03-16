namespace ScrumPoker.Dto
{
    public class EmailSendModelDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string TemplateName { get; set; }
        public string Body { get; set; }
        public string BccEmails { get; set; }
        public List<string> AttachmentFileUrlList { get; set; } = new();
    }
}
