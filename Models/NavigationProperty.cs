namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class NavigationProperty
    {
        public string Name { get; set; }
        public string ReferentialConstraint { get; set; }
        public string ReferencedProperty { get; set; }
        public string ReferencedEntityTypeName { get; set; }

        public bool IsCollection { get; set; }
    }
}