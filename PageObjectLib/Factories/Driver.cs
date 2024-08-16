using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace PageObjectLib.Factories
{
    public static class Driver
    {
        private static IWebDriver? _driver;
        private static WebDriverWait? _wait;
        private static ChromeOptions? _chromeOptions;
        private static EdgeOptions? _edgeOptions;
        private static Actions? _actions;

        public static WebDriverWait GetWait() => _wait ??= new(_driver, TimeSpan.FromSeconds(60));

        public static WebDriverWait GetWaitByTime(TimeSpan time) => _wait ??= new(_driver, time);
        public static IWebDriver GetDriver() => _driver;
        public static void CreateDriver(string driver)
        {
            switch (driver)
            {
                case "chrome":
                    _driver ??= new ChromeDriver(GetChromeOptions());
                    break;

                case "edge":
                    _driver ??= new EdgeDriver(GetEdgeOptions());
                    break;
            };
        }

        public static Actions GetActions() => _actions ??= new Actions(_driver);

        public static void GoUrl(string url) => _driver?.Navigate().GoToUrl(url);

        public static ChromeOptions GetChromeOptions()
        {
            if (_chromeOptions == null)
            {
                _chromeOptions = new ChromeOptions();
                _chromeOptions.AddArgument("start-maximized");
                _chromeOptions.AddArgument("--disable-notifications");
            }
            return _chromeOptions;
        }

        public static EdgeOptions GetEdgeOptions()
        {
            if (_edgeOptions == null)
            {
                _edgeOptions = new EdgeOptions();
                _edgeOptions.AddArgument("start-maximized");
                _edgeOptions.AddArgument("--disable-notifications");
            }
            return _edgeOptions;
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