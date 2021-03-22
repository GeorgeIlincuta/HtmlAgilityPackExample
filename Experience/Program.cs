using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Experience
{
    class Program
    {
        static void Main(string[] args)
        {
            var olxScraper = new Olx();
            var home = olxScraper.GetApartments("bucuresti");

            foreach (var item in home)
            {
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Price);
                Console.WriteLine(item.Location);
                Console.WriteLine(item.Time);
                Console.WriteLine("-----------------------------------------");
            }
            Console.WriteLine(home.Count);
        }
    }
}
