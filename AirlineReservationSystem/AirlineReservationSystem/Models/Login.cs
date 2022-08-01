using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservationSystem.Models
{
    public class Login
    {
        
        string? _username;
        [Key]
        public string? Username 
        {   
            get { return _username; } 
            set { _username = value; } 
        }

        string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
