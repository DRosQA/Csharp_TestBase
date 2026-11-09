namespace Csharp_TestBase.POM.Objects
{
    public partial class LoginPage : PageObjectsFactory
    {
        public TopMenu Menu;

        public LoginPage(IWebDriver driver)
            : base(driver)
        {
            PageTitle = "Automation Exercise - Signup / Login";
            Url = envUrl + "/login";
            Menu = new(driver);
        }

        private IWebElement InputLogin =>
            Driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
        private IWebElement InputPassword =>
            Driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        private IWebElement BtnLogin =>
            Driver.FindElement(By.XPath("//button[@data-qa='login-button']"));
    }
}
