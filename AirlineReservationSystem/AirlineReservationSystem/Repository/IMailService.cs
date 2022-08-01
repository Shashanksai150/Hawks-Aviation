using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Repository
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
