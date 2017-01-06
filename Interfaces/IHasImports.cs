using System;
using System.Collections.Generic;

namespace FuryTech.OdataTypescriptServiceGenerator.Interfaces
{
    public interface IHasImports : IHasUri
    {
        IEnumerable<Uri> Imports { get; }
    }
}
