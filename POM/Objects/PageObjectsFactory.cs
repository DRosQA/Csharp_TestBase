using Csharp_TestBase.Utils.SeleniumUtils;

namespace Csharp_TestBase.POM.Objects
{
    public abstract class PageObjectsFactory : IPageObject
    {
        internal IWebDriver Driver;
        public string? PageTitle;
        public string Url = envUrl;
        protected static string envUrl = "https://www.automationexercise.com";

        internal PageObjectsFactory(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool PageLoaded()
        {
            if (PageTitle != null)
            {
                WaitHelpers.WaitForPageLoadingFinish(Driver);
                return ConditionHelpers.TitleIs(PageTitle)(Driver);
            }
            return false;
        }

        public void AcceptCookies()
        {
            By consentModalBy = By.ClassName("fc-dialog-container");
            try
            {
                WaitHelpers.WaitForElement(Driver, consentModalBy, 2);
                Driver
                    .FindElement(consentModalBy)
                    .FindElement(By.ClassName("fc-cta-consent"))
                    .Click();
            }
            catch (WebDriverTimeoutException) { }
        }
    }
}
