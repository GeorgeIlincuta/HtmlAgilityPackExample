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
                var title    = item.SelectNodes("//*[@class='lheight22 margintop5']");
                var price    = item.SelectNodes("//*[@class='price']");
                var location = item.SelectNodes("//*[@class='lheight16']/small[1]/span/text()");
                var time     = item.SelectNodes("//*[@class='lheight16']/small[2]/span/text()");

                for (var i = 0; i <= parentId.Count - 1; i++)
                {
                    home.Add(new Apartament()
                    {
                        Title    = title[i].InnerText.Trim(),
                        Price    = price[i].InnerText.Trim(),
                        Location = location[i].InnerText.Trim(),
                        Time     = time[i].InnerText.Trim()
                    });
                }
                
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
