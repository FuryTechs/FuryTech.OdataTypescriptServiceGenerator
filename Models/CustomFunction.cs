using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Abstracts;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class CustomFunction : CustomEventAbstract
    {
        public CustomFunction(XElement xElement) : base(xElement)
        {
            // TODO: Render any function parameters
            var parameters = xElement.Descendants()
                .Where(a => a.Name.LocalName == "Parameter" && a.Attribute("Name").Value != "bindingParameter")
                .Select(propElement => new Property
                {
                    Name = propElement.Attribute("Name")?.Value,
                    IsRequired = propElement.Attribute("Nullable")?.Value == "false",
                    Type = propElement.Attribute("Type")?.Value
                }).ToList();

            FunctionParameters = parameters;
        }

        public List<Property> FunctionParameters { get; set; }
    }
}
