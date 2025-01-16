﻿using MimeKit;

namespace Hydra.Infrastructure.Notification.Email.Models
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
            Attachments = new List<MimeEntity>();
            AttachmentPaths = new List<string>();
        }

        public string UID { get; set; }
        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Content { get; set; }
        public List<MimeEntity> Attachments { get; set; }
        public List<string> AttachmentPaths { get; set; }
    }
}
