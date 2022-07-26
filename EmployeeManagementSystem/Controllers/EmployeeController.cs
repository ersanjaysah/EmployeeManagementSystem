using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]//is used to binding the source by manually by applying attribute.
    [Route("[controller]")]//controller is the class that handels the HTTP Request.
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeBL employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }  


        [Authorize(Roles = Role.Admin)]
        [HttpPost("Registration")]//HttpPost used to send the data to the server from HttpClient
        public IActionResult Registration(EmpRegistration empRegistration)
        {
            try
            {
                EmpRegistration EmpData =this.employeeBL.Registration(empRegistration);
                if (EmpData != null)
                {
                    return this.Ok(new {Success = true, message ="User Added Successfully", Response = EmpData});
                }
                else
                {
                    return this.Ok(new { Success = true, message = "Sorry!!! User Already Exist" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });

            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateEmployee/{EmpId}")]
        public IActionResult UpdateEmployee(int EmpId, EmpRegistration updateEmployee)
        {
            try
            {
                var Data = this.employeeBL.UpdateEmployee(EmpId, updateEmployee);
                if (Data == true)
                {
                    return this.Ok(new { Success = true, message = "Employee Updated successfully", Response = Data });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid Employee Id" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

         [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteEmployee/{EmpId}")]
        public IActionResult DeleteEmployee(int EmpId)
        {
            try
            {
                var Data = this.employeeBL.DeleteEmployee(EmpId);
                if (Data == true)
                {
                    return this.Ok(new { Success = true, message = "Employee Deleted Successfully", Response = Data });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter valid EmployeeId" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles =Role.Admin)]
        [HttpGet("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var result=this.employeeBL.GetAllEmployee();
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Employee Details Fatched Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Oops!! Details Not Fatched" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new {Success=false, message = ex.Message});
            }
        }

       

        [HttpPost("EmployeeLogin")] 
        public IActionResult EmployeeLogin(EmpLogin empLogin)
        {
            try
            {
                var result = this.employeeBL.EmployeeLogin(empLogin);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", Token = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Sorry!!! Login Failed", Token = result });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize(Roles = Role.Employee)]
        [HttpGet("EmployeeDetails")]
        public IActionResult EmployeeDetails()
        {
            try
            {
                int EmpId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.employeeBL.EmployeeDetails(EmpId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Here is the Details", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "please Enter valid credentials" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
