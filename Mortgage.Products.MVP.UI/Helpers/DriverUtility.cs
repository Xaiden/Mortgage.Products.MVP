using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mortgage.Products.MVP.UI.SeleniumHelpers
{
    public class DriverUtility
    {
        public static IWebDriver _driver;

        public static void SetupBrowser(string browserType, double timeout)
        {
            switch(browserType.Trim().ToLower())
            {
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                default:
                    throw new Exception($"Unrecognised brower type: {browserType}");
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
            _driver.Manage().Window.Maximize();

        }

        public static void NavigateToUrl(string url)
        {
            url.Should().NotBeNullOrEmpty();
            _driver.Navigate().GoToUrl(url);
        }

        public static void CloseBrowser()
        {
            _driver.Close();
            _driver.Quit();
        }

        public static By GetElementLocationIdentifier(string locator, string identifierValue)
        {
            //TODO Refactor this mess
            //Allow Driver methods to be accessed outside of DriverUtility class
            AssemblyName name = new AssemblyName("WebDriver");
            Assembly assembly = Assembly.Load(name);
            Type classType = assembly.GetTypes().FirstOrDefault(item => item.Name.ToLower().Contains("by"));
            MethodInfo method = classType.GetMethods().FirstOrDefault(item => item.Name.ToLower().Contains(locator.ToLower()));
            By elementLocatorBy = (By)method.Invoke(null, new[] { identifierValue });

            return elementLocatorBy;
        }

        public static IWebElement GetElementObjectFromByLocator(By byObj)
        {
            try
            {
                return _driver.FindElement(byObj);
            }
            catch
            {
                throw new Exception($"unable to find object with By Locator: {byObj.ToString()}");
            }
        }

        public static List<IWebElement> GetElementObjectsFromByLocator(By byObj)
        {
            try
            {
                return _driver.FindElements(byObj).ToList();
            }
            catch
            {
                throw new Exception($"unable to find objects with By Locator: {byObj.ToString()}");
            }
        }
    }
}
