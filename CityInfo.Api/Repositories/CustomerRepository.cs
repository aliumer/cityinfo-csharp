using CityInfo.Api.Models;
using CityInfo.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private const string constring = @"Server=SEAKING; Database=POS; Trusted_Connection=True;";

        public bool Delete(int id)
        {
            return executeQuery($"delete from Customer where Id = {id}") is object;
        }

        public ICollection<Customer> GetAll()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Customer", constring);
            var dt = new DataTable();
            da.Fill(dt);

            List<Customer> lst = new List<Customer>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Customer()
                {
                    Id = Convert.ToInt16(row["Id"]),
                    FirstName = row["FirstName"].ToString(),
                    SurName = row["SurName"].ToString(),
                    Phone = row["phone"].ToString(),
                    Email = row["email"].ToString(),
                    Address = row["Address"].ToString(),
                    Town = row["town"].ToString(),
                    Postcode = row["Postcode"].ToString()
                });
            }

            return lst;
        }

        public Customer GetById(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from Customer where id = {id}", constring);
            var dt = new DataTable();
            da.Fill(dt);

            Customer customer = new Customer();
            foreach (DataRow row in dt.Rows)
            {
                customer.Id = Convert.ToInt16(row["Id"]);
                customer.FirstName = row["FirstName"].ToString();
                customer.SurName = row["SurName"].ToString();
                customer.Phone = row["phone"].ToString();
                customer.Email = row["email"].ToString();
                customer.Address = row["Address"].ToString();
                customer.Town = row["town"].ToString();
                customer.Postcode = row["Postcode"].ToString();
            }
            return customer;
        }

        public Customer insert(Customer t)
        {
            string query = $"insert into Customer(FirstName,SurName,email,phone, Address,Town,Postcode)";
            query += $"values('{t.FirstName}', '{t.SurName}', '{t.Email}', '{t.Phone}', '{t.Address}', '{t.Town}', '{t.Postcode}');";
            query += "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var id = executeQuery(query);
            return GetById(Convert.ToInt32(id));
        }

        public Customer update(Customer t)
        {
            string query = $"update Customer set ";
            query += $"FirstName = '{t.FirstName}', surName='{t.SurName}', phone='{t.Phone}', email='{t.Email}', Address='{t.Address}', town='{t.Town}', Postcode='{t.Postcode}' where Employeeid = {t.Id};";
            executeQuery(query);
            return GetById(Convert.ToInt32(t.Id));
        }

        private object executeQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.CommandText = query;
                    var identity = cmd.ExecuteScalar();
                    cmd.Connection.Close();
                    return identity;
                }
            }

        }
    }
}
