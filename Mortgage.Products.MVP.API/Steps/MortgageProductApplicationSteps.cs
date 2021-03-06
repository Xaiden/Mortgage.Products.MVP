﻿using FluentAssertions;
using Mortgage.Products.MVP.API.Helpers;
using Mortgage.Products.MVP.API.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Mortgage.Products.MVP.API.StepDefinitions
{
    [Binding]
    public class MortgageProductApplicationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ScenarioData _scenarioData;
        private readonly ITestOutputHelper _output;

        public MortgageProductApplicationSteps(ScenarioContext scenarioContext, ITestOutputHelper output)
        {
            _scenarioContext = scenarioContext;
            _output = output;

            _scenarioData = new ScenarioData();
            _scenarioContext.Set(_scenarioData, "ScenarioData");
        }

        [Given(@"the following product application model exists")]
        public void GivenTheFollowingProductApplicationExist(MortgageProductApplication mortgageProductApplication)
        {
            _scenarioContext["MortgageProductApplication"] = mortgageProductApplication;
        }

        [When(@"a Request is made to create a Mortgage Application for Product Id '(.*)'")]
        public void WhenARequestIsMadeToCreateAMortgageApplication(int productId)
        {
            JObject jsonRequestBody = JObject.FromObject(_scenarioContext["MortgageProductApplication"]);

            string requestUri = $"products/{productId}/applications";

            _scenarioData.ApiResponse = RequestHelper.MakeApiPostRequest(_output, requestUri, jsonRequestBody);
        }

        [Then(@"the '(.*)' response code was returned")]
        public void ThenTheResponseCodeWasReturned(int statusCode)
        {
            _scenarioData.ApiResponse.StatusCode.Should().Be((HttpStatusCode)statusCode);
        }

        [Then(@"the response contains a '(.*)' decision")]
        public void ThenTheResponseContainsADecision(string decision)
        {
            JObject responseContent = JObject.Parse(_scenarioData.ApiResponse.Content.ToString());
                
            MortgageApplicationResponse mortgageApplicationResponse =
                responseContent.ToObject<MortgageApplicationResponse>();

            mortgageApplicationResponse.Decision.Should().BeEquivalentTo(decision);
        }


        [Then(@"the following error messages are returned")]
        public void ThenTheFollowingErrorMessagesAreReturned(MortgageApplicationResponseReasons[] expectedErrorMessages)
        {
            JObject responseContent = JObject.Parse(_scenarioData.ApiResponse.Content.ToString());

            MortgageApplicationResponseReasons[] mortgageApplicationResponseReasons = 
                responseContent.ToObject<MortgageApplicationResponse>().Reasons;

            mortgageApplicationResponseReasons.Should().BeEquivalentTo(expectedErrorMessages);
        }
    }
}
