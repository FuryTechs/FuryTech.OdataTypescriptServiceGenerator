﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Interfaces;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public abstract class CustomEventAbstract : IHasImports
    {
        public CustomEventAbstract(XElement xElement)
        {
            Name = xElement.Attribute("Name")?.Value;
            BindingParameter = xElement.Descendants()
                .Single(a => a.Name.LocalName == "Parameter" && a.Attribute("Name").Value == "bindingParameter")
                .Attribute("Type")?.Value;

            ReturnType = xElement.Descendants().SingleOrDefault(a => a.Name.LocalName == "ReturnType")?.Attribute("Type")?.Value;

            if (!string.IsNullOrWhiteSpace(ReturnType) && ReturnType.StartsWith("Collection("))
            {
                ReturnsCollection = true;
                ReturnType = ReturnType.TrimStart("Collection(".ToCharArray()).TrimEnd(')');
            }

            if (!string.IsNullOrWhiteSpace(BindingParameter) && BindingParameter.StartsWith("Collection("))
            {
                IsCollectionAction = true;
                BindingParameter = BindingParameter.TrimStart("Collection(".ToCharArray()).TrimEnd(')');
            }

            NameSpace =
                xElement.Ancestors().First(a => a.Attribute("Namespace") != null)?.Attribute("Namespace")?.Value;
        }
        public string Name { get; }
        public string NameSpace { get; }

        public string ReturnType { get; }
        public string BindingParameter { get; }

        public bool IsCollectionAction { get; }
        public bool ReturnsCollection { get; }

        private Uri _uri;
        public Uri Uri => _uri ?? (_uri = new Uri("r://" + NameSpace.Replace(".", Path.DirectorySeparatorChar.ToString()) + Path.DirectorySeparatorChar + Name, UriKind.Absolute));

        public IEnumerable<Uri> Imports
        {
            get
            {
                var uriList = new List<Uri>();
                if (!string.IsNullOrWhiteSpace(ReturnType))
                {
                    uriList.Add(new Uri("r://" + ReturnType.Replace(".", Path.DirectorySeparatorChar.ToString())));
                }
                if (!string.IsNullOrWhiteSpace(BindingParameter))
                {
                    uriList.Add(new Uri("r://" + BindingParameter.Replace(".", Path.DirectorySeparatorChar.ToString())));
                }
                return uriList;
            }
        }
    }
}
