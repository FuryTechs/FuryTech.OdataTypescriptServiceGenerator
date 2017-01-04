﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class EnumType
    {

        public string Name { get; private set; }

        public string Namespace { get; set; }
        public IEnumerable<EnumMember> Members { get; private set; }

        public EnumType(XElement sourceElement)
        {
            Name = sourceElement.Attribute("Name")?.Value;
            Namespace = sourceElement.Parent?.Attribute("Namespace")?.Value;
            Members = sourceElement.Descendants().Where(a => a.Name.LocalName == "Member")
                .Select(propElement => new EnumMember()
                {
                    Name = propElement.Attribute("Name")?.Value,
                    Value = propElement.Attribute("Value")?.Value
                }).ToList();
        }
    }
}
