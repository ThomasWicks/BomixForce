using Bomix_Force.AppServices.Interface;
using Bomix_Force.Data.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.AppServices
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string message, List<IFormFile>? attachments)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                mimeMessage.To.Add(MailboxAddress.Parse(email));


                mimeMessage.Subject = subject;
                
                if (attachments != null && attachments.Count() > 0)
                {
                    var bodyBuilder = addAttachment(message, attachments);
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                }
                else
                {
                    mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = message
                    };
                }


                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                    }
                    else
                    {
                        await client.ConnectAsync(_emailSettings.MailServer, 465/*25*/, false);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

        private BodyBuilder addAttachment(string MailText, List<IFormFile> attachments)
        {
            var bodyBuilder = new BodyBuilder { HtmlBody = MailText };
            foreach (var attachment in attachments)
            {

                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();

                }
                MemoryStream fileStream = new MemoryStream(fileBytes);
                var attach = new MimePart()
                {
                    Content = new MimeContent(fileStream, ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachment.FileName
                };
                bodyBuilder.Attachments.Add(attach.FileName, fileBytes, ContentType.Parse(attach.ContentType.MimeType));
            }

            return bodyBuilder;
        }

    }
}
