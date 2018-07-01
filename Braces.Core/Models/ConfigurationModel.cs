using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Braces.Core.Models
{
    public class ConfigurationModel
    {

        private string modifier;

        public string Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

        private string saveKey;

        public string SaveKey
        {
            get { return saveKey; }
            set { saveKey = value; }
        }
    }
}
