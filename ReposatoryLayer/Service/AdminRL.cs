﻿using DatabaseLayer.Model;
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
            try
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
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin.AdminId = Convert.ToInt32(reader["AdminId"]);
                        admin.FullName = Convert.ToString(reader["FullName"]);
                        admin.Email = Convert.ToString(reader["Email"]);
                        admin.MobileNumber= Convert.ToString(reader["MobileNumber"]);
                        admin.Address = Convert.ToString(reader["Address"]);

                    }
                    this.sqlConnection.Close();
                    admin.Token = this.GetJWTToken(admin);
                    return admin;
                }
                else
                {
                    throw new Exception("Email or Password is Wrong");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetJWTToken(AdminLoginModel admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("This is My Key To Generate Token");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Email", admin.Email),
                    new Claim("AdminId", admin.AdminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}