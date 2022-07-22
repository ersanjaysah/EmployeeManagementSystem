using DatabaseLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReposatoryLayer.Service
{
    public class EmployeeRL : IEmployeeRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public EmployeeRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method is used for Register the Employee details by admin
        /// </summary>
        /// <param name="empRegistration"></param>
        /// <returns></returns>
        public EmpRegistration Registration(EmpRegistration empRegistration)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagementSystem"]);
            try
            {
                using(sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPEmpRegistration", this.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //command type is a class to set the store procedure
                    cmd.Parameters.AddWithValue("@FullName", empRegistration.FullName);
                    cmd.Parameters.AddWithValue("@Email", empRegistration.Email);
                    cmd.Parameters.AddWithValue("@Password", empRegistration.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", empRegistration.MobileNumber);
                    cmd.Parameters.AddWithValue("@Address", empRegistration.Address);
                    cmd.Parameters.AddWithValue("@Gender", empRegistration.Gender);
                    cmd.Parameters.AddWithValue("@Position", empRegistration.Position);
                    cmd.Parameters.AddWithValue("@Salary", empRegistration.Salary);

                    sqlConnection.Open();
                    int result=cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result!=0)
                    {
                        return empRegistration;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

       
        /// <summary>
        /// this method is used for generate the Token
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string Email, int EmpId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("This is My Key To Generate Token");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "Employee"),
                    new Claim("Email", Email),
                    new Claim("EmpId", EmpId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        /// <summary>
        /// This method is used for update Employee
        /// </summary>
        /// <param name="EmpId"></param>
        /// <param name="updateEmployee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(int EmpId, EmpRegistration updateEmployee)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagementSystem"]);
                SqlCommand cmd = new SqlCommand("SPUpdateEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@FullName", updateEmployee.FullName);
                cmd.Parameters.AddWithValue("@Email", updateEmployee.Email);
                cmd.Parameters.AddWithValue("@Password", updateEmployee.Password);
                cmd.Parameters.AddWithValue("@MobileNumber", updateEmployee.MobileNumber);
                cmd.Parameters.AddWithValue("@Address", updateEmployee.Address);
                cmd.Parameters.AddWithValue("@Gender", updateEmployee.Gender);
                cmd.Parameters.AddWithValue("@Position", updateEmployee.Position);
                cmd.Parameters.AddWithValue("@Salary", updateEmployee.Salary);
                
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }

        }

        public bool DeleteEmployee(int EmpId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagementSystem"]);
                SqlCommand cmd = new SqlCommand("SPDeleteEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public List<EmpRegistration> GetAllEmployee()
        {
            try
            {
                List<EmpRegistration> empRegistration = new List<EmpRegistration>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagementSystem"]);
                SqlCommand cmd = new SqlCommand("SPGetAllEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        empRegistration.Add(new EmpRegistration
                        {
                            EmpId = Convert.ToInt32(reader["EmpId"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password=reader["Password"].ToString(),
                            MobileNumber = Convert.ToInt64(reader["MobileNumber"]),
                            Address = reader["Address"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Position = reader["Position"].ToString(),
                            Salary= Convert.ToInt64(reader["Salary"]),
                            
                        });
                    }
                    this.sqlConnection.Close();
                    return empRegistration;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public string EmployeeLogin(EmpLogin empLogin)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagementSystem"]);
                SqlCommand cmd = new SqlCommand("SPLoginEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", empLogin.Email);
                cmd.Parameters.AddWithValue("@Password", empLogin.Password);
                // opening the connection
                this.sqlConnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                // for checking datareader has row or not
                if (reader.HasRows)
                {
                    int EmpId = 0;
                    EmpLogin emp = new EmpLogin();

                    while (reader.Read())
                    {
                        emp.Email = Convert.ToString(reader["Email"]);
                        emp.Password = Convert.ToString(reader["Password"]);
                        EmpId = Convert.ToInt32(reader["EmpId"]);
                    }
                    // closing the connection
                    this.sqlConnection.Close();
                    var Token = this.GenerateJWTToken(emp.Email, EmpId);
                    return Token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
  
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
