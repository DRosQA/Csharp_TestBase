using Csharp_TestBase.POM.Objects;
using Csharp_TestBase.Utils;

namespace Csharp_TestBase.Tests
{
    [TestFixture, Category("Login")]
    public class LoginPageTests : TestCaseSetup
    {
        [TestCase, Category("Critical")]
        public void CheckCorrectLogin()
        {
            User userData = UsersHelpers.GetValidUser(UsersFile);
            LandingPage? page = new LandingPage(Driver).GoTo().Menu.GoToLoginPage().Login(userData);
            new LandingPageValidations(page).ValidatePageLoaded();
        }

        [TestCase, Category("Critical")]
        public void CheckInorrectLogin()
        {
            User userData = UsersHelpers.GetIncorrectUserData(UsersFile);
            LandingPage? page = new LandingPage(Driver).GoTo().Menu.GoToLoginPage().Login(userData);
            new LandingPageValidations(page).ValidatePageLoaded();
            new LoginPageValidations(new LoginPage(Driver)).ValidatePageLoaded();
        }
    }
}
