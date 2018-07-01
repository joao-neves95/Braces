using System;
using System.Collections.Generic;
using System.Text;
using Braces.Core.Models;

namespace Braces.Core
{
    class Configuration
    {
        private ConfigurationModel GetDefaultConfiguration()
        {
            ConfigurationModel defaultConfiguration = new ConfigurationModel() {
                Modifier = "Control",
                SaveKey = "S"
            };

            return defaultConfiguration; 
        }
    }
}
