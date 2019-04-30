﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;
using Braces.Core.DTOs;

namespace Braces.Core.ApiClientManager
{
    public sealed class ApiClientManager
    {
        #region SINGLETON CONTRUCTOR

        static ApiClientManager() { }

        private ApiClientManager() { }

        private static readonly ApiClientManager _instance = new ApiClientManager();

        public static ApiClientManager Instance
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
            Process.Start( "dotnet", "C:\\Users\\jpedrone\\DEV\\Braces\\Braces.ApiServer\\bin\\Debug\\netcoreapp2.2\\Braces.ApiServer.dll" );
        }

        /// <summary>
        /// 
        /// Send a post request to the ApiServer WebAPI.
        /// 
        /// </summary>
        /// <param name="dto"> The Data Transfer Object (DTO) instance </param>
        /// <returns></returns>
        public async Task Post( string apiPath, IDTO dto )
        {
            try
            {
                using ( _HttpClient = new HttpClient() )
                {
                    await _HttpClient.PostAsync(
                        $"{PROTOCOL}://localhost:{PORT}/api/{apiPath}",
                        new StringContent( dto.ToJSON() )
                    );
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex );
            }
            finally
            {
                _HttpClient.Dispose();
            }
        }

        #endregion
    }
}
