using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

public class UrlData
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string ParentUrl { get; set; }
    public int Depth { get; set; }
}

public class DataContext : DbContext
{
    public DbSet<UrlData> Urls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

public class Program
{
    public static void Main()
    {
        string seedUrl = "https://example.com";
        int maxDepth = 5;
        
        Queue<string> urlsToVisit = new Queue<string>();
        HashSet<string> visitedUrls = new HashSet<string>();

        urlsToVisit.Enqueue(seedUrl);

        while (urlsToVisit.Count > 0)
        {
            string currentUrl = urlsToVisit.Dequeue();

            if (!visitedUrls.Contains(currentUrl))
            {
                visitedUrls.Add(currentUrl);

                var web = new HtmlWeb();
                var doc = web.Load(currentUrl);

                var links = doc.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        string url = link.GetAttributeValue("href", "");

                        if (!string.IsNullOrEmpty(url) && url.StartsWith(seedUrl))
                        {
                            urlsToVisit.Enqueue(url);

                            using (var context = new DataContext())
                            {
                                var urlData = new UrlData
                                {
                                    Url = url,
                                    ParentUrl = currentUrl,
                                    Depth = GetDepth(currentUrl, seedUrl)
                                };

                                context.Urls.Add(urlData);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("URLs have been scraped and stored in the database.");
    }

    private static int GetDepth(string url, string seedUrl)
    {
        int depth = 0;

        while (!url.Equals(seedUrl, StringComparison.OrdinalIgnoreCase))
        {
            depth++;
            url = new Uri(url).Segments[^1];
        }

        return depth;
    }
}
