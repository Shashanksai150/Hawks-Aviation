using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Repository
{
    public interface ICustomerRepository
    {
        int AddNewCustomer(Customers customer);
        List<Customers> GetAllCustomers();
        Customers GetCustomerById(int Id);
        int DeleteCustomerbyId(int Id);
        int UpdateCustomer(Customers customer);
        int ChangePasswordCustomer(Login creds, string newPassword);

    }
}
