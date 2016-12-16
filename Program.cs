using System;
using System.Configuration;
using FuryTech.OdataTypescriptServiceGenerator.Exporter;
using FuryTech.OdataTypescriptServiceGenerator.Parsers;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var xml = Loader.Load(ConfigurationManager.AppSettings["metadataPath"]);
                var metadataReader = new MetadataReader(xml);

                var directoryManager = new DirectoryManager(ConfigurationManager.AppSettings["output"]);

                directoryManager.PrepareOutput(bool.Parse(ConfigurationManager.AppSettings["purgeOutput"]));
                directoryManager.PrepareNamespaceFolders(metadataReader.NameSpaces);

            }
            catch (Exception ex)
            {
                Logger.Error($"Error details: {ex}");

            }
            Logger.Log("Press any key to exit");
            Console.ReadKey();
        }
    }
}
