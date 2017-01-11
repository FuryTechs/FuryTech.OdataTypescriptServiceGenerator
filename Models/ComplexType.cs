using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Abstracts;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class ComplexType : TypescriptModelClassAbstract
    {
        public ComplexType(XElement sourceElement) : base(sourceElement)
        {
        }
    }
}
