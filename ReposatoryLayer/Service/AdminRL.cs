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
    public class AdminRL : IAdminRL
    {
        private SqlConnection sqlConnection;
        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public AdminLoginModel Adminlogin(AdminLogin adminLogin)
        {

            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagementSystem"]);
            SqlCommand cmd = new SqlCommand("SPLoginAdmin", this.sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Email", adminLogin.Email);
            cmd.Parameters.AddWithValue("@Password", adminLogin.Password);

            this.sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            AdminLoginModel admin = new AdminLoginModel();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    admin.AdminId = Convert.ToInt32(reader["AdminId"]);
                    admin.FullName = Convert.ToString(reader["FullName"]);
                    admin.Email = Convert.ToString(reader["Email"]);
                    admin.MobileNumber = Convert.ToString(reader["MobileNumber"]);
                    admin.Address = Convert.ToString(reader["Address"]);

                }
                this.sqlConnection.Close();
                admin.Token = this.GenerateSecurityToken(admin.Email, admin.AdminId);
                return admin;
            }
            else
            {
                throw new Exception("Email or Password is Wrong");
            }

        }


        public string GenerateSecurityToken(string emailID, int AdminId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("AdminId", AdminId.ToString())
            };
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
