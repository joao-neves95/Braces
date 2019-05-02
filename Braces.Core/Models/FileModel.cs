using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Braces.Core.Enums;

namespace Braces.Core.Models
{
    public class FileModel : FileModelBase
    {
        public FileModel( string completePath )
        {
            this.CompletePath = completePath;
            this.Name = Path.GetFileName( completePath );
            this.Extension = Path.GetExtension( completePath );
            this.CreationDateTime = File.GetCreationTime( completePath );
        }
    }
}
