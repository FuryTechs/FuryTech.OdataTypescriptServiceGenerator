using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Models;

namespace FuryTech.OdataTypescriptServiceGenerator.Parsers
{
    public class MetadataReader
    {
        public List<EntityType> EntityTypes { get; set; }

        public IEnumerable<string> NameSpaces
        {
            get { return EntityTypes.Select(a => a.Namespace).Distinct(); }
        }

        public MetadataReader(XDocument xdoc)
        {
            var typeList = new List<EntityType>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EntityType");

            foreach (var xElement in elements)
            {
                var enT = new EntityType(xElement);
                typeList.Add(enT);
                Logger.Log($"Parsed {enT.Namespace}.{enT.Name}");
            }
            EntityTypes = typeList;
        }
    }
}
