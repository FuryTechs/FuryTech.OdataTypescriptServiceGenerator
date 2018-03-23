
namespace FuryTech.OdataTypescriptServiceGenerator.Models
{
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string TypescriptType => TypeMapper.MapType(Type);

        public bool IsRequired { get; set; }
    }
}
