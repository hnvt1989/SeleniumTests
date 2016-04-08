using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    static class Utils
    {
        public static void WaitForCartSpinner(this IWebDriver driver, TimeSpan timeout)
        {
            DateTime now = DateTime.Now;
            DateTime end = now.Add(timeout);

            while (DateTime.Now < end)
            {
                Thread.Sleep(1000);

                var ret = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return Ctls.productBusyHandler.isBusy()");
                if (!ret)
                {
                    return;
                }
            }

            throw new Exception("Timeout", new TimeoutException(string.Format("Ctls.productBusyHandler.isBusy() is not True after {0} seconds", timeout.TotalSeconds)));
        }

        public static IWebElement FindElementOnPage(IWebDriver webdriver, By by)
        {
            RemoteWebElement element = (RemoteWebElement)webdriver.FindElement(by);
            var hack = element.LocationOnScreenOnceScrolledIntoView;
            return element;
        }

        /// <summary>
        /// click on the background
        /// </summary>
        /// <param name="driver"></param>
        public static void ClickOnBackGround(this IWebDriver driver)
        {
            driver.FindElementOnPoll(By.XPath("/html/body")).Click();
        }

        /// <summary>
        /// Execute the Js Command
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="cmd"></param>
        public static void ExecuteJsCmd(this IWebDriver driver, string cmd)
        {
            Thread.Sleep(2000);
            (driver as IJavaScriptExecutor).ExecuteScript(cmd);
        }


        /// <summary>
        /// Wait for the js variable
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="var"></param>
        /// <param name="timeout"></param>
        public static void WaitForJsVariable(this IWebDriver driver, string var, TimeSpan timeout)
        {
            string retVar = "return" + " " + var + " == true";

            DateTime now = DateTime.Now;
            DateTime end = now.Add(timeout);

            while (DateTime.Now < end)
            {
                Thread.Sleep(1000);

                var ret = (bool)(driver as IJavaScriptExecutor).ExecuteScript(retVar);
                if (ret)
                {
                    return;
                }
            }

            throw new Exception("Timeout", new TimeoutException(string.Format("JavaScript variable is not True after {0} seconds", timeout.TotalSeconds)));
        }

        /// <summary>
        /// Wait for Ajax call to complete
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        public static void WaitForAjax(this IWebDriver driver, TimeSpan timeout)
        {
            //wait until jQuery is loaded
            DateTime now = DateTime.Now;
            DateTime end = now.Add(timeout);

            var jQueryLoaded = false;
            while (now < end)
            {
                Thread.Sleep(100);
                jQueryLoaded = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery != 'undefined'");
                if (jQueryLoaded)
                    break;
                now = DateTime.Now;
            }

            if (!jQueryLoaded)
                throw new TimeoutException("jQuery has not loaded");

            now = DateTime.Now;
            end = now.Add(timeout);

            while (now < end)
            {
                Thread.Sleep(200);
                var ajaxDone = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxDone)
                    return;
                now = DateTime.Now;
            }

            throw new Exception("Timeout", new TimeoutException(string.Format("jQuery.active is not 0 after {0} seconds", timeout.TotalSeconds)));
        }


        /// <summary>
        /// Wait until the page is fully loaded:
        /// The Document.readyState property of a document describes the loading state of the document.
        /// 1. loading: The document is still loading.
        /// 2. Interactive: The document has finished loading and the document has been parsed but sub-resources such as images, stylesheets and frames are still loading. The state indicates that the DOMContentLoaded event has been fired.
        /// 3. Complete: The document and all sub-resources have finished loading. The state indicates that the load event has been fired
        /// ****** When the value of this property changes a readystatechange event fires on the document object.
        /// 
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static void WaitForPageToLoad(this IWebDriver driver, TimeSpan timeout)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(_driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void TakeScreenShot(this IWebDriver driver, string filePath)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();

            //Use it as you want now
            //string screenshot = ss.AsBase64EncodedString;
            //byte[] screenshotAsByteArray = ss.AsByteArray;
            ss.SaveAsFile(filePath, ImageFormat.Png); //use any of the built in image formating
        }

        public static void FluentWait(this IWebDriver driver, By by, TimeSpan timeout, TimeSpan pollingInterval)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.PollingInterval = pollingInterval;
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => d.FindElement(@by));
        }

        /// <summary>
        /// Polling until the element is visible
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IWebElement FindElementOnPoll(this IWebDriver driver, By by, int timeout = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            IWebElement retElement = null;
            try
            {
                retElement = wait.Until(ExpectedConditions.ElementIsVisible(by));

            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {

            }
            return retElement;
        }

        public static IWebElement FindChildElement(this IWebElement parent, By by, int timeout = 5)
        {

            //todo need timeout 
            IWebElement retElement = null;
            try
            {
                retElement = parent.FindElement(by);

            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {

            }
            return retElement;
        }

        public static String GetAttribute(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            return executor.ExecuteScript("arguments[0].getAttribute('maxlength')", element).ToString();
        }
        /// <summary>
        /// poll unitl the element is enable
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static void WaitForElementEnable(this IWebDriver driver, By by, TimeSpan timeout)
        {

            DateTime now = DateTime.Now;
            DateTime end = now.Add(timeout);

            while (DateTime.Now < end)
            {
                Thread.Sleep(500);
                try
                {
                    driver.FindElementOnPoll(by).SendKeys("");
                }
                catch (InvalidElementStateException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
                return;
            }

            throw new TimeoutException("Element is still disable");

            //IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            //DateTime now = DateTime.Now;
            //DateTime end = now.Add(timeout);
            //if(driver.FindElement(by).GetAttribute("disabled") == null)
            //    throw new WebTestException("NULL");
            //while (DateTime.Now < end)
            //{
            //    Thread.Sleep(500);
            //    if (Boolean.Parse(driver.FindElement(by).GetAttribute("disabled")) == false)
            //        return;
            //}

            //throw new TimeoutException("Element is still disable");
        }

        public static IWebElement FindStaleElementOnPoll(this IWebDriver driver, By by, int timeout = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            try
            {

                return wait.Until(PresenceOfElementLocated(driver, by));

            }
            catch
            {
                Thread.Sleep(500);
                return FindStaleElementOnPoll(driver, by);
            }
        }

        private static Func<IWebDriver, IWebElement> PresenceOfElementLocated(IWebDriver driver, By locator)
        {
            var f = new Func<IWebDriver, IWebElement>(webDriver => driver.FindElement(locator));

            return f;

        }

        //click on this WebElement
        public static void ClickOn(this IWebDriver driver, IWebElement element, bool backgroundClick = true)
        {
            string somewhere = "/html/body";
            if (backgroundClick)
                driver.FindElement(By.XPath(somewhere)).Click();

            // Scroll the browser to the element's Y position
            //(driver as IJavaScriptExecutor).ExecuteScript(string.Format("window.scrollTo(0, {0});", element.Location.Y));

            // Click the element
            element.Click();
        }


        /// <summary>
        /// Click on element using JavaScript
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void JSClick(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static IAlert AlertIsPresent(IWebDriver drv)
        {
            try
            {
                // Attempt to switch to an alert
                return drv.SwitchTo().Alert();
            }
            catch (OpenQA.Selenium.NoAlertPresentException)
            {
                // We ignore this execption, as it means there is no alert present...yet.
                return null;
            }

            // Other exceptions will be ignored and up the stack
        }

        public static void WaitForAlert(this IWebDriver driver, int timeoutInSecond = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecond));
            //wait until the alert is shown
            wait.Until(driver1 => AlertIsPresent(driver1));

        }

        /// <summary>
        /// Wait until this element to become not available
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        public static void WaitForElementNotPresent(this IWebDriver driver, By by, TimeSpan timeout)
        {
            DateTime end = DateTime.Now.Add(timeout);
            while (DateTime.Now < end)
            {
                if (driver.FindElementOnPoll(by, 1) == null) return;
            }

            throw new TimeoutException("Element still present");
        }

        public static bool AssertElementPresent(this IWebDriver driver, By by)
        {
            if (driver.FindElementOnPoll(by) != null)
                return true;
            else
                return false;
        }
        public static ArrayList JSError(this IWebDriver driver)
        {
            ArrayList ret = (ArrayList)(driver as IJavaScriptExecutor).ExecuteScript("return window.JSErrorCollector_errors.pump()");
            return ret;
        }


        //return the javascript errors on page
        public static string GetJsError(this IWebDriver driver)
        {
            var jsErrCnt = (int)((IJavaScriptExecutor)driver).ExecuteScript("return webtest.jsErrorLog.length");
            var ret = "";
            if (jsErrCnt > 0)
            {
                ret = (string)((IJavaScriptExecutor)driver).ExecuteScript("return JSON.stringify(webtest.jsErrorLog, null, 2)");
            }
            return ret;
        }

    }
}
