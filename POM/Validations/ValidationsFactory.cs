using Csharp_TestBase.Utils.SeleniumUtils;

namespace Csharp_TestBase.POM.Objects
{
    public abstract class ValidationsFactory : IValidations
    {
        internal IPageObject? Page;

        public ValidationsFactory(IPageObject? page)
        {
            Page = page;
        }

        public void ValidatePageLoaded(bool shouldLoad = true)
        {
            Assert.That(Page != null == shouldLoad, "A page loaded when it shouldn't.");
            if (Page != null)
            {
                Assert.That(Page.PageLoaded(), $"Page {Page.GetType()} did not load properly.");
            }
        }
    }
}
