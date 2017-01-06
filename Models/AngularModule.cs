using System;
using System.Collections.Generic;
using System.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Interfaces;

namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class AngularModule : IHasImports
    {
        public AngularModule(string endpointName, IEnumerable<EntitySet> entitySets)
        {
            EntitySets = entitySets;
            Name = endpointName;
            NameSpace = string.Empty;
        }

        public string Name { get; }
        public string NameSpace { get; }

        private Uri _uri;
        public readonly IEnumerable<EntitySet> EntitySets;

        public Uri Uri => _uri ?? (_uri = new Uri("r://"  + Name, UriKind.Absolute));

        public IEnumerable<Uri> Imports
        {
            get
            {
                return EntitySets.Select(a => a.Uri);
            }
        }
    }
}
