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

        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
