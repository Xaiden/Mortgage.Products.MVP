using Mortgage_Products_UI.SeleniumHelpers;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Mortgage_Products_UI.Models
{
    public class Element
    {
        public string _identifier;
        public string _locator;

        public Element(string locator, string identifier)
        {
            _identifier = identifier;
            _locator = locator;
        }

        public IWebElement GetElement()
        {
            By byObj = DriverUtility.GetElementLocationIdentifier(_locator, _identifier);

            return DriverUtility.GetElementObjectFromByLocator(byObj);
        }

        public List<IWebElement> GetElements()
        {
            By byObj = DriverUtility.GetElementLocationIdentifier(_locator, _identifier);

            return DriverUtility.GetElementObjectsFromByLocator(byObj);
        }

    }
}
