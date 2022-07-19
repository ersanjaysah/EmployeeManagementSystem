﻿using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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


        // [Authorize(Roles = Role.Admin)]
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

       // [Authorize(Roles = Role.Admin)]
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
                else { return this.BadRequest(new { Success = false, message = "Enter Valid Employee" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}