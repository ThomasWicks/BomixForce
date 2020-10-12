using Bomix_Force.Data.Context;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo
{
    public class NonconformityRepository : GenericRepository<Nonconformity>, INonconformityRepository
    {
        private ModelContext _context;
        private readonly IConfiguration _configuration;


        public NonconformityRepository(ModelContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;

        }

        public string GetSellerEmail(string CNPJ)
        {
            string Email = String.Empty;
            string queryString = String.Format("Select Bomix_Cliente.Vendedor_FK, Employee_Seller.SellerId, Employee.Email " +
                "FROM(Bomix_Cliente " +
                "INNER JOIN Employee_Seller ON Bomix_Cliente.Vendedor_FK = Employee_Seller.SellerId) " +
                "INNER JOIN Employee ON Employee_Seller.EmployeeId = Employee.Id " +
                "where Bomix_Cliente.CNPJ = '{0}'", CNPJ);

            string connectionString = _configuration.GetConnectionString("Mysqlconnection");
            //string connectionString = "Server=192.168.254.75;Database=Cimatec;Trusted_Connection=False;MultipleActiveResultSets=true;UID=cimatec;PWD=Cim@TEK@1996;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Email = reader.GetString(2);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return Email;
        }
    }
}
