﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace EmployeeService
{
    // NOT: "EmployeeService" sınıf adını kodda ve yapılandırma dosyasında birlikte değiştirmek için "Yeniden Düzenle" menüsündeki "Yeniden Adlandır" komutunu kullanabilirsiniz.
    public class EmployeeService : IEmployeeService
    {
        
        public Employee GetEmployee(int Id)
        {
            Employee employee = new Employee();
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("upGetEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter();
                parameterId.ParameterName = "@Id";
                parameterId.Value = Id;
                cmd.Parameters.Add(parameterId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.Name = reader["Name"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                }
            }
            return employee;
        }

        public void SaveEmployee(Employee employee)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("upSaveEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameterId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = employee.Id
                };
                cmd.Parameters.Add(parameterId);

                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = employee.Name
                };
                cmd.Parameters.Add(parameterName);

                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = employee.Gender
                };
                cmd.Parameters.Add(parameterGender);

                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = employee.DateOfBirth
                };
                cmd.Parameters.Add(parameterDateOfBirth);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
