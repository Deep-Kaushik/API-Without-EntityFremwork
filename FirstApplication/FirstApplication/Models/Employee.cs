using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContectNumber { get; set; }
        public string Gender { get; set; }
        public string Contrey { get; set; }
    }

    public class SaveEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContectNumber { get; set; }
        public string Gender { get; set; }
        public string Contrey { get; set; }
    }
}
