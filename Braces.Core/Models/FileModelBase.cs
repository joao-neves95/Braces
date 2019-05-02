using System;
using System.Collections.Generic;
using System.Text;

namespace Braces.Core.Models
{
    // This class exists for JSON serialization.
    public class FileModelBase
    {
        public string CompletePath { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
