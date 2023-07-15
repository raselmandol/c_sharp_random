using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://www.example.com"; //Replace with the URL of the website you want to crawl

        using (HttpClient client = new HttpClient())
        {
            try
            {
                //Send a GET request to the specified URL
                HttpResponseMessage response = await client.GetAsync(url);

                //Ensure the request was successful
                response.EnsureSuccessStatusCode();

                //Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                //Print the response to the console
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
