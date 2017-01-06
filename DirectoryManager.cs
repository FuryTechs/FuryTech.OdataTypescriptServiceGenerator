using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FuryTech.OdataTypescriptServiceGenerator.Interfaces;

namespace FuryTech.OdataTypescriptServiceGenerator
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

        public void PrepareNamespaceFolders(IEnumerable<IRenderableElement> namespaces)
        {
            var nsList = namespaces.ToList().OrderBy(a => a.NameSpace);
            foreach (var ns in nsList)
            {
                var path = ns.NameSpace.Replace('.', Path.DirectorySeparatorChar);
                if (!Directory.Exists( Path.Combine(_di.FullName, path)))
                {
                    Logger.Log($"Creating subdirectory '{path}'");
                    _di.CreateSubdirectory(path);
                }
            }
        }

        public DirectoryManager(string outFolder)
        {
            _outFolder = outFolder;
            _di = new DirectoryInfo(_outFolder);
        }
    }
}
