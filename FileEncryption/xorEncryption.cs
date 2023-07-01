using System;
using System.IO;

namespace FileEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = "C:\\Path\\To\\Directory"; //Replace with the directory path containing the files to encrypt

            EncryptFilesInDirectory(directoryPath);

            Console.WriteLine("Encryption complete.");
            Console.ReadLine();
        }

        static void EncryptFilesInDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*.*");

            foreach (string filePath in files)
            {
                if (Path.GetExtension(filePath) != ".buggy")
                {
                    string encryptedFilePath = Path.ChangeExtension(filePath, ".buggy");
                    EncryptFile(filePath, encryptedFilePath);
                }
            }
        }

        static void EncryptFile(string inputFilePath, string outputFilePath)
        {
            byte[] key = { 0xAB, 0xCD, 0xEF }; //Encryption key

            using (FileStream inputFileStream = File.OpenRead(inputFilePath))
            using (FileStream outputFileStream = File.Create(outputFilePath))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        //Perform XOR encryption on each byte with the encryption key
                        buffer[i] ^= key[i % key.Length];
                    }

                    outputFileStream.Write(buffer, 0, bytesRead);
                }
            }

            //Delete the original file after encryption, if desired
            //File.Delete(inputFilePath);
        }
    }
}
