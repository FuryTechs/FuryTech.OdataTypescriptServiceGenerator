using System;
using System.Collections.Generic;
using System.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Interfaces;

namespace FuryTech.OdataTypescriptServiceGenerator.Extensions
{

    public class ImportRecord
    {
        public string ElementTypeName { get; set; }
        public Uri RelativeNamespace { get; set; }
    }

    public static class ImportsExtensions
    {
        public static IEnumerable<ImportRecord> GetImportRecords(this IHasImports element)
        {
            var records = element.Imports.Where(a=>a != element.Uri).Select(a => new ImportRecord
            {
                RelativeNamespace = element.Uri.MakeRelativeUri(a),
                ElementTypeName = a.Segments.Last()
            });
            return records;
        }
    }
}
