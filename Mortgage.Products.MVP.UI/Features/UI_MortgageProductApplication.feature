@UI
Feature: UI_MortgageProductApplication

	As a mortgage purchaser
	I want to view all the available mortgage products
	So I know which products are available to me

Scenario: Mortgage Products displayed on Page matches the Product API Response
	Given the mortgage product information is retrieved via the API
	When the application homepage is loaded
	Then the Mortgage Products dispalyed matches the API response
