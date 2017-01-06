using System;

namespace FuryTech.OdataTypescriptServiceGenerator.Interfaces
{
    public interface IHasUri : IRenderableElement
    {
        Uri Uri { get; }
    }
}
