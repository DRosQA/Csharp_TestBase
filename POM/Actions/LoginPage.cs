using Csharp_TestBase.Utils;

namespace Csharp_TestBase.POM.Objects
{
    public partial class LoginPage
    {
        public LandingPage? Login(User userData)
        {
            InputLogin.SendKeys(userData.Login);
            InputPassword.SendKeys(userData.Password);
            BtnLogin.Click();
            return new(Driver);
        }
    }
}
