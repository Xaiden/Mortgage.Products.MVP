using Mortgage_Products_MVP.Models;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mortgage_Products_MVP.Hooks
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
