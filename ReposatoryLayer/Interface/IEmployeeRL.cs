using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface IEmployeeRL
    {
        public EmpRegistration Registration(EmpRegistration empRegistration);
        public bool UpdateEmployee(int EmpId, EmpRegistration updateEmployee);
        public bool DeleteEmployee(int EmpId);
        public List<EmpRegistration> GetAllEmployee();
    }
}
