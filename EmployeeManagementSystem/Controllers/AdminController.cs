using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(AdminLogin adminLogin)
        {
            try
            {
                var result = this.adminBL.Adminlogin(adminLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });

                }
                else
                {
                    return this.BadRequest(new {success=false, message="Login Failed", data= result});
                }
            }
            catch (Exception )
            {

                 return this.BadRequest(new { success = false, message = "Login Failed !!! check Email or Password" });
               
            }
        }
    }
}
