using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace PageObjectLib.Factories
{
    public static class Driver
    {
        private static IWebDriver? _driver;
        private static WebDriverWait? _wait;
        private static ChromeOptions? _chromeOptions;
        private static Actions? _actions;

        public static WebDriverWait GetWait() => _wait ??= new(_driver, TimeSpan.FromSeconds(60));
        public static IWebDriver GetDriver() => _driver ??= new ChromeDriver(GetOptions());
        public static Actions GetActions() => _actions ??= new Actions(_driver);
        public static ChromeOptions GetOptions()
        {
            if (_chromeOptions == null)
            {
                _chromeOptions = new ChromeOptions();
                _chromeOptions.AddArgument("start-maximized");
            }
            return _chromeOptions;
        }
        public static void QuitDriver()
        {
            _driver?.Quit();
            _driver = null;
            _wait = null;
            _actions = null;
        }
        public static void DisposeDriver() => _driver?.Dispose();
    }
}