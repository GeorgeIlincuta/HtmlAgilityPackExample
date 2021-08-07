using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Experience
{
    class Olx
    {
        public List<Apartment> GetApartments(string locationParam)
        {
            var home = new List<Apartment>();
            var url  = $"https://www.olx.ro/imobiliare/apartamente-garsoniere-de-vanzare/{ locationParam }/";
            var web  = new HtmlWeb();

            HtmlNode nextButton = null;

            do
            {
                var doc = web.Load(url);
                HtmlNodeCollection parentId = doc.DocumentNode.SelectNodes("//*[@id='offers_table']//*[@class='wrap']");

                foreach (var item in parentId)
                {
                    var title       = item.SelectSingleNode(".//*[@class='lheight22 margintop5']").InnerText.Trim();
                    var price       = item.SelectSingleNode(".//*[@class='price']").InnerText.Trim();
                    var priceFormat = Regex.Replace(price, @"[^\u0000-\u007F]", string.Empty);
                    var location    = item.SelectSingleNode(".//*[@class='lheight16']/small[1]/span/text()").InnerText.Trim();
                    var time        = item.SelectSingleNode(".//*[@class='lheight16']/small[2]/span/text()").InnerText.Trim();
                    var link        = item.SelectSingleNode(".//*[@class='photo-cell']//a").GetAttributeValue("href", String.Empty);

                    if (time.Contains("Azi"))
                    {
                        time = DateTime.Today.ToShortDateString();
                    }
                    else if (time.Contains("Ieri"))
                    {
                        time = DateTime.Today.AddDays(-1).ToShortDateString();
                    }

                    home.Add(new Apartment()
                    {
                        Title    = title,
                        Price    = priceFormat,
                        Location = location,
                        Time     = time,
                        Link     = link
                    });

                    nextButton = doc.DocumentNode.SelectSingleNode("//*[@class='fbold next abs large']/a");
                    url        = nextButton?.GetAttributeValue<string>("href", null);
                }
            } while (nextButton != null);

            return home;
        } 
    }
}
