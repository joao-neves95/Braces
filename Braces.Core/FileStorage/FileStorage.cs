using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Braces.Core
{
    public static class FileStorage
    {
        public static string GetHOMEPATH()
        {
            return GetCurrentOperatingSystem() == OSPlatform.Windows ? Environment.GetEnvironmentVariable( "HOMEDRIVE" ) + Environment.GetEnvironmentVariable( "HOMEPATH" ) :
                                                                       Environment.GetEnvironmentVariable( "$HOME" );
        }

        public static OSPlatform GetCurrentOperatingSystem()
        {
            return RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ? OSPlatform.Windows :
                   RuntimeInformation.IsOSPlatform( OSPlatform.Linux ) ? OSPlatform.Linux :
                   RuntimeInformation.IsOSPlatform( OSPlatform.OSX ) ? OSPlatform.OSX :
                   throw new NotSupportedException( "Unrecognized OSPlatform." );
        }

        public static string GetUserProfilePath()
        {
            return Environment.GetFolderPath( Environment.SpecialFolder.UserProfile );
        }

        public static async Task<string> ReadFileAsStringAsync(string filePath)
        {
            byte[] buffer = await ReadFileAsync( filePath );
            return Encoding.UTF8.GetString( buffer, 0, buffer.Length );
            //return new UnicodeEncoding().GetString(buffer, 0, buffer.Length);
        }

        private static object readLock = new object();

        public static async Task<byte[]> ReadFileAsync(string filePath)
        {
            lock(readLock)
            {
                try
                {
                    byte[] buffer;

                    using (FileStream fileStream = File.Open( filePath, FileMode.Open, FileAccess.Read ))
                    {
                        buffer = new byte[fileStream.Length];
                        fileStream.ReadAsync( buffer, 0, (int)fileStream.Length ).GetAwaiter().GetResult();
                        fileStream.Close();
                        fileStream.Dispose();
                        return buffer;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine( $"An error has occured: {e}" );
                    // throw new ApplicationException($"An error has occured: {e}");
                    return new byte[0];
                }
            }
        }

        private static object _saveLock = new object();

        public static async Task SaveFileAsync(string filePath, string content)
        {
            lock (_saveLock)
            {
                int bufferSize = content.Length;
                byte[] buffer = Encoding.UTF8.GetBytes(content.ToCharArray(0, bufferSize), 0, bufferSize);
                try
                {
                    using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
                    {
                        fileStream.Seek(0, SeekOrigin.Begin);
                        fileStream.WriteAsync( buffer, 0, buffer.Length ).GetAwaiter().GetResult();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error has occured: {e}");
                }
            }
        }

        public static async Task ReadDirAsync(string directoryPath)
        {

        }
    }
}
