using System.Xml.Linq;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class Container
    {
        public Container(XElement xElement)
        {
            Name = xElement.Attribute("Name")?.Value;
            EntityType = xElement.Attribute("EntityType")?.Value;
        }

        public string Name { get; set; }
        public string EntityType { get; set; }
    }
}
