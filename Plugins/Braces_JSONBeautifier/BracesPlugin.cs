using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Braces.Core.Enums;
using Braces.Core.ExtensionSystem;
using Braces.UI.WPF.EventArguments;

namespace Braces_JSONBeautifier
{
    public class BracesPlugin : Plugin
    {
        #region INTERFACE PROPERTIES

        public override List<string> FileTypes => new List<string>() { FileTypeName.JSON };

        public override string Name => "JSONBeautifier";

        public override string Description => "A beautifier for JSON";

        public override string[] Authors => new string[] { "shivayl" };

        public override string Version => "0.0.0.1";

        public override ExtensionType ExtensionType => ExtensionType.Plugin;

        #endregion

        // TODO: Add the commands to the Braces API.
        public override void Execute()
        {
        }

        public override async Task OnFileSave(object args)
        {
            Console.WriteLine( Braces.PluginHost.Program.Connection );
            await Braces.PluginHost.Program.Connection.InvokeCoreAsync( APIMethods.GetAllText, typeof( Task ), new object[] { this.FileTypes[0] } );
        }

        public override async Task OnReceiveAllText(string allText)
        {
            try
            {
                string formatedJson = JValue.Parse( allText ).ToString( Formatting.Indented );
                Console.WriteLine( Braces.PluginHost.Program.Connection );
                await Braces.PluginHost.Program.Connection.InvokeCoreAsync( APIMethods.SetAllText, typeof( Task ), new object[] { formatedJson } );

            } catch (Exception e)
            {
                // If there is any error resend the user input again.
                Console.WriteLine( $"Exception on the { this.Name } plugin." );
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );

                await Braces.PluginHost.Program.Connection.InvokeCoreAsync( APIMethods.SetAllText, typeof( Task ), new object[] { allText } );
            }
        }

    }
}
