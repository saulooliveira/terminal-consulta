using System.Net.Mail;
using System.Net;
using System.IO;

public static class EmailService
{
    public static void SendEmail(string subject, string body, string attachmentPath)
    {
        var client = new SmtpClient("smtp.seudominio.com")
        {
            Credentials = new NetworkCredential("usuario", "senha"),
            EnableSsl = true,
            Port = 587
        };

        var mail = new MailMessage("remetente@seudominio.com", "destinatario@seudominio.com")
        {
            Subject = subject,
            Body = body
        };

        if (File.Exists(attachmentPath))
            mail.Attachments.Add(new Attachment(attachmentPath));

        client.Send(mail);
    }
}
