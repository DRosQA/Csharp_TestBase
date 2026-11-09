using Csharp_TestBase.Utils;

namespace Csharp_TestBase.Tests
{
    [TestFixture]
    public class GlobalTestRunSetup
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected IWebDriver Driver;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected string UsersFile = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "TestData",
            "Users.csv"
        );

        [OneTimeSetUp]
        public void RunSetup()
        {
            Console.WriteLine("Test run started.");
        }

        [OneTimeTearDown]
        public void RunTeardown()
        {
            Console.WriteLine("Test run finished.");
        }
    }
}
