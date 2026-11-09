using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Csharp_TestBase.Utils
{
    public class WebdriverFactory
    {
        public enum BrowserType
        {
            Chrome,
            Firefox,
        }

        public static IWebDriver InitializeWebdriver(
            BrowserType userSelectedBrowser = BrowserType.Chrome,
            string? downloadPath = null
        )
        {
            return userSelectedBrowser switch
            {
                BrowserType.Firefox => new FirefoxDriver(DefineFirefoxOptions(downloadPath)),
                BrowserType.Chrome => new ChromeDriver(
                    GetDriverService(),
                    DefineChromeOptions(downloadPath)
                ),
                _ => throw new NotImplementedException($"{userSelectedBrowser} is not supported."),
            };
        }

        static FirefoxOptions DefineFirefoxOptions(string? downloadPath = null)
        {
            return string.IsNullOrEmpty(downloadPath)
                ? DefaultFirefoxOptions()
                : AddAutoSaveFilesFirefoxOptions(downloadPath, DefaultFirefoxOptions());
        }

        static ChromeOptions DefineChromeOptions(string? downloadPath = null)
        {
            return string.IsNullOrEmpty(downloadPath)
                ? DefaultChromeOptions()
                : AddAutoSaveFilesChromeOptions(downloadPath, DefaultChromeOptions());
        }

        static FirefoxOptions DefaultFirefoxOptions()
        {
            FirefoxOptions options = new();
            options.AddArguments(
                "--start-maximized",
                "--credentials_enable_service",
                "--disable-infobars"
            );
            return options;
        }

        static FirefoxOptions AddAutoSaveFilesFirefoxOptions(
            string downloadPath,
            FirefoxOptions options
        )
        {
            options.SetPreference("browser.download.dir", downloadPath);
            options.SetPreference("browser.download.manager.showWhenStarting", false);
            options.SetPreference("browser.download.folderList", 2);

            return options;
        }

        static ChromeOptions DefaultChromeOptions()
        {
            ChromeOptions chromeOptions = new();
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--disable_extensions");
            chromeOptions.AddArgument("--start-maximized");
            //chromeOptions.AddArgument("--help");
            //chromeOptions.AddArgument("-allow-cross-origin-auth-prompt");
            //chromeOptions.AddArgument("-allow-external-pages");
            //chromeOptions.AddArgument("-allow-insecure-localhost");
            //chromeOptions.AddArgument("-allow-running-insecure-content");
            //chromeOptions.AddArgument("incognito");
            //chromeOptions.AddArgument("headless");
            //chromeOptions.AddArgument("version"); //prints version
            //chromeOptions.AddArgument("--enable-automation");
            //chromeOptions.AddArgument("--disable-notifications");
            //chromeOptions.AddArgument("--ignore-certificate-errors");
            //chromeOptions.AddArgument("--disable-popup-blocking");
            //chromeOptions.AddArgument("--disable-default-apps");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddExcludedArgument("enable-automation");
            return chromeOptions;
        }

        static ChromeOptions AddAutoSaveFilesChromeOptions(
            string downloadPath,
            ChromeOptions options
        )
        {
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            return options;
        }

        static ChromeDriverService GetDriverService()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            return chromeDriverService;
        }
    }
}
