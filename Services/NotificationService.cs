using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarRental_System.Services
{
    public class NotificationService
    {
        private readonly string api, _email, _name;
        public NotificationService(IConfiguration configuration)
        {
            api = configuration["SendGrid:ApiKey"];
            _email = configuration["SendGrid:SenderEmail"];
            _name = configuration["SendGrid:SenderName"];
        }

        public async Task SendNotification(string email, string name, string make, string model, decimal pricePerDay)
        {
            var client = new SendGridClient(api);
            var from = new EmailAddress(_email, _name);
            var subject = "Car Rental Booking Details";
            var to = new EmailAddress(email, name);
            var plainTextContent = $"Welcome {name} to CarRental Application";
            var htmlContent = $"<h1>Your booking for the Car {make} {model} has been successful.</h1> <br>" +
                $"<h1>Your Booking Price Per Day for the above car is Rs.{pricePerDay}</h1>" +
                $"<h2>Thankyou For Choosing CarRentals.</h2>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
