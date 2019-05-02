using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Braces.ApiServer.Hubs;
using Braces.Core.ExtensionSystem;
using Braces.Core.DTOs;
using Braces.Core;
using Braces.ApiServer.Interfaces;

namespace Braces.ApiServer.Controllers
{
    [ApiController]
    [Route("api/text-editor")]
    public class TextEditorController : ControllerBase
    {
        private readonly IHubContext<TextEditorHub> _hubContext;

        public TextEditorController( IHubContext<TextEditorHub> hubContext )
        {
            this._hubContext = hubContext;
        }

#pragma warning disable SG0016 // Controller method is vulnerable to CSRF

        [HttpPost("fire-event")]
        public async Task FireEvent([FromBody] FireEventDTO fireEventDTO )
        {
            await this._hubContext.Clients.All.SendAsync( fireEventDTO.eventName, fireEventDTO.fileTypeName, fireEventDTO.args );
        }

#pragma warning restore SG0016 // Controller method is vulnerable to CSRF
    }
}