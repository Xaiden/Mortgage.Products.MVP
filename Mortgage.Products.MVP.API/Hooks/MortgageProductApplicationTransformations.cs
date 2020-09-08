using Mortgage.Products.MVP.API.Models;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mortgage.Products.MVP.Hooks
{
    [Binding]
    public class MortgageProductApplicationTransformations
    {
        [StepArgumentTransformation]
        public MortgageProductApplication MortgageProductApplicationTransformation(Table table)
        {
            return table.CreateInstance<MortgageProductApplication>();
        }

        [StepArgumentTransformation]
        public MortgageApplicationResponseReasons[] MortgageProductApplicationErrorTransformation(Table table)
        {
            return table.CreateSet<MortgageApplicationResponseReasons>().ToArray();
        }
    }
}
