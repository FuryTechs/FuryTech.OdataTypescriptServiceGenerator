using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class EntityType
    {
        public EntityType(XElement sourceElement)
        {
            Name = sourceElement.Attribute("Name")?.Value;
            KeyName =
                sourceElement.Descendants()
                    .Where(a => a.Name.LocalName == "Key")
                    .Descendants()
                    .SingleOrDefault()?
                    .Attribute("Name")?
                    .Value;
            Namespace = sourceElement.Parent?.Attribute("Namespace")?.Value;

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
                    FullName = prop.Attribute("Name")?.Value,
                    IsCollection = prop.Attribute("Type")?.Value.StartsWith("Collection(") ?? false,
                    Type = prop.Attribute("Type")?.Value.TrimStart("Collection(".ToCharArray()).TrimEnd(')'),
                    ReferentialConstraint = prop.Descendants().SingleOrDefault(a => a.Name.LocalName == "ReferentialConstraint")?.Attribute("Property")?.Value,
                    ReferencedProperty = prop.Descendants().SingleOrDefault(a=>a.Name.LocalName == "ReferentialConstraint")?.Attribute("ReferencedProperty")?.Value
                }).ToList();
        }

        public string Namespace { get; private set; }
        public string Name { get; private set; }
        public string KeyName { get; set; }
        public List<Property> Properties { get; private set; }
        public List<NavigationProperty> NavigationProperties { get; set; }

        public IEnumerable<Uri> Imports
        {
            get
            {
                var namespaces = NavigationProperties.Select(a => a.Type).Where(a=>a != Namespace+"."+Name).Distinct();
                var uris = namespaces.Select(a => new Uri("//" + a.Replace(".", "/")));
                return uris;
            }
        }

        private Uri _uri;
        public Uri Uri => _uri ?? (_uri = new Uri("//"+Namespace.Replace(".", "/") + "/" + Name));
    }
}