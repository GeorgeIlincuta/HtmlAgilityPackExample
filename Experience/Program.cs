using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Experience
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.olx.ro/imobiliare/apartamente-garsoniere-de-vanzare/bucuresti/";
            var web = new HtmlWeb();
            var doc = web.Load(url);            
            HtmlNodeCollection parentId = doc.DocumentNode.SelectNodes("//*[@id='offers_table']//*[@class='wrap']");
            
            var home = new List<Apartament>();

            foreach (var item in parentId)
            {
                var title    = item.SelectSingleNode(".//*[@class='lheight22 margintop5']").InnerText.Trim();
                var price    = item.SelectSingleNode(".//*[@class='price']").InnerText.Trim();
                var location = item.SelectSingleNode(".//*[@class='lheight16']/small[1]/span/text()").InnerText.Trim();
                var time     = item.SelectSingleNode(".//*[@class='lheight16']/small[2]/span/text()").InnerText.Trim();

                if (time.Contains("Azi"))
                {
                    time = DateTime.Today.ToShortDateString();
                }
                else if (time.Contains("Ieri"))
                {
                    time = DateTime.Today.AddDays(-1).ToShortDateString();
                }

                home.Add(new Apartament()
                {
                    Title    = title,
                    Price    = price,
                    Location = location,
                    Time     = time
                });
              
            }
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
