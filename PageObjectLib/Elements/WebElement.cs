using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PageObjectLib.Factories;

namespace PageObjectLib.Elements
{
    public class WebElement
    {
        private static readonly IWebDriver _driver = Driver.GetDriver();
        private static readonly WebDriverWait _wait = Driver.GetWait();
        private readonly By? _locator;
        private static Actions _actions = new Actions(_driver);

        public WebElement(By locator) => _locator = locator;

        public IWebElement Element
        {
            get
            {
                WaitWebElementPresent();
                return _driver.FindElement(_locator);
            }
        }

        public void WaitWebElementPresent() => _wait.Until(drv => drv.FindElements(_locator).Count() > 0);

        public void SendValue(string value)
        {
            ScrollToElementByJS();
            Element.SendKeys(value);
        }

        public void Click()
        {
            ScrollToElementByJS();
            Element.Click();
        }

        public void ScrollToElement() => _actions.MoveToElement(Element);

        public void ScrollToElementByJS() => ((IJavaScriptExecutor)_driver).ExecuteScript("argument[0].scrollIntoView(true)", Element);

        public string GetAttribute(string atr) => Element.GetAttribute(atr);

        public string GetText() => Element.Text;

        public static void AcceptAlert()
        {
            var alert = _driver.SwitchTo().Alert();
            alert.Accept();
        }

        public static bool IsalertShown()
        {
            try
            {
                _driver.SwitchTo().Alert();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void SwitchToFrame()
        {
            _driver.SwitchTo().Frame(Element);
        }

        public void FrameExit() => _driver.SwitchTo().DefaultContent();
    }
}