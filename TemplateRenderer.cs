using System.Collections.Generic;
using System.IO;
using System.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Models;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    public class TemplateRenderer
    {

        private readonly string _entityTemplate;
        private readonly string _propertyTemplate;
        private readonly string _importTemplate;

        private readonly string _outFolder;

        public TemplateRenderer(string outFolder)
        {
            _outFolder = outFolder;
            _entityTemplate = File.ReadAllText("Templates\\EntityTypeTemplate.tst");
            _propertyTemplate = File.ReadAllText("Templates\\PropertyTemplate.tst");
            _importTemplate = File.ReadAllText("Templates\\ImportTemplate.tst");
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
            var imports = entityType.Imports.Select(import =>
            {
                var rel = entityType.Uri.MakeRelativeUri(import); // Uri.MakeRelativeUri(entityType.Name.Replace(".", "/"), import.Replace(".", "/"));

                return _importTemplate.Clone()
                    .ToString()
                    .Replace("$moduleNames$", import.Segments.Last())
                    .Replace("$relativePaths$", "./" + rel.ToString());
            });


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

            var ns = entityType.Namespace.Replace('.', Path.DirectorySeparatorChar);

            var ent = _entityTemplate.Clone().ToString()
                .Replace("$EntityType$", entityType.Name)
                .Replace("$properties$", string.Join("", props))
                .Replace("$navigationProperties$", string.Join("", refs))
                .Replace("$imports$", string.Join("", imports));

            File.WriteAllText($"{_outFolder}\\{ns}\\{entityType.Name}.ts", ent);
        }
    }
}
