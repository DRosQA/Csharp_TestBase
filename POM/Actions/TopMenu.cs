namespace Csharp_TestBase.POM.Objects
{
    public partial class TopMenu
    {
        public LoginPage GoToLoginPage()
        {
            BtnLogin.Click();
            return new(Driver);
        }
    }
}
