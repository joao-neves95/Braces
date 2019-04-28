using System;
using System.Collections.Generic;
using System.Text;
using Braces.Core.Enums;

namespace Braces.Core.ExtensionSystem
{
    public abstract class ComponentPlugin : Plugin
    {
        #region INTERFACE CONTENTS

        public override List<string> FileTypes => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string[] Authors => throw new NotImplementedException();

        public override string Version => throw new NotImplementedException();

        public override ExtensionType ExtensionType => throw new NotImplementedException();

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
