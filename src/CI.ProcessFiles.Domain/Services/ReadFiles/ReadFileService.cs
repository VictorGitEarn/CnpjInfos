using CI.ProcessFiles.Domain.Business;
using CI.ProcessFiles.Domain.Config;
using CI.ProcessFiles.Domain.Data;
using CI.ProcessFiles.Domain.Strategy;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CI.ProcessFiles.Domain.Services.ReadFiles
{
    public class ReadFileService : IReadFileService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ConfigHelper _config;

        private readonly string fileExtension = "*.zip";
        private readonly string readFileName = "\\READ.txt";
        private readonly int extensionNumber = 4;

        public ReadFileService(ICompanyRepository companyRepository, ConfigHelper config)
        {
            _companyRepository = companyRepository;
            _config = config;
        }

        public void ReadFiles()
        {
            Enum.TryParse(_config.Provider, true, out Provider provider);

            IReadProvider readProvider = ReadProviderFactory.CreateProvider(provider);

            var zipedFiles = Directory.GetFiles(_config.DownloadDir, fileExtension, SearchOption.AllDirectories);

            foreach (var zipedFile in zipedFiles)
            {
                var fileName = Path.GetFileName(zipedFile);

                var path = $"{_config.DownloadDir}{fileName.Remove(fileName.Length - extensionNumber)}";

                if (WasRead(path + readFileName))
                    continue;

                var companies = readProvider.ReadCompaniesFromFile(path);

                Task.Factory.StartNew(() =>
                {
                    foreach (var company in companies)
                    {
                        var companyExists = _companyRepository.GetCompanyBySocialSecurity(company.SocialSecurity);

                        if (companyExists != null)
                        {
                            _companyRepository.Delete(companyExists);

                        }
                        _companyRepository.Save(company);
                    }
                });

                CreateReadFile(path + readFileName);
            }
        }

        private bool WasRead(string path)
        {
            if (!File.Exists(path))
                return false;

            return true;
        }

        private void CreateReadFile(string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                file.WriteLine("Arquivo lido");
            }
        }
    }
}
