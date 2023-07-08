using System;
using System.IO;

class Program
{
    static void Main()
    {
        //Get the current directory
        string currentDir = Directory.GetCurrentDirectory();

        //Prompt the user for the file extension to search for
        Console.WriteLine("Enter the file extension to search for (e.g., txt, jpg):");
        string fileExtension = Console.ReadLine();

        //Search for files with the given extension
        SearchFiles(currentDir, fileExtension);

        Console.WriteLine("Search complete.");
    }

    static void SearchFiles(string directory, string fileExtension)
    {
        try
        {
            //Search for files with the given extension in the current directory
            foreach (string filePath in Directory.GetFiles(directory, $"*.{fileExtension}"))
            {
                Console.WriteLine(filePath);
            }

            //Recursively search for files in subdirectories
            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                SearchFiles(subdirectory, fileExtension);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
