using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string _connectionString;
        private SqlDataAccess db = new SqlDataAccess();
        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Apartment> GetAllApartments()
        {
            string sql = "select * from dbo.Apartments";

            return db.LoadData<Apartment, dynamic>(sql, new { }, _connectionString);
        }

        public Apartment GetApartmentById(int id)
        {
            string sql = "select Id, Title, Price, Location, Time, Link from dbo.Apartments where Id = @Id";
            Apartment output = new Apartment();

            output = db.LoadData<Apartment, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault(); 
            if (output == null)
            {
                return null;
            }
            return output;
        }

        public void CreateApartment(Apartment apartment)
        {
            string sql = "select Link from dbo.Apartments where Link = @Link";
            Apartment compareLink = new Apartment();

            compareLink = db.LoadData<Apartment, dynamic>(sql, new { apartment.Link }, _connectionString).FirstOrDefault();
            if (compareLink == null)
            {
                sql = "insert into dbo.Apartments (Title, Price, Location, Time, Link) values (@Title, @Price, @Location, @Time, @Link);";
                db.SaveData(sql, new
                {
                    apartment.Title,
                    apartment.Price,
                    apartment.Location,
                    apartment.Time,
                    apartment.Link
                }, _connectionString);
            }
           
        }
    }
}
