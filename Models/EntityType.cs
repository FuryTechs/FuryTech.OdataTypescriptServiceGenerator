using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class EntityType
    {
        public EntityType(XElement sourceElement)
        {
            Name = sourceElement.Attribute("Name").Value;
            KeyName =
                sourceElement.Descendants()
                    .Where(a => a.Name.LocalName == "Key")
                    .Descendants()
                    .SingleOrDefault()
                    .Attribute("Name")
                    .Value;
            Namespace = sourceElement.Parent.Attribute("Namespace").Value;

            Properties = sourceElement.Descendants().Where(a => a.Name.LocalName == "Property")
                .Select(propElement => new Property()
                {
                    Name = propElement.Attribute("Name")?.Value,
                    IsRequired = propElement.Attribute("Nullable")?.Value == "true",
                    Type = propElement.Attribute("Type")?.Value
                }).ToList();

            NavigationProperties = sourceElement.Descendants().Where(a => a.Name.LocalName == "NavigationProperty")
                .Select(prop => new NavigationProperty()
                {
                    Name = prop.Attribute("Name")?.Value,
                    IsCollection = prop.Attribute("Type")?.Value.StartsWith("Collection(") ?? false,
                    ReferencedEntityTypeName = prop.Attribute("Type")?.Value,
                    ReferentialConstraint = prop.Descendants().SingleOrDefault(a => a.Name.LocalName == "ReferentialConstraint")?.Attribute("Property")?.Value,
                    ReferencedProperty = prop.Descendants().SingleOrDefault(a=>a.Name.LocalName == "ReferentialConstraint")?.Attribute("ReferencedProperty")?.Value
                }).ToList();
        }

        public string Namespace { get; set; }
        public string Name { get; set; }
        public string KeyName { get; set; }
        private List<Property> Properties { get; set; }

        private List<NavigationProperty> NavigationProperties { get; set; }

    }
}