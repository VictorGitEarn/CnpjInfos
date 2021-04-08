using CI.DownloadFiles.Worker.Config;
using CI.DownloadFiles.Worker.Strategy;
using System;

namespace CI.DownloadFiles.Worker.Services
{
    public class DownloadFileService : IDownloadFileService
    {
        protected readonly SeleniumHelper _helper;
        protected readonly ConfigurationHelper _config;

        public DownloadFileService(ConfigurationHelper config)
        {
            _config = config;
            _helper = new SeleniumHelper(Browser.Chrome, _config, false);
        }

        public void Execute()
        {
            try
            {
                Enum.TryParse(_config.Provider, true, out Provider provider);

                IDownloadFileProvider downloadFileProvider = ProviderFactory.CreateProvider(provider, _config);

                if (!downloadFileProvider.DownloadFiles(_helper))
                {
                    downloadFileProvider.DownloadFromDemo(_helper);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
