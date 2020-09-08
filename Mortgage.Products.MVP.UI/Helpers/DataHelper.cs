using Mortgage.Products.MVP.API.Models;
using Mortgage.Products.MVP.UI.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Mortgage.Products.MVP.UI.Helpers
{
    public static class DataHelper
    {
        public static IList<MortgageProduct> ProcessApiDataToMortgageProducts(ApiResponse<object> apiResponse)
        {
            IList<MortgageProduct> mortgageProducts = new List<MortgageProduct>();

            JObject responseContent = JObject.Parse(apiResponse.Content.ToString());

            foreach( var mortgageProduct in responseContent["data"])
            {
                mortgageProducts.Add(new MortgageProduct()
                {
                    Description = $"Mortgage provided by {mortgageProduct["lender"]["name"].ToString()}",
                    ImageSrc = mortgageProduct["lender"]["logo"].ToString(),
                    Header = mortgageProduct["name"].ToString(),
                    Price = $"£{mortgageProduct["monthlyRepayment"].ToString()}",
                    Stay =  $"{mortgageProduct["interestRate"].ToString()}%",
                });
            }

            return mortgageProducts;
        }
    }
}
