using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumTests.Pages
{
    public class Login
    {
        private IWebDriver driver;

        public Login(IWebDriver _driver)
        {
            this.driver = _driver;
            RetryingElementLocator factory = new RetryingElementLocator(driver, TimeSpan.FromMinutes(2));
            PageFactory.InitElements(this,factory);
        }

        public string LoginUrl
        {
            get { return TestSettings.SiteUrl + "Login.aspx"; }
        }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_PortalLogin_UserName")]
        private IWebElement Username;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_PortalLogin_Password")]
        private IWebElement Password;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_PortalLogin_LoginButton")]
        private IWebElement LoginButton;


        public void NavigateToLogInPage()
        {
            driver.Navigate().GoToUrl(LoginUrl);
        }

        public void SetUserName(string username)
        {
            Username.SendKeys(username);
        }

        public void SetPassword(string password)
        {
            Password.SendKeys(password);
        }

        public void LogIn()
        {
            LoginButton.Click();
        }
    }
}
