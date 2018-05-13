using System;
using System.Threading;
using SeleniumTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver driver;
        private Login login;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            login = new Login(driver);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }

        [Test]
        public void Login()
        {
            login.NavigateToLogInPage();
            login.SetUserName(TestSettings.Username);
            login.SetPassword(TestSettings.Password);
            login.LogIn();
        }
    }
}
