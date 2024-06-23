using OpenQA.Selenium;
using PageObjectLib.Factories;

namespace PageObjectLib.Elements
{
    public class WebElements
    {
        private readonly By? _locator;

        public WebElements(By locator) => _locator = locator;

        public IWebElement Element
        {
            get
            {
                WaitWebElementPresent();
                return Driver.GetDriver().FindElement(_locator);
            }
        }

        public void WaitWebElementPresent() => Driver.GetWait().Until(drv => drv.FindElements(_locator).Count() > 0);

        public void SendValue(string value)
        {
            ScrollToElement();
            Element.SendKeys(value);
        }

        public void Click()
        {
            ScrollToElement();
            Element.Click();
        }

        public void ScrollToElement() => Driver.GetActions().MoveToElement(Element);

        //public void ScrollToElementByJS() => ((IJavaScriptExecutor)_driver).ExecuteScript("argument[0].scrollIntoView(true)", Element);

        public string GetAttribute(string atr) => Element.GetAttribute(atr);

        public string GetText() => Element.Text;

        public static void AcceptAlert() => GetAlert().Accept();

        public static string GetAlertText() => GetAlert().Text;

        public static void DismissAlert() => GetAlert().Dismiss();

        public static void SendTextToAlert(string text)
        {
            var alert = Driver.GetDriver().SwitchTo().Alert();
            alert.SendKeys(text);
        }

        public static IAlert GetAlert() => Driver.GetDriver().SwitchTo().Alert();

        public static bool IsAlertShown()
        {
            try
            {
                GetAlert();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void SwitchToFrame()
        {
            Driver.GetDriver().SwitchTo().Frame(Element);
        }

        public void FrameExit() => Driver.GetDriver().SwitchTo().DefaultContent();

    }
}