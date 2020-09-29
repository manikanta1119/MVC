using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;


namespace MVCAdoDemo.Models
{
    public class EmployeeDataAccessLayer
        {
            string ConnectionString="Data Source=US5CD9011ZYR\\MSSQLMANI;Initial Catalog=Demo;Integrated Security=True;";
            public IEnumerable<Employee> GetAllEmployees()
            {
                List<Employee> lstemployee= new List<Employee>();
                using(SqlConnection connection = new SqlConnection(ConnectionString))
                {
                      SqlCommand command= new SqlCommand("spGetAllEmployees",connection);   
                      command.CommandType=System.Data.CommandType.StoredProcedure;

                      connection.Open();

                      SqlDataReader rdr= command.ExecuteReader();

                      while (rdr.Read())    
                {    
                    Employee employee = new Employee();    
    
                    employee.ID = Convert.ToInt32(rdr["EmployeeID"]);    
                    employee.Name = rdr["Name"].ToString();    
                    employee.Gender = rdr["Gender"].ToString();    
                    employee.Department = rdr["Department"].ToString();    
                    employee.City = rdr["City"].ToString();    
    
                    lstemployee.Add(employee);    
                }    
                connection.Close();

                }

                return lstemployee;
            }

            public void AddEmployee(Employee employee)    
            {    
                using (SqlConnection con = new SqlConnection(ConnectionString))    
                {    
                    SqlCommand cmd = new SqlCommand("spAddEmployee", con);    
                    cmd.CommandType = CommandType.StoredProcedure;    
    
                    cmd.Parameters.AddWithValue("@Name", employee.Name);    
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);    
                    cmd.Parameters.AddWithValue("@Department", employee.Department);    
                    cmd.Parameters.AddWithValue("@City", employee.City);    
    
                    con.Open();    
                    cmd.ExecuteNonQuery();    
                    con.Close();    
                }    
            } 

            public void UpdateEmployee(Employee employee)    
        {    
            using (SqlConnection con = new SqlConnection(ConnectionString))    
            {    
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);    
                cmd.CommandType = CommandType.StoredProcedure;    
    
                cmd.Parameters.AddWithValue("@EmpId", employee.ID);    
                cmd.Parameters.AddWithValue("@Name", employee.Name);    
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);    
                cmd.Parameters.AddWithValue("@Department", employee.Department);    
                cmd.Parameters.AddWithValue("@City", employee.City);    
    
                con.Open();    
                cmd.ExecuteNonQuery();    
                con.Close();    
            }    
        }    
    
        //Get the details of a particular employee    
        public Employee GetEmployeeData(int? id)    
        {    
            Employee employee = new Employee();    
    
            using (SqlConnection con = new SqlConnection(ConnectionString))    
            {    
                string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;    
                SqlCommand cmd = new SqlCommand(sqlQuery, con);    
    
                con.Open();    
                SqlDataReader rdr = cmd.ExecuteReader();    
    
                while (rdr.Read())    
                {    
                    employee.ID = Convert.ToInt32(rdr["EmployeeID"]);    
                    employee.Name = rdr["Name"].ToString();    
                    employee.Gender = rdr["Gender"].ToString();    
                    employee.Department = rdr["Department"].ToString();    
                    employee.City = rdr["City"].ToString();    
                }    
            }    
            return employee;    
        }    
    
        //To Delete the record on a particular employee    
        public void DeleteEmployee(int? id)    
        {    
    
            using (SqlConnection con = new SqlConnection(ConnectionString))    
            {    
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);    
                cmd.CommandType = CommandType.StoredProcedure;    
    
                cmd.Parameters.AddWithValue("@EmpId", id);    
    
                con.Open();    
                cmd.ExecuteNonQuery();    
                con.Close();    
            }    
        }       
        
        }
    
}