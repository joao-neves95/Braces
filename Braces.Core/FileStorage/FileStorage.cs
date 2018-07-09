using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Braces.Core
{
    public static class FileStorage
    {
        public static string GetHOMEPATH()
        {
            return Environment.GetEnvironmentVariable("HOMEPATH");
        }

        public static string GetUserProfilePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        public static async Task<string> ReadFileAsStringAsync(string filePath)
        {
            byte[] buffer = await ReadFileAsync(filePath);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            //return new UnicodeEncoding().GetString(buffer, 0, buffer.Length);
        }

        public static async Task<byte[]> ReadFileAsync(string filePath)
        {
            try
            {
                byte[] buffer;

                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fileStream.Length];
                    await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                    fileStream.Close();
                    fileStream.Dispose();
                    return buffer;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occured: {e}");
                // throw new ApplicationException($"An error has occured: {e}");
                return new byte[0];
            }
        }

        public static async Task SaveFileAsync(string filePath, string content)
        {
            int bufferSize = content.Length;
            byte[] buffer = Encoding.UTF8.GetBytes(content.ToCharArray(0, bufferSize), 0, bufferSize);
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await fileStream.WriteAsync(buffer, 0, buffer.Length);
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occured: {e}");
                // throw new ApplicationException($"An error has occured: {e}");
            }
        }

        public static async Task ReadDirAsync(string directoryPath)
        {

        }
    }
}
