using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Models;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    public class MetadataReader
    {
        public List<EntityType> EntityTypes { get; private set; }
        public List<EnumType> EnumTypes { get; private set; }
        public List<EntitySet> EntitySets { get; private set; }

        private void ReadEntityTypes(XDocument xdoc)
        {
            Logger.Log("Parsing entity types...");
            var typeList = new List<EntityType>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EntityType");

            foreach (var xElement in elements)
            {
                var enT = new EntityType(xElement);
                typeList.Add(enT);
                Logger.Log($"Entity Type '{enT.NameSpace}.{enT.Name}' parsed");
            }
            EntityTypes = typeList;
        }

        private void ReadEnums(XDocument xdoc)
        {
            Logger.Log("Parsing enums...");
            var enumList = new List<EnumType>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EnumType");

            foreach (var xElement in elements)
            {
                var enT = new EnumType(xElement);
                enumList.Add(enT);
                Logger.Log($"Enum Type  '{enT.NameSpace}.{enT.Name}' parsed");
            }
            EnumTypes = enumList;

        }

        private void ReadContainers(XDocument xdoc)
        {
            Logger.Log("Parsing entity sets...");
            var containerList = new List<EntitySet>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EntitySet");

            foreach (var xElement in elements)
            {
                var tContainer = new EntitySet(xElement);
                containerList.Add(tContainer);
                Logger.Log($"Entity set '{tContainer.Name}' parsed");
            }

            EntitySets = containerList;
        }

        public MetadataReader(XDocument xdoc)
        {
            ReadEntityTypes(xdoc);
            ReadEnums(xdoc);

            ReadContainers(xdoc);

        }
    }
}
