using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Extensions;
using FuryTech.OdataTypescriptServiceGenerator.Interfaces;
using FuryTech.OdataTypescriptServiceGenerator.Models;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    public class TemplateRenderer
    {
        private readonly string _entityTemplate;
        private readonly string _propertyTemplate;
        private readonly string _importTemplate;
        private readonly string _enumTypeTemplate;
        private readonly string _enumMemberTemplate;
        private readonly string _entitySetServiceTemplate;
        private readonly string _contextTemplate;
        private readonly string _angularModuleTemplate;
        private readonly string _angularCustomActionTemplate;
        private readonly string _angularCustomFunctionTemplate;


        private readonly string _outFolder;

        public TemplateRenderer(string outFolder)
        {
            _outFolder = outFolder;
            _entityTemplate = File.ReadAllText(ConfigurationManager.AppSettings["EntityTypeTemplate"]);
            _propertyTemplate = File.ReadAllText(ConfigurationManager.AppSettings["EntityPropertyTemplate"]);
            _importTemplate = File.ReadAllText(ConfigurationManager.AppSettings["ImportsTemplate"]);
            _enumTypeTemplate = File.ReadAllText(ConfigurationManager.AppSettings["EnumTypeTemplate"]);
            _enumMemberTemplate = File.ReadAllText(ConfigurationManager.AppSettings["EnumMemberTemplate"]);
            _entitySetServiceTemplate = File.ReadAllText(ConfigurationManager.AppSettings["EntitySetServiceTemplate"]);
            _contextTemplate = File.ReadAllText(ConfigurationManager.AppSettings["ContextTemplate"]);
            _angularModuleTemplate = File.ReadAllText(ConfigurationManager.AppSettings["AngularModuleTemplate"]);
            _angularCustomActionTemplate = File.ReadAllText(ConfigurationManager.AppSettings["AngularCustomActionTemplate"]);
            _angularCustomFunctionTemplate = File.ReadAllText(ConfigurationManager.AppSettings["AngularCustomFunctionTemplate"]);
        }

        private string ParseImports(IHasImports entity)
        {
            return string.Join("", entity.GetImportRecords().Select(a =>
                _importTemplate.Clone().ToString()
                    .Replace("$moduleNames$", a.ElementTypeName)
                    .Replace("$relativePaths$", "./" + a.RelativeNamespace)));
        }

        private void DoRender(IRenderableElement entity, string template, string fileName = null)
        {
            var ns = entity.NameSpace.Replace('.', Path.DirectorySeparatorChar);
            if (fileName == null)
                fileName = entity.Name;

            var imports = entity as IHasImports;
            if (imports != null)
            {
                template = template.Replace("$imports$", ParseImports(imports));
            }

            template = template
                .Replace("$EntityType$", entity.Name)
                .Replace("$Name$", entity.Name)
                .Replace("$NameSpace$", entity.NameSpace);

            File.WriteAllText($"{_outFolder}\\{ns}\\{fileName}.ts", template);
        }

        public void CreateEntityTypes(IEnumerable<EntityType> types)
        {
            foreach (var entityType in types)
            {
                CreateEntityType(entityType);
            }
        }
        private void CreateEntityType(EntityType entityType)
        {
            var props = entityType.Properties.Select(prop =>
                _propertyTemplate.Clone()
                    .ToString()
                    .Replace("$propertyName$", prop.Name)
                    .Replace("$propertyType$", prop.TypescriptType));


            var refs = entityType.NavigationProperties.Select(nav =>
                _propertyTemplate.Clone()
                    .ToString()
                    .Replace("$propertyName$", nav.Name)
                    .Replace("$propertyType$", nav.Type.Split('.').Last() + (nav.IsCollection ? "[]" : ""))
            );

            var template = _entityTemplate.Clone().ToString()
                .Replace("$properties$", string.Join("", props))
                .Replace("$navigationProperties$", string.Join("", refs));

            DoRender(entityType, template);
        }

        public void CreateEnums(IEnumerable<EnumType> types)
        {
            foreach (var enumType in types)
            {
                CreateEnum(enumType);
            }
        }

        private void CreateEnum(EnumType enumType)
        {
            var members = enumType.Members.Select(m => _enumMemberTemplate.Clone().ToString()
                .Replace("$memberName$", m.Name)
                .Replace("$memberValue$", m.Value));

            var template = _enumTypeTemplate.Clone().ToString()
                .Replace("$members$", string.Join("", members).TrimEnd(','));
            DoRender(enumType, template);
        }


        public void CreateServicesForEntitySets(IEnumerable<EntitySet> entitySets)
        {
            foreach (var entitySet in entitySets)
            {
                CreateServiceForEntitySet(entitySet);
            }
        }

        private string GetCustomActionsTemplate(List<CustomAction> actions)
        {
            if (!actions.Any())
            {
                return string.Empty;
            }
            var result = "\r\n/*Custom Actions*/\r\n";
            foreach (var customAction in actions)
            {
                result += _angularCustomActionTemplate.Clone().ToString()
                    .Replace("$actionName$", customAction.Name);
            }
            return result;
        }

        private string GetCustomFunctionsTemplate(List<CustomFunction> functions)
        {
            if (!functions.Any())
            {
                return string.Empty;
            }
            var result = "\r\n/*Custom Functions*/\r\n";
            foreach (var customFunction in functions)
            {
                result += _angularCustomFunctionTemplate.Clone().ToString()
                    .Replace("$functionName$", customFunction.Name);
            }
            return result;


        }

        private void CreateServiceForEntitySet(EntitySet entitySet)
        {
            var template = _entitySetServiceTemplate.Clone().ToString()
                .Replace("$entitySetUrl$", entitySet.EntitySetName)
                .Replace("$entityTypeName$", entitySet.EntityType.Split('.').Last())
                .Replace("$customActions$", GetCustomActionsTemplate(entitySet.CustomActions.ToList()))
                .Replace("$customFunctions$", GetCustomFunctionsTemplate(entitySet.CustomFunctions.ToList()));
            DoRender(entitySet, template);
        }

        public void CreateContext(string metadataPath, string odataVersion)
        {

            var template = _contextTemplate.Clone().ToString()
                .Replace("$odataRootPath$", metadataPath.TrimEnd("$metadata".ToCharArray()))
                .Replace("$metadataPath$", metadataPath)
                .Replace("$CreationDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("$ODataVersion$", odataVersion);
            File.WriteAllText($"{_outFolder}\\ODataContext.ts", template);
        }

        public void CreateAngularModule(AngularModule module)
        {
            var template = _angularModuleTemplate.Clone().ToString()
                .Replace("$moduleProviders$", string.Join(",\r\n\t",module.EntitySets.Select(a=>a.Name)))
                .Replace("$moduleName$", module.Name);

            DoRender(module, template);

        }
    }
}
