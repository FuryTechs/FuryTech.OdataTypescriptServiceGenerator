using System;
using System.Configuration;
using FuryTech.OdataTypescriptServiceGenerator.Models;
using McMaster.Extensions.CommandLineUtils;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    class Program
    {
        private static string _metadataPath;
        private static string _outputDirectory;
        private static bool _purgeOutput;
        private static string _endpointName;

        static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.HelpOption();

            // -m url The url to the OData metadata endpoint.
            var metadataUri = app.Option("-m|--metadata <URL>", "The URL of the OData metadata endpoint", CommandOptionType.SingleValue)
                .IsRequired();

            var endpointNameOption = app.Option("-n|--name <NAME>", "The name of the endpoint", CommandOptionType.SingleValue)
                .IsRequired();

            var outputDirectory = app.Option("-o|--output <DIRECTORY>", "Output directory path.",
                CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                Logger.Log("Starting...");

                _metadataPath = metadataUri.Value();
                _purgeOutput = true;
                _endpointName = endpointNameOption.Value();
                _outputDirectory = outputDirectory.HasValue() ? outputDirectory.Value() : "./output";

                var xml = Loader.Load(_metadataPath);
                var metadataReader = new MetadataReader(xml);

                var directoryManager = new DirectoryManager(_outputDirectory);
                var templateRenderer = new TemplateRenderer(_outputDirectory);

                directoryManager.PrepareOutput(_purgeOutput);

                Logger.Log("Preparing namespace structure");
                directoryManager.PrepareNamespaceFolders(metadataReader.EnumTypes);
                directoryManager.PrepareNamespaceFolders(metadataReader.EntitySets);
                directoryManager.PrepareNamespaceFolders(metadataReader.EntityTypes);
                directoryManager.PrepareNamespaceFolders(metadataReader.ComplexTypes);

                directoryManager.DirectoryCopy("./StaticContent", _outputDirectory, true);

                templateRenderer.CreateContext(_metadataPath, "4.0");

                templateRenderer.CreateEntityTypes(metadataReader.EntityTypes);
                templateRenderer.CreateComplexTypes(metadataReader.ComplexTypes);

                templateRenderer.CreateEnums(metadataReader.EnumTypes);

                templateRenderer.CreateServicesForEntitySets(metadataReader.EntitySets);

                templateRenderer.CreateAngularModule(new AngularModule(_endpointName, metadataReader.EntitySets));
                Console.Write("Completed...press any key to exit.");
                Console.ReadKey();
                return 0;
            });

            return app.Execute(args);
        }
    }
}
