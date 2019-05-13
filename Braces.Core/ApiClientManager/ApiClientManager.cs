using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;
using Braces.Core.DTOs;
using System.IO;

namespace Braces.Core.ApiClientManager
{
    public sealed class ApiClientManager
    {
        #region SINGLETON CONTRUCTOR

        static ApiClientManager() { }

        private ApiClientManager() { }

        private static readonly ApiClientManager _instance = new ApiClientManager();

        /// <summary>
        /// 
        /// Get the current singleton instance.
        /// 
        /// </summary>
        public static ApiClientManager _
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region PRIVATE PROPERTIES

        public const string PROTOCOL = "http";
        public const string PORT = "5000";

        private HttpClient _HttpClient { get; set; }

        #endregion

        #region PUBLIC METHODS

        public void StartApiServer()
        {
            // The path is hardcoded for now.
            string apiServerPath = Path.GetFullPath( "C:\\Users\\jpedrone\\DEV\\Braces\\Braces.ApiServer\\bin\\Debug\\netcoreapp3.0\\Braces.ApiServer.dll" );
            Process.Start( "dotnet", apiServerPath );
        }

        /// <summary>
        /// 
        /// Send a post request to the ApiServer WebAPI.
        /// 
        /// </summary>
        /// <param name="dto"> The Data Transfer Object (DTO) instance </param>
        /// <returns></returns>
        public async Task PostAsJSON( string apiPath, IDTO dto )
        {
            try
            {
                using ( _HttpClient = new HttpClient() )
                {
                    await _HttpClient.PostAsync(
                        $"{PROTOCOL}://localhost:{PORT}/api/{apiPath}",
                        new StringContent( dto.ToJSON(), Encoding.UTF8, "application/json" )
                    );
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex );
                _HttpClient.Dispose();
            }
        }

        #endregion
    }
}
