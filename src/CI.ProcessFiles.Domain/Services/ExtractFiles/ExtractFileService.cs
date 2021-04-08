using CI.ProcessFiles.Domain.Config;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace CI.ProcessFiles.Domain.Services.ExtractFiles
{
    public class ExtractFileService : IExtractFileService
    {
        private readonly ConfigHelper _config;
        private readonly string fileExtension = "*.zip";
        private readonly int extensionNumber = 4;

        public ExtractFileService(ConfigHelper config)
        {
            _config = config;
        }

        public void ExtractFiles()
        {
            var files = Directory.GetFiles(_config.DownloadDir, fileExtension, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);

                var extractDirectory = $"{_config.DownloadDir}{fileName.Remove(fileName.Length - extensionNumber)}";

                if (Directory.Exists(extractDirectory) && !Convert.ToBoolean(_config.ExtractAgain))
                    continue;

                if (!Directory.Exists(extractDirectory))
                    Directory.CreateDirectory(extractDirectory);

                var filesInDirectory = Directory.GetFiles(extractDirectory);

                foreach (var fileToDelete in filesInDirectory)
                {
                    File.Delete(fileToDelete);
                }

                ZipFile.ExtractToDirectory(_config.DownloadDir + fileName, extractDirectory);
            }
        }
    }
}
