using System;
using System.Collections.Generic;
using System.Text;

namespace Braces.Core.Models
{
    public class ProjectModel : FileModel
    {
        public string ProjectName { get; set; }

        public string Authors { get; set; }

        public UserConfig Configuration { get; set; }

        public Dictionary<string, FileModel> Files { get; set; }
    }
}
