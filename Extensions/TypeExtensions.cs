using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuryTech.OdataTypescriptServiceGenerator.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsCollection(this string typeString)
        {
            // OData collection types are of the form "Collection(T)".
            return typeString.StartsWith("Collection(");
        }

        public static string TypeOfCollection(this string typeString)
        {
            if (IsCollection(typeString))
            {
                // Retrieve the fully qualified type.
                var startParen = typeString.IndexOf("(");
                var endParen = typeString.IndexOf(")", startParen + 1, 1);
                return typeString.Substring(startParen + 1, endParen - startParen - 1);
            }

            throw new ArgumentException("The supplied type string is not a collection.", nameof(typeString));
        }

        /// <summary>
        /// Returns the base type of a fully qualified type.
        /// </summary>
        /// <param name="typeString"></param>
        /// <returns></returns>
        public static string BaseType(this string typeString)
        {
            var lastDot = typeString.LastIndexOf(".");
            return typeString.Substring(lastDot + 1);
        }
    }
}
