namespace Csharp_TestBase.POM.Objects
{
    public partial class LandingPage : PageObjectsFactory
    {
        public TopMenu Menu;

        public LandingPage(IWebDriver driver)
            : base(driver)
        {
            PageTitle = "Automation Exercise";
            Url = envUrl;
            Menu = new(driver);
        }

        public LandingPage GoTo()
        {
            Driver.Url = Url;
            Driver.Navigate();
            AcceptCookies();
            return new(Driver);
        }
    }
}
