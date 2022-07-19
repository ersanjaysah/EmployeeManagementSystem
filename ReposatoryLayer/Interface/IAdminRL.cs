using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface IAdminRL
    {
        public AdminLoginModel Adminlogin(AdminLogin adminLogin);
    }
}
