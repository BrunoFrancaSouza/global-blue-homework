# Global Blue Homework

User story
As an API user, I would like to calculate Net, Gross, VAT amounts for purchases in Austria so that I can use correctly calculated purchase data.

ACCEPTANCE CRITERIA

<br />
• If the API receives one of the net, gross or VAT amounts and additionally a valid Austrian VAT rate (10%, 13%, 20%), the other two missing amounts (net/gross/VAT) are calculated by the system and returned to the client in a meaningful structure

<br />
• The API provides an error with meaningful error messages, in case of:
o missing or invalid (0 or non-numeric) amount input
o more than one input
o missing or invalid (0 or non-numeric) VAT rate input

<br />

TECHNICAL REQUIREMENTS
• the solution needs to be implemented in .NET Core latest version
• the solution needs to use dependency injection (DI) software design pattern
• the API must fulfil the REST API standards
• the application needs to use Nuget package manager
