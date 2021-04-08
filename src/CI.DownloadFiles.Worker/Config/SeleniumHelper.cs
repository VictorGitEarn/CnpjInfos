using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace CI.DownloadFiles.Worker.Config
{
    public class SeleniumHelper
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;

        public SeleniumHelper(Browser browser, ConfigurationHelper configuration, bool headless = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateWebDriver(browser, Configuration.WebDrivers, headless);
            WebDriver.Manage().Window.Maximize();
        }

        public string GetUrl()
        {
            return WebDriver.Url;
        }

        public void GoToUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public IWebElement GetElementByXPath(string xPath)
        {
            return WebDriver.FindElement(By.XPath(xPath));
        }

        public ReadOnlyCollection<IWebElement> GetElementsByXPath(string xPath)
        {
            return WebDriver.FindElements(By.XPath(xPath));
        }
        public void ClickByXPath(string xPath)
        {
            var element = WebDriver.FindElement(By.XPath(xPath));
            element.Click();
        }

        public void BackPage()
        {
            WebDriver.Navigate().Back();
        }

        public void ClosePage()
        {
            WebDriver.Close();
        }

        public void SwitchPage(int index)
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[index]);
        }
    }
}
