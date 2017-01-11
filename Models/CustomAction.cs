using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Abstracts;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class CustomAction : CustomEventAbstract
    {
        public CustomAction(XElement xElement) : base(xElement)
        {

        }
    }
}
