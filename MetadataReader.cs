using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Models;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    public class MetadataReader
    {
        public List<EntityType> EntityTypes { get; private set; }
        public List<Container> ContainerList { get; private set; }

        public IEnumerable<string> NameSpaces
        {
            get { return EntityTypes.Select(a => a.Namespace).Distinct(); }
        }

        private void ReadEntityTypes(XDocument xdoc)
        {
            Logger.Log("Parsing entity types...");
            var typeList = new List<EntityType>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EntityType");

            foreach (var xElement in elements)
            {
                var enT = new EntityType(xElement);
                typeList.Add(enT);
                Logger.Log($"Entity Type '{enT.Namespace}.{enT.Name}' parsed");
            }
            EntityTypes = typeList;
        }

        private void ReadContainers(XDocument xdoc)
        {
            Logger.Log("Parsing containers...");
            var containerList = new List<Container>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EntitySet");

            foreach (var xElement in elements)
            {
                var tContainer = new Container(xElement);
                containerList.Add(tContainer);
                Logger.Log($"Container '{tContainer.Name}' parsed");
            }

            ContainerList = containerList;
        }

        public MetadataReader(XDocument xdoc)
        {
            ReadEntityTypes(xdoc);
            ReadContainers(xdoc);
        }
    }
}
