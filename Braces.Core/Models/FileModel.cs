using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Braces.Core.Models
{
    public class FileModel
    {
        public string CompletePath { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public DateTime CreationDateTime { get; set; }

        public FileModel(string completePath)
        {
            this.CompletePath = completePath;
            this.Name = Path.GetFileName(completePath);
            this.Extension = Path.GetExtension(completePath);
            this.CreationDateTime = File.GetCreationTime(completePath);
        }
    }
}
