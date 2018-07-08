using System;
using System.Collections.Generic;
using System.Text;

namespace Braces.Core.Models
{
    public class ProjectModel : FileModel
    {
        #region PROPERTIES

        public string ProjectName { get; set; }

        public string Authors { get; set; }

        public UserConfig Configuration { get; set; }

        public Dictionary<string, FileModel> FolderStructure { get; set; }

        public Dictionary<string, FileModel> Files { get; set; }
        
        #endregion

        public ProjectModel(string completePath) : base(completePath)
        {
        }
    }
}
