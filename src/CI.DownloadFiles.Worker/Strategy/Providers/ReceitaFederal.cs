using CI.DownloadFiles.Worker.Config;
using OpenQA.Selenium;

namespace CI.DownloadFiles.Worker.Strategy.Providers
{
    public class ReceitaFederal : AbstractDownloadFile
    {
        private SeleniumHelper _helper;

        public ReceitaFederal(ConfigurationHelper config) : base(config) { }

        private readonly int StartLinkWithouSpan = 3;
        private readonly int MaxLinkWithouSpan = 13;
        private readonly int StartLinkWithSpan = 14;
        private readonly int MaxLinkWithSpan = 22;
        private readonly string ErrorMessage = "Serviço temporariamente indisponível";

        private bool WasAvailableOnReceita = true;

        public override bool DownloadFiles(SeleniumHelper helper)
        {
            _helper = helper;

            _helper.GoToUrl(_config.ReceitaFederalUrl);

            LinksWithouSpan();

            LinksWithSpan();

            if (!WasAvailableOnReceita)
                return false;

            return true;
        }

        private bool LinksWithouSpan()
        {
            for (int x = StartLinkWithouSpan; x <= MaxLinkWithouSpan; x++)
            {
                var link = $"/html/body/div[3]/div[2]/div[1]/div/div[3]/div[1]/div/div[5]/div/p[{x}]/a";

                ProcessDownload(link);
            }

            return true;
        }

        private bool LinksWithSpan()
        {
            string span = "/span/";

            for (int x = StartLinkWithSpan; x <= MaxLinkWithSpan; x++)
            {
                var link = $"/html/body/div[3]/div[2]/div[1]/div/div[3]/div[1]/div/div[5]/div/p[{x}]{span}a";

                ProcessDownload(link);

                span += "span/";
            }

            return true;
        }

        private void ProcessDownload(string link)
        {
            HasAlreadyDownloaded(_helper.GetElementByXPath(link).Text);

            _helper.ClickByXPath(link);

            if (!IsAvailable())
            {
                WasAvailableOnReceita = false;
            }

            _helper.BackPage();
        }

        private bool IsAvailable()
        {
            try
            {
                var errorText = _helper.GetElementByXPath("/html/body").Text;

                if (errorText.Contains(ErrorMessage))
                    return false;

                return true;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }
    }
}
