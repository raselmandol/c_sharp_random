//EPPlus is a popular open-source library for creating and manipulating Excel files in C#. 
//You can install it via NuGet.
//Make sure to install this NuGet package
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using OfficeOpenXml;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://www.example.com"; //Replace with the URL of the website you want to crawl
        string filePath = "output.xlsx"; //Replace with the desired file path

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

                //Save the response to an Excel file
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].Value = responseBody;

                    package.SaveAs(new FileInfo(filePath));
                }

                Console.WriteLine("Data saved to Excel file: " + filePath);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
