﻿using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IEmployeeBL
    {
        public EmpRegistration Registration(EmpRegistration empRegistration);
       public bool UpdateEmployee(int EmpId, EmpRegistration updateEmployee);

    }
}
