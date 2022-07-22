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
            try
            {
                return this.employeeRL.Registration(empRegistration);
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        public bool UpdateEmployee(int EmpId, EmpRegistration updateEmployee)
        {
            try
            {
                return this.employeeRL.UpdateEmployee(EmpId, updateEmployee);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteEmployee(int EmpId)
        {
            try
            {
                return this.employeeRL.DeleteEmployee(EmpId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EmpRegistration> GetAllEmployee()
        {
            try
            {
               return this.employeeRL.GetAllEmployee();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string EmployeeLogin(EmpLogin empLogin)
        {
            try
            {
                return this.employeeRL.EmployeeLogin(empLogin);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
