﻿using Braces.Core.Models;

namespace Braces.UI.WPF.EventArguments
{
    public class FileEventArgs
    {
        public FileEventArgs( FileModel fileModel )
        {
            this.File = fileModel;
        }

        public FileModel File { get; set; }
    }
}