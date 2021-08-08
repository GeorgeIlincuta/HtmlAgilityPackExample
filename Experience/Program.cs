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
                sql.CreateApartment(item);
            }

            ReadAllApartments(sql);

        }
        private static void ReadAllApartments(SqlCrud sql)
        {
            var rows = sql.GetAllApartments();
            foreach(var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.Title} || { row.Price } || { row.Location } || { row.Time }");
            }
        }

        private static void ReadApartment(SqlCrud sql, int contactId)
        {
            var contact = sql.GetApartmentById(contactId);

            Console.WriteLine($"{ contact.Id }: { contact.Title } { contact.Price } { contact.Location }");
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
