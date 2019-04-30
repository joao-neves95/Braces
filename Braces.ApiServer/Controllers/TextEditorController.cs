using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Braces.ApiServer.Hubs;
using Braces.Core.ExtensionSystem;
using Braces.Core.DTOs;

namespace Braces.ApiServer.Controllers
{
    [ApiController]
    [Route("api/text-editor")]
    public class TextEditorController : ControllerBase
    {
        private readonly IHubContext<TextEditorHub, Plugin> _hubContext;

        public TextEditorController( IHubContext<TextEditorHub, Plugin> hubContext )
        {
            this._hubContext = hubContext;
        }

        [HttpPost("fire-event")]
        public async Task FireEvent([FromBody] FireEventDTO fireEventDTO )
        {
            await Handlers.FireEvent<TextEditorHub, Plugin>(
                _hubContext,
                fireEventDTO.fileTypeName,
                fireEventDTO.eventName,
                fireEventDTO.sender,
                fireEventDTO.e,
                fireEventDTO.args
            );
        }
    }
}