using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;


namespace SeleniumTests
{
    public class TestBase
    {
        private IWebDriver _driver;
        protected string _baseUrl;

        public TestBase()
        {

        }
        public TestBase(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        ~TestBase()
        {
            //_driver.Quit();
        }

        public IWebDriver StartBrowser()
        {

            string webBrowser = System.Configuration.ConfigurationManager.AppSettings["Browser"]; ;

            DesiredCapabilities caps;

            bool remote = false;

            switch (webBrowser.ToLower())
            {
                case "firefox":
                    if (remote)
                    {
                        caps = DesiredCapabilities.Firefox();
                        caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["Platform"]);
                        caps.SetCapability(CapabilityType.Version, ConfigurationManager.AppSettings["FireFoxVersion"]);
                        caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                        caps.SetCapability("username", "hmnd42009");
                        caps.SetCapability("accessKey", "0cc5c5c4-db5a-4e69-bfd0-67889d3557e0");
                        _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    }
                    else
                    {
                        _driver = new FirefoxDriver();
                    }
                    break;

                case "iexplore":
                    if (remote)
                    {
                        caps = DesiredCapabilities.InternetExplorer();
                        caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["Platform"]);
                        caps.SetCapability(CapabilityType.Version, ConfigurationManager.AppSettings["IexploreVersion"]);
                        caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                        caps.SetCapability("username", "hmnd42009");
                        caps.SetCapability("accessKey", "0cc5c5c4-db5a-4e69-bfd0-67889d3557e0");
                        _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    }
                    else
                    {
                        _driver = new InternetExplorerDriver();
                    }
                    break;

                case "chrome":
                    if (remote)
                    {

                        caps = DesiredCapabilities.Chrome();
                        caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["Platform"]);
                        caps.SetCapability(CapabilityType.Version, "");
                        caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                        caps.SetCapability("username", "xingeryu");
                        caps.SetCapability("accessKey", "66647a62-0334-41a5-92ea-0c80b14a5a50");
                        _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    }
                    else
                    {
                        _driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromMinutes(5));
                    }
                    break;

                case "android":
                    caps = DesiredCapabilities.Android();
                    caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["AndroidPlatform"]);
                    caps.SetCapability(CapabilityType.Version, ConfigurationManager.AppSettings["AndroidVersion"]);
                    caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                    caps.SetCapability("username", "hmnd42009");
                    caps.SetCapability("accessKey", "0cc5c5c4-db5a-4e69-bfd0-67889d3557e0");
                    _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    break;

                case "android_tablet":
                    //NOT WORKING
                    caps = DesiredCapabilities.Android();
                    caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["AndroidPlatform"]);
                    caps.SetCapability(CapabilityType.Version, ConfigurationManager.AppSettings["AndroidVersion"]);
                    //caps.SetCapability(CapabilityType.DeviceType, "tablet");
                    caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                    caps.SetCapability("username", "hmnd42009");
                    caps.SetCapability("accessKey", "0cc5c5c4-db5a-4e69-bfd0-67889d3557e0");
                    _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    break;

                case "iphone":
                    caps = DesiredCapabilities.IPhone();
                    caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["AppleOS"]);
                    caps.SetCapability(CapabilityType.Version, ConfigurationManager.AppSettings["IPhoneVersion"]);
                    caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                    caps.SetCapability("username", "hmnd42009");
                    caps.SetCapability("accessKey", "0cc5c5c4-db5a-4e69-bfd0-67889d3557e0");
                    _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    break;

                case "ipad":
                    caps = DesiredCapabilities.IPad();
                    caps.SetCapability(CapabilityType.Platform, ConfigurationManager.AppSettings["AppleOS"]);
                    caps.SetCapability(CapabilityType.Version, ConfigurationManager.AppSettings["IPadVersion"]);
                    caps.SetCapability("j6", "Testing Selenium 2 with C# on Sauce");
                    caps.SetCapability("username", "hmnd42009");
                    caps.SetCapability("accessKey", "0cc5c5c4-db5a-4e69-bfd0-67889d3557e0");
                    _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                    break;
            }

            //_driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
            //_driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(300));
            //_driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(300));

            _driver.Manage().Window.Maximize();

            return _driver;
        }

        public void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(_baseUrl + url);
        }

        public enum AccountTypeIndex { CUS = 2, KIT = 3, KIT25 = 4, CON = 5 }
    }

}
