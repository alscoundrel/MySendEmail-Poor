using System;
using SendGrid.Helpers.Mail;

namespace MySendEmail.Cli
{

    class SingleEmail{
        public EmailAddress From { get; set;}
        public string Subject { get; set;}
        public EmailAddress To { get; set;}
        public string PlainTextContent { get; set;}

        public string HtmlContent { get; set;}


        public SingleEmail(EmailAddress from, string subject, EmailAddress to, string plainTextContent, string htmlContent){
            From = from;
            Subject = subject;
            To = to;
            PlainTextContent = plainTextContent;
            HtmlContent = htmlContent;
        }

        public SendGridMessage ConstroiMensagem(){
            return MailHelper.CreateSingleEmail(From, To, Subject, PlainTextContent, HtmlContent);
        }
    }
}