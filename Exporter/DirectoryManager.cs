using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FuryTech.OdataTypescriptServiceGenerator.Exporter
{
    public class DirectoryManager
    {
        private readonly string _outFolder;
        private readonly DirectoryInfo _di;

        public void PrepareOutput(bool purgeOutput)
        {
            Logger.Log($"Preparing output path '{_outFolder}'...");
            if (!Directory.Exists(_outFolder))
            {
                Logger.Log("Folder doesn't exists, creating...");
                Directory.CreateDirectory(_outFolder);
            }
            else
            {
                var files = _di.GetFiles();
                var dirs = _di.GetDirectories();
                if (files.Any() || dirs.Any())
                {
                    if (purgeOutput)
                    {
                        Logger.Log("Purging output directory...");

                        foreach (var directoryInfo in dirs)
                        {
                            Logger.Log($"Removing directory '{directoryInfo.Name}'");
                            directoryInfo.Delete(true);
                        }

                        foreach (var fileInfo in files)
                        {
                            Logger.Log($"Removing file '{fileInfo.Name}'");
                            fileInfo.Delete();
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("The output folder is not empty and output  purging is disabled. Please enable purging or delete your output folder manually.");
                    }
                }
            }
        }

        public void PrepareNamespaceFolders(IEnumerable<string> namespaces)
        {
            Logger.Log("Preparing namespace structure");
            var nsList = namespaces.ToList().OrderBy(a => a);
            foreach (var ns in nsList)
            {
                var path = ns.Replace('.', Path.DirectorySeparatorChar);
                Logger.Log($"Creating subdirectory '{path}'");
                _di.CreateSubdirectory(path);
            }

        }

        public DirectoryManager(string outFolder)
        {
            _outFolder = outFolder;
            _di = new DirectoryInfo(_outFolder);
        }
    }
}
