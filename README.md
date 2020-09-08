# Mortgage.Products.MVP
CaseStudy


Test Cases to Cover:  

API  

-HappyPaths  
Valid Product Id  
Age = Min Age  
Age = Max Age  
mortgageAmount / propertyValue > loanToValue  
mortgageAmount / propertyValue = loanToValue  
LTV > 100%


-ExceptionPaths
Product Does not exist  
Age < Min Age  
Age > Max Age  
Property Value < 0  
Mortgage Amount < 0  
Invalid Property Value - non-int values  
Invalid Mortgage Amount - non-int values 
mortgageAmount / propertyValue < loanToValue  

UI

Does the Webpage display all the correct Product information which is return by the API call
