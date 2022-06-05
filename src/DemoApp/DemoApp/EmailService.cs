using Azure.Communication.Email;
using Azure.Communication.Email.Models;
public class EmailService
{
    private readonly EmailClient client;

    public EmailService()
    {
        client = new EmailClient(Environment.GetEnvironmentVariable("AzureCommunicationServiceConnectionString"), new EmailClientOptions() { });

    }
    public async Task<EmailDeliveryStatus> Send(string to, string subject, string content)
    {


        
        var c = new EmailContent(subject);
        c.Html = content;
        var msg = new Azure.Communication.Email.Models.EmailMessage(Environment.GetEnvironmentVariable("FromAddress"), c, new EmailRecipients(new[] { new EmailAddress(to) }));

        var res = await client.SendAsync(msg);

        return new EmailDeliveryStatus(Guid.Parse(res.Value.MessageId), SendStatus.Queued.ToString());
    }

    public async Task<EmailDeliveryStatus> Status(Guid messageId)
    {
        var status = await client.GetSendStatusAsync(messageId.ToString());

        return new EmailDeliveryStatus(messageId, status.Value.Status.ToString());
    }
}