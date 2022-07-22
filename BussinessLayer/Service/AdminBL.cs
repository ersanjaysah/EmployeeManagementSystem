using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class AdminBL : IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public AdminLoginModel Adminlogin(AdminLogin adminLogin)
        {
            try
            {
                return this.adminRL.Adminlogin(adminLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
