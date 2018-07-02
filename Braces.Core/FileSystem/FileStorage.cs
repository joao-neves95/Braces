using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Braces.Core.FileSystem
{
    public class FileStorage
    {
        public static async Task<string> ReadFileAsStringAsync(string filePath)
        {
            byte[] buffer = await ReadFileAsync(filePath);
            return Encoding.UTF8.GetString(buffer);
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
                    return buffer;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occured: {e}");
                throw new ApplicationException($"An error has occured: {e}");
            }
        }

        public static async Task SaveFileAsync(string filePath, byte[] buffer)
        {
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await fileStream.WriteAsync(buffer, 0, buffer.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error has occured: {e}");
                throw new ApplicationException($"An error has occured: {e}");
            }
        }
    }
}
