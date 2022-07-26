using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeRL employeeRL;

        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }
        public EmpRegistration Registration(EmpRegistration empRegistration)
        {
            return this.employeeRL.Registration(empRegistration);
        }

        public bool UpdateEmployee(int EmpId, EmpRegistration updateEmployee)
        {
            return this.employeeRL.UpdateEmployee(EmpId, updateEmployee);
        }

        public bool DeleteEmployee(int EmpId)
        {
            return this.employeeRL.DeleteEmployee(EmpId);
        }

        public List<EmpRegistration> GetAllEmployee()
        {
            return this.employeeRL.GetAllEmployee();
        }

        public string EmployeeLogin(EmpLogin empLogin)
        {
            return this.employeeRL.EmployeeLogin(empLogin);
        }

        public EmpRegistration EmployeeDetails(int EmpId)
        {
            return this.employeeRL.EmployeeDetails(EmpId);
        }
    }
}
