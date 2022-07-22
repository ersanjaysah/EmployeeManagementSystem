using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class EmpRegistration
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Email  { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Position { get; set; }
        public long Salary { get; set; }

    }
}
