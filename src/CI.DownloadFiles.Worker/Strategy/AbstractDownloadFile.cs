using CI.DownloadFiles.Worker.Config;
using System;
using System.IO;

namespace CI.DownloadFiles.Worker.Strategy
{
    public abstract class AbstractDownloadFile : IDownloadFileProvider
    {
        protected readonly ConfigurationHelper _config;

        public AbstractDownloadFile(ConfigurationHelper config)
        {
            _config = config;
        }

        public abstract bool DownloadFiles(SeleniumHelper helper);

        public virtual void DownloadFromDemo(SeleniumHelper helper)
        {
            if (HasAlreadyDownloaded("DADOS_ABERTOS_CNPJ_01.zip") || !Convert.ToBoolean(_config.ShouldDownloadFromDemo))
                return;

            helper.GoToUrl(_config.DemoUrl);

            helper.ClickByXPath(_config.DemoElementToDownload);

            helper.SwitchPage(1);

            helper.ClickByXPath(_config.DemoConfirmElementToDownload);

            helper.ClosePage();

            helper.SwitchPage(0);
        }

        protected bool HasAlreadyDownloaded(string file)
        {
            if (File.Exists($"{_config.DownloadDir}{file}"))
                return true;

            return false;
        }
    }
}
