using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Repository
{
    public interface IBookingRepository
    {
        int CreateNewBooking(Bookings bookings);
        List<Bookings> GetAllBookings();
        List<Bookings> GetAllCustBookings(int custId);
        Bookings GetBookingById(int Id);
        int CancelBookingbyId(int Id);
        int UpdateBooking(Bookings bookings);
        int CheckInBooking(int Id);
    }
}
