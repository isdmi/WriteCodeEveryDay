using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WPFPractice3
{
    public class EmployeeRepository
    {
        private readonly string _connectionString = Properties.Settings.Default.ConnectionString;


        public async Task<Employee> GetEmployeeAsync(string id, string password)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand(
                "SELECT EmployeeId, EmployeeName FROM Employees WHERE EmployeeId=@id AND Password=@pass",
                conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@pass", password);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Employee
                {
                    EmployeeId = reader.GetString(0),
                    EmployeeName = reader.GetString(1)
                };
            }

            return null;
        }
    }
}
