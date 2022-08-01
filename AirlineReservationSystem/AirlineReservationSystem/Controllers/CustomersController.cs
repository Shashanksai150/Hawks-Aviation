using AirlineReservationSystem.DTOs;
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            if (_customerService == null)
            {
                return NotFound();
            }
            return Ok(_customerService.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerbyId(int id)
        {
            if (_customerService == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetCustomerbyId(id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddNewCustomer(CustomerCreateDTO customerDto)
        {
            Customers customer = new Customers();
            customer.CustomerId = 0;
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.MobileNumber = customerDto.MobileNumber;
            customer.Gender = customerDto.Gender;
            customer.Age = customerDto.Age;
            customer.EmailId = customerDto.EmailId;
            customer.Username = customerDto.Username;
            customer.Password = customerDto.Password;
            if (_customerService == null)
            {
                return Problem("Entity set 'CGAirwaysDbContext.CustomerContext'  is null.");
            }
            int val = _customerService.AddNewCustomer(customer);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCustomerbyId", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customers customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            if (_customerService == null)
            {
                return Problem("Entity set 'CGAirwaysDbContext.CustomerContext'  is null.");
            }

            int val = _customerService.UpdateCustomer(customer);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCustomerbyId", new { id = customer.CustomerId }, customer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerbyId(int id)
        {
            if (_customerService == null)
            {
                return NotFound();
            }
            int val = _customerService.DeleteCustomerbyId(id);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult ChangePassword(Changepassword chamodel)
        {

            if (chamodel == null)
            {
                return BadRequest();
            }

            if (_customerService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.Customer'  is null.");
            }

            Login creds = new Login();
            creds.Username = chamodel.Username;
            creds.Password = chamodel.OldPassword;

            String newPwd = chamodel.NewPassword;

            int val = _customerService.ChangePassword(creds, newPwd);
            if (val != 200)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
