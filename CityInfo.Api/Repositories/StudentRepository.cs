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
    public class StudentRepository : IStudentRepository
    {
        public bool Delete(int id)
        {
            return executeQuery($"delete from student where studentid = {id}") is object;
        }

        public ICollection<Student> GetAll()
        {
            var constring = @"Server=SEAKING; Database=BTSIS; Trusted_Connection=True;";
            SqlDataAdapter da = new SqlDataAdapter("select * from student", constring);
            var dt = new DataTable();
            da.Fill(dt);

            List<Student> lst = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Student() {
                    StudentId = Convert.ToInt16(row["StudentId"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    DOB = Convert.ToDateTime(row["DOB"]),
                    Telephone = row["Telephone"].ToString(),
                    AddressLine1 = row["AddressLine1"].ToString(),
                    AddressLine2 = row["AddressLine2"].ToString(),
                    Town = row["Town"].ToString(),
                    Postcode = row["Postcode"].ToString()
            });
            }

            return lst;
        }

        public Student GetById(int id)
        {
            var constring = @"Server=SEAKING; Database=BTSIS; Trusted_Connection=True;";
            SqlDataAdapter da = new SqlDataAdapter($"select * from student where studentid = {id}", constring);
            var dt = new DataTable();
            da.Fill(dt);

            Student student = new Student();
            foreach (DataRow row in dt.Rows)
            {
                student.StudentId = Convert.ToInt16(row["StudentId"]);
                student.FirstName = row["FirstName"].ToString();
                student.LastName = row["LastName"].ToString();
                student.DOB = Convert.ToDateTime(row["DOB"]);
                student.Telephone = row["Telephone"].ToString();
                student.AddressLine1 = row["AddressLine1"].ToString();
                student.AddressLine2 = row["AddressLine2"].ToString();
                student.Town = row["Town"].ToString();
                student.Postcode = row["Postcode"].ToString();
            }
            return student;
        }

        public Student insert(Student t)
        {
            string query = $"insert into student(FirstName,LastName,DOB,Telephone,AddressLine1,AddressLine2,Town,Postcode)";
            query += $"values('{t.FirstName}', '{t.LastName}', '{t.DOB.ToString("yyyy-MM-dd")}', '{t.Telephone}', '{t.AddressLine1}', '{t.AddressLine2}', '{t.Town}', '{t.Postcode}');";
            query += "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var id = executeQuery(query);
            return GetById(Convert.ToInt32(id));
        }

        public Student update(Student t)
        {
            string query = $"update student set ";
            query += $"FirstName = '{t.FirstName}', LastName='{t.LastName}', DOB='{t.DOB.ToString("yyyy-MM-dd")}', Telephone='{t.Telephone}', AddressLine1='{t.AddressLine1}', AddressLine2='{t.AddressLine2}', Town='{t.Town}', Postcode='{t.Postcode}' where studentid={t.StudentId};";
            executeQuery(query);
            return GetById(Convert.ToInt32(t.StudentId));
        }

        private object executeQuery(string query)
        {
            var constring = @"Server=SEAKING; Database=BTSIS; Trusted_Connection=True;";
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
