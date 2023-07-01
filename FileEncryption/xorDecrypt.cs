using System;
using System.IO;

namespace FileEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = "C:\\Path\\To\\Directory";  //Replace with the directory path containing the encrypted files

            DecryptFilesInDirectory(directoryPath);

            Console.WriteLine("Decryption complete.");
            Console.ReadLine();
        }

        static void DecryptFilesInDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*.buggy");

            foreach (string filePath in files)
            {
                string decryptedFilePath = Path.ChangeExtension(filePath, null);
                DecryptFile(filePath, decryptedFilePath);
            }
        }

        static void DecryptFile(string inputFilePath, string outputFilePath)
        {
            byte[] key = { 0xAB, 0xCD, 0xEF };  //Encryption key (same as used for encryption)

            using (FileStream inputFileStream = File.OpenRead(inputFilePath))
            using (FileStream outputFileStream = File.Create(outputFilePath))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        //Perform XOR decryption on each byte with the encryption key
                        buffer[i] ^= key[i % key.Length];
                    }

                    outputFileStream.Write(buffer, 0, bytesRead);
                }
            }

            //Delete the encrypted file after decryption, if desired
            // File.Delete(inputFilePath);
        }
    }
}
