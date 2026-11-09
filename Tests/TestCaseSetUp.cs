using static Csharp_TestBase.Utils.WebdriverFactory;

namespace Csharp_TestBase.Tests
{
    [TestFixture]
    public class TestCaseSetup : GlobalTestRunSetup
    {
        [SetUp]
        public void TestSetup()
        {
            Driver = InitializeWebdriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TestTeardown()
        {
            try
            {
                if (Driver is not null)
                {
                    if (!string.IsNullOrWhiteSpace(Driver.Url) && Driver.Url != @"data:,")
                    {
                        Console.WriteLine(string.Format($"Ending page: {Driver.Url}"));
                    }
                    Driver.Quit();
                    Driver.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception during driver teardown: {ex.Message}");
            }
        }
    }
}
