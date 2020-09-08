using Mortgage.Products.MVP.UI.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Mortgage.Products.MVP.UI.BusinessObjects
{
    public class MortgageProductsObjects
    {
        private Element mortgageItemCollection = new Element("xpath", "//*[contains(@class, 'ui items divided')]/child::*");
        private ScenarioContext _scenarioContext;

        public MortgageProductsObjects(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IList<MortgageProduct> GetDisplayedMortageProducts()
        {
            IList<MortgageProduct> displayedMortgageProducts = new List<MortgageProduct>();

            IList<IWebElement> mortgageElements = mortgageItemCollection.GetElements();

            foreach (var mortgageElement in mortgageElements)
            {
                var imageElement = mortgageElement.FindElement(By.XPath(".//*[contains(@class, 'ui small image')]/child::*"));
                var contentElement = mortgageElement.FindElement(By.XPath(".//*[contains(@class, 'content')]"));

                var headerElement = contentElement.FindElement(By.XPath(".//*[contains(@class, 'header')]"));

                var metaElement = contentElement.FindElement(By.XPath(".//*[contains(@class, 'meta')]"));
                var priceElement = metaElement.FindElement(By.XPath(".//*[contains(@class, 'price')]"));
                var stayElement = metaElement.FindElement(By.XPath(".//*[contains(@class, 'stay')]"));

                var descriptionElement = mortgageElement.FindElement(By.XPath(".//*[contains(@class, 'description')]"));

                displayedMortgageProducts.Add(new MortgageProduct()
                {
                    Description = descriptionElement.Text,
                    Header = headerElement.Text,
                    ImageSrc = imageElement.GetAttribute("src"),
                    Price = priceElement.Text,
                    Stay = stayElement.Text
                });        
            }

            return displayedMortgageProducts;
        }
    }
}
