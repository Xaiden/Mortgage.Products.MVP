@API
Feature: POST_MortgageProductApplication

	As a home buyer
	I want to submit a mortgage product application
	So I can purchase a home

Scenario Outline: Successfully make a mortgage application
	Given the following product application model exists
		| Name   | Age   | MortgageAmount   | PropertyValue   |
		| <Name> | <Age> | <MortgageAmount> | <PropertyValue> |
	When a Request is made to create a Mortgage Application for Product Id '2'
	Then the '200' response code was returned
	And the response contains a 'accepted' decision
Examples:
	| Scenario          | Name | Age | MortgageAmount | PropertyValue |
	| Minimum LTV Value | Bob  | 20  | 4000           | 5000          |
	| LTV Value = 100%  | Bob  | 20  | 5000           | 5000          |
	| LTV Value > 100%  | Bob  | 20  | 10000          | 5000          |


Scenario Outline: Successfully make a mortgage application - Valid Age values
	If Age value = Minimum Age, it is not accepted
	Spec details the Age need to be Age >= Min
	Current Logic seems to follow Age > Min

	Given the following product application model exists
		| Name | Age   | MortgageAmount | PropertyValue |
		| Bob  | <Age> | 4000           | 5000          |
	When a Request is made to create a Mortgage Application for Product Id '1'
	Then the '200' response code was returned
	And the response contains a 'accepted' decision
Examples:
	| Scenario                     | Age   |
	| Minimum Age                  | 18    |
	| Maximum Age                  | 70    |
	| Above Minimum Age by Decimal | 18.01 |


Scenario Outline: Atempt to create a mortgage application with invalid Age field values
	Given the following product application model exists
		| Name   | Age   | MortgageAmount   | PropertyValue   |
		| <Name> | <Age> | <MortgageAmount> | <PropertyValue> |
	When a Request is made to create a Mortgage Application for Product Id '1'
	Then the '400' response code was returned
	And the response contains a 'rejected' decision
	Then the following error messages are returned
	| Field        | Message        |
	| <ErrorField> | <ErrorMessage> |

Examples:
	| Scenario                     | Name | Age    | MortgageAmount | PropertyValue | ErrorField | ErrorMessage            |
	| Age Below Minimum            | Bob  | 17     | 4500           | 5000          | age        | Invalid age for product |
	| Age Above Maximum            | Bob  | 99     | 4500           | 5000          | age        | Invalid age for product |
	| Age Above Maximum by Decimal | Bob  | 70.99  | 4500           | 5000          | age        | Invalid age for product |
	| Non-Integer Age Value        | Bob  | Twenty | 4500           | 5000          | age        | Invalid age for product |


Scenario Outline: Atempt to create a mortgage application where LTV does not meet product requirement
Given the following product application model exists
		| Name   | Age   | MortgageAmount   | PropertyValue   |
		| <Name> | <Age> | <MortgageAmount> | <PropertyValue> |
	When a Request is made to create a Mortgage Application for Product Id '<ProductId>'
	Then the '400' response code was returned
	And the response contains a 'rejected' decision
	Then the following error messages are returned
	| Field        | Message        |
	| <ErrorField> | <ErrorMessage> |

Examples:
	| Scenario                    | ProductId | Name | Age | MortgageAmount | PropertyValue | ErrorField  | ErrorMessage                   |
	| LTV > 0.5 required          | 3         | Bob  | 20  | 2000           | 5000          | loanToValue | Invalid LVT of 0.4 min is 0.5  |
	| Negative Mortgage Amount    | 3         | Bob  | 20  | -2000          | 5000          | loanToValue | Invalid LVT of -0.4 min is 0.5 |
	| Zero Mortgage Amount        | 3         | Bob  | 20  | 0              | 1             | loanToValue | Invalid LVT of 0 min is 0.5    |
	| Negative Property Value     | 3         | Bob  | 20  | 2000           | -5000         | loanToValue | Invalid LVT of -0.4 min is 0.5 |
	| Non-Integer Mortgage Amount | 3         | Bob  | 20  | Four Thousand  | 5000          | loanToValue | Invalid LVT of NaN min is 0.5  |
	| Non-Integer Property Value  | 3         | Bob  | 20  | 2000           | Five Thousand | loanToValue | Invalid LVT of NaN min is 0.5  |
	| Zero Property Value         | 3         | Bob  | 20  | 1              | 0             | loanToValue |                                |


@Ignore
Scenario Outline: Atempt to create a mortgage application with empty name field
	Should there be Validation on the API for a required length on the Name field?

	Given the following product application model exists
		| Name | Age | MortgageAmount | PropertyValue |
		|      | 20  | 4500           | 5000          |
	When a Request is made to create a Mortgage Application for Product Id '1'
	Then the '400' response code was returned
	And the response contains a 'rejected' decision
	Then the following error messages are returned
	| Field        | Message        |
	| <ErrorField> | <ErrorMessage> |


Scenario: Atempt to create a mortgage application where Product Id is not valid 
	Given the following product application model exists
		| Name | Age | MortgageAmount | PropertyValue |
		| Bob  | 20  | 4500           | 5000          |
	When a Request is made to create a Mortgage Application for Product Id '13'
	Then the '400' response code was returned
	And the response contains a 'rejected' decision
	Then the following error messages are returned
	| Field     | Message                      |
	| productId | Product not found by that ID |
