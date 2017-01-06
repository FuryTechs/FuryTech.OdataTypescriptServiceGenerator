using System;
using System.Configuration;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    class Program
    {
        private static string _metadataPath;
        private static string _outputDirectory;
        private static bool _purgeOutput;

        private static void InitConfig()
        {
            Logger.Log("Reading config...");
            _metadataPath = ConfigurationManager.AppSettings["metadataPath"];
            _outputDirectory = ConfigurationManager.AppSettings["output"];
            _purgeOutput = bool.Parse(ConfigurationManager.AppSettings["purgeOutput"]);
        }

        static void Main(string[] args)
        {
            try
            {
                Logger.Log("Starting...");
                InitConfig();

                var xml = Loader.Load(_metadataPath);
                var metadataReader = new MetadataReader(xml);

                var directoryManager = new DirectoryManager(_outputDirectory);
                var templateRenderer = new TemplateRenderer(_outputDirectory);

                directoryManager.PrepareOutput(_purgeOutput);

                Logger.Log("Preparing namespace structure");
                directoryManager.PrepareNamespaceFolders(metadataReader.EnumTypes);
                directoryManager.PrepareNamespaceFolders(metadataReader.EntitySets);
                directoryManager.PrepareNamespaceFolders(metadataReader.EnumTypes);

                templateRenderer.CreateContext(_metadataPath, "4.0");

                templateRenderer.CreateEntityTypes(metadataReader.EntityTypes);

                templateRenderer.CreateEnums(metadataReader.EnumTypes);

                templateRenderer.CreateServicesForEntitySets(metadataReader.EntitySets);

            }
            catch (Exception ex)
            {
                Logger.Error($"Error details: {ex}");
            }

            Logger.Log("Service generation finished, exiting...");
            Logger.Log("Press any key to exit");
            Console.ReadKey();
        }
    }
}
