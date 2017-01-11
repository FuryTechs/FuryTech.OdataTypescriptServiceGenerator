using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Abstracts;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class EntityType : TypescriptModelClassAbstract{
        public EntityType(XElement sourceElement) : base(sourceElement)
        {
        }
    }
}