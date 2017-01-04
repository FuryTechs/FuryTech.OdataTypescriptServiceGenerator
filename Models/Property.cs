using System.Linq;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string TypescriptType
        {
            get
            {
                switch (Type)
                {
                    case "Edm.String":
                        return "string";
                    case "Edm.Int32":
                        return "number";
                    case "Edm.Boolean":
                        return "boolean";
                    case "Edm.DateTimeOffset":
                        return "Date";
                    default:
                    {
                        return Type.Contains(".") ? Type.Split('.').Last(a => !string.IsNullOrWhiteSpace(a)) : "any";
                    }
                }
            }
        }

        public bool IsRequired { get; set; }
    }
}
