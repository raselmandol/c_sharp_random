using System;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

public class ScrapeData
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public class DataContext : DbContext
{
    public DbSet<ScrapeData> ScrapeData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

public class Program
{
    public static void Main()
    {
        var web = new HtmlWeb();
        var doc = web.Load("https://www.example.com");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='item']");

        var scrapeDataList = nodes.Select(node => new ScrapeData
        {
            Title = node.SelectSingleNode(".//h2").InnerText.Trim(),
            Description = node.SelectSingleNode(".//p").InnerText.Trim()
        }).ToList();

        // Step 6: Save the scraped data to the database
        using (var context = new DataContext())
        {
            context.ScrapeData.AddRange(scrapeDataList);
            context.SaveChanges();
        }

        Console.WriteLine("Scraped data has been saved to the database.");
    }
}
