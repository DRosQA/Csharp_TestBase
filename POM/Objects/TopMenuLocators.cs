namespace Csharp_TestBase.POM.Objects
{
    public partial class TopMenu : PageObjectsFactory
    {
        public TopMenu(IWebDriver driver)
            : base(driver) { }

        private IWebElement BtnLogin => Driver.FindElement(By.XPath("//a[@href='/login']"));
    }
}
