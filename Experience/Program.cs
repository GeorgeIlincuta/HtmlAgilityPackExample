using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace Experience
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());

            var olxScraper = new Olx();
            var home = olxScraper.GetApartments("bucuresti");

            foreach (var item in home)
            {
                CreateNewApartment(sql, item);
            }
            ReadAllApartments(sql);

        }
        private static void CreateNewApartment(SqlCrud sql, Apartment apartment)
        {
            sql.CreateApartment(apartment);
        }
        private static void ReadAllApartments(SqlCrud sql)
        {
            var rows = sql.GetAllApartments();
            foreach(var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.Title} || { row.Price } || { row.Location } || { row.Time }");
            }
        }
        private static void ReadApartment(SqlCrud sql, int id)
        {
            var contact = sql.GetApartmentById(id);

            Console.WriteLine($"{ contact.Id }: { contact.Title } { contact.Price } { contact.Location }");
        }
        private static void UpdateApartment(SqlCrud sql, Apartment apartment)
        {
            Apartment updateApartment = new Apartment()
            {
                Id = apartment.Id,
                Title = apartment.Title,
                Price = apartment.Price,
                Location = apartment.Location,
                Time = apartment.Time,
                Link = apartment.Link
            };
            sql.UpdateApartment(updateApartment);
        }
        private static void DeleteApartment(SqlCrud sql, int id)
        {
            sql.RemoveApartment(id);
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }

    }
}
