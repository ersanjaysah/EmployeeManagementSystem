using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class AdminLoginModel
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
    }
}
