using System;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public static class TypeMapper
    {
        /// <summary>
        /// Returns a boolean indicating that the specified type is a built in Typescript type.
        /// </summary>
        /// <param name="odataType"></param>
        /// <returns></returns>
        public static bool IsBuiltInType(string odataType)
        {
            switch (odataType)
            {
                case "number":
                case "string":
                case "boolean":
                case "Uint8Array":
                case "Date":
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Map an OData Edm type to either a built-in Typescript type or a custom type.
        /// </summary>
        /// <param name="odataType"></param>
        /// <returns></returns>
        public static string MapType(string odataType)
        {
            switch (odataType)
            {
                case "Edm.Binary":
                    return "Uint8Array";

                case "Edm.String":
                case "Edm.Duration":
                case "Edm.Guid":
                case "Edm.TimeOfDay":
                case "Edm.Time":
                    return "string";

                case "Edm.Byte":
                case "Edm.SByte":
                case "Edm.Int16":
                case "Edm.Int32":
                case "Edm.Single":
                case "Edm.Double":
                    return "number";

                case "Edm.Decimal":
                case "Edm.Int64":
                    return "string";

                case "Edm.Boolean":
                    return "boolean";

                case "Edm.DateTimeOffset":
                case "Edm.Date":
                    return "Date";

                default:
                {
                    // A Collection(t) can be considered t[]
                    if (odataType.StartsWith("Collection("))
                    {
                        var fullType = MapType(odataType.TrimStart("Collection(".ToCharArray()).TrimEnd(')'));
                        return $"{fullType}[]";
                    }

                    // Don't reduce a custom type, keep the complete namespace.
                    return odataType;
                }
            }
        }
    }
}