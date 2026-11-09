using System.Text.RegularExpressions;

namespace Csharp_TestBase.Utils.SeleniumUtils
{
    public static class WaitHelpers
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private static DefaultWait<IWebDriver> _fluentWait;
        private static IWebDriver _driver;
        private static readonly int defaultTimeoutSeconds = 5;

        private static void ConfigureWait(IWebDriver driver, int timeoutInSeconds)
        {
            _driver = driver;
            _fluentWait = new DefaultWait<IWebDriver>(_driver)
            {
                PollingInterval = TimeSpan.FromSeconds(0.1),
                Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
            };
            _fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public static void WaitForLoaderImageFinish(
            this IWebDriver driver,
            string? loaderImageID = null,
            int? timeoutInSeconds = null
        )
        {
            int timeout = timeoutInSeconds != null ? (int)timeoutInSeconds : defaultTimeoutSeconds;
            ConfigureWait(driver, timeout);
            By loadingImage = By.Id(loaderImageID ?? "loadingImage");
            try
            {
                _driver.FindElement(loadingImage);
                _fluentWait.Until(ConditionHelpers.InvisibilityOfElementLocated(loadingImage));
            }
            catch { }
        }

        public static void WaitForPageLoadingFinish(
            this IWebDriver driver,
            int? timeoutInSeconds = null
        )
        {
            int timeout = timeoutInSeconds != null ? (int)timeoutInSeconds : defaultTimeoutSeconds;
            ConfigureWait(driver, timeout);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ConditionHelpers.PageLoaded()
            );
        }

        public static void WaitForElementToBeClickable(
            this IWebDriver driver,
            By by,
            int? timeoutInSeconds = null
        )
        {
            int timeout = timeoutInSeconds != null ? (int)timeoutInSeconds : defaultTimeoutSeconds;
            ConfigureWait(driver, timeout);

            _fluentWait.Until(ConditionHelpers.ElementIsClickable(by));
        }

        public static void WaitForElementToBeClickable(
            this IWebDriver driver,
            IWebElement webElement,
            int? timeoutInSeconds = null
        )
        {
            int timeout = timeoutInSeconds != null ? (int)timeoutInSeconds : defaultTimeoutSeconds;
            ConfigureWait(driver, timeout);

            _fluentWait.Until(ConditionHelpers.ElementIsClickable(webElement));
        }

        public static void WaitForElement(
            this IWebDriver driver,
            By webElementBy,
            int? timeoutInSeconds = null
        )
        {
            int timeout = timeoutInSeconds != null ? (int)timeoutInSeconds : defaultTimeoutSeconds;
            ConfigureWait(driver, timeout);

            _fluentWait.Until(ConditionHelpers.ElementExists(webElementBy));
        }
    }

    public static class ConditionHelpers
    {
        public static Func<IWebDriver, bool> TitleIs(string title) =>
            (driver) => driver.Title.Equals(title, StringComparison.Ordinal);

        public static Func<IWebDriver, bool> TitleContains(string title) =>
            driver => driver.Title.Contains(title, StringComparison.Ordinal);

        public static Func<IWebDriver, bool> UrlIs(string url) =>
            driver => string.Equals(driver.Url, url, StringComparison.OrdinalIgnoreCase);

        public static Func<IWebDriver, bool> UrlContains(string fraction) =>
            driver => driver.Url?.Contains(fraction, StringComparison.OrdinalIgnoreCase) ?? false;

        public static Func<IWebDriver, bool> UrlMatches(string regex)
        {
            var pattern = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return driver => pattern.IsMatch(driver.Url);
        }

        public static Func<IWebDriver, bool> PageLoaded() =>
            driver =>
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return ((IJavaScriptExecutor)driver)
                    .ExecuteScript("return document.readyState")
                    .Equals("complete");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            };

        public static Func<IWebDriver, IWebElement?> ElementExists(By locator) =>
            driver =>
            {
                try
                {
                    return driver.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };

        public static Func<IWebDriver, IWebElement?> ElementIsVisible(By locator) =>
            driver =>
            {
                try
                {
                    var el = driver.FindElement(locator);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };

        public static Func<IWebDriver, bool> ExactTextInElement(IWebElement element, string text) =>
            driver =>
            {
                try
                {
                    return element.Text.Equals(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, bool> ExactTextInElement(By locator, string text) =>
            driver =>
            {
                try
                {
                    return driver.FindElement(locator).Text.Equals(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, bool> ExactTextInElementValue(
            IWebElement element,
            string text
        ) =>
            driver =>
            {
                try
                {
                    var val = element.GetAttribute("value");
                    return val != null && val.Equals(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, bool> ExactTextInElementValue(By locator, string text) =>
            driver =>
            {
                try
                {
                    var val = driver.FindElement(locator).GetAttribute("value");
                    return val != null && val.Equals(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, bool> TextInElement(IWebElement element, string text) =>
            driver =>
            {
                try
                {
                    return element.Text.Contains(text, StringComparison.OrdinalIgnoreCase);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, bool> TextInElement(By locator, string text) =>
            driver =>
            {
                try
                {
                    return driver
                        .FindElement(locator)
                        .Text.Contains(text, StringComparison.OrdinalIgnoreCase);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, IWebElement?> ElementIsClickable(By locator) =>
            driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);

                    return element.Displayed && element.Enabled ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };

        public static Func<IWebDriver, IWebElement?> ElementIsClickable(IWebElement element) =>
            driver =>
            {
                return element.Displayed && element.Enabled ? element : null;
            };

        public static Func<IWebDriver, bool> ElementIsSelected(
            IWebElement element,
            bool selected = true
        ) => driver => element.Selected == selected;

        public static Func<IWebDriver, bool> ElementIsSelected(By locator, bool selected = true) =>
            driver =>
            {
                try
                {
                    return driver.FindElement(locator).Selected == selected;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };

        public static Func<IWebDriver, bool> AlertIsPresent(bool state = true) =>
            driver =>
            {
                var alertState = false;
                try
                {
                    driver.SwitchTo().Alert();
                    alertState = true;
                }
                catch (NoAlertPresentException)
                {
                    alertState = false;
                }

                return alertState == state;
            };

        public static Func<IWebDriver, bool> InvisibilityOfElementLocated(By locator) =>
            driver =>
            {
                try
                {
                    return !driver.FindElement(locator).Displayed;
                }
                catch (Exception ex)
                    when (ex is NoSuchElementException || ex is StaleElementReferenceException)
                {
                    return true;
                }
            };
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
