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
        public List<CustomAction> CustomActions { get; private set; }
        public List<CustomFunction> CustomFunctions { get; private set; }

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

        private void ReadEntitySets(XDocument xdoc, List<CustomAction> actions, List<CustomFunction> functions)
        {
            Logger.Log("Parsing entity sets...");
            var entitySetList = new List<EntitySet>();
            var elements = xdoc.Descendants().Where(a => a.Name.LocalName == "EntitySet");

            foreach (var xElement in elements)
            {
                var tContainer = new EntitySet(xElement, actions, functions);
                entitySetList.Add(tContainer);
                Logger.Log($"Entity set '{tContainer.Name}' parsed");
            }

            EntitySets = entitySetList;
        }

        private void ReadCustomActions(XDocument xDoc)
        {
            Logger.Log("Parsing custom actions...");
            List<CustomAction> customActionList = new List<CustomAction>();
            var elements = xDoc.Descendants().Where(a => a.Name.LocalName == "Action");
            foreach (var xElement in elements)
            {
                var tCustomAction = new CustomAction(xElement);
                customActionList.Add(tCustomAction);
                Logger.Log($"Custom Action '{tCustomAction.Name}' parsed");
            }
            CustomActions = customActionList;
        }

        private void ReadCustomFunctions(XDocument xDoc)
        {
            Logger.Log("Parsing custom functions...");
            List<CustomFunction> customFunctionList = new List<CustomFunction>();
            var elements = xDoc.Descendants().Where(a => a.Name.LocalName == "Function");
            foreach (var xElement in elements)
            {
                var tCustomAction = new CustomFunction(xElement);
                customFunctionList.Add(tCustomAction);
                Logger.Log($"Custom Action '{tCustomAction.Name}' parsed");
            }
            CustomFunctions = customFunctionList;
        }


        public MetadataReader(XDocument xdoc)
        {
            ReadEntityTypes(xdoc);
            ReadEnums(xdoc);

            ReadCustomActions(xdoc);
            ReadCustomFunctions(xdoc);

            ReadEntitySets(xdoc, CustomActions, CustomFunctions);
        }
    }
}
