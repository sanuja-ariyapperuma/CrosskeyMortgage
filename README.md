# CrosskeyMortgage

A working copy of this application is hosted in Azure App Service and can be accessed with the following URL

https://mortgageplan20240301.azurewebsites.net/

All the source code are resides inside src folder

For the development, Microsoft Visual Studio 2022 have been used.

The project is segregated as layered architecture considering the seperation of duties.

MortgagePlan - is a .NET 4.8 MVC application (main application).
MortgagePlan.Business and MortgagePlan.Data have the business logics and DTOs respectively

MortgagePlan.Tests project contains test project using NUnit covering all the calculation parts.

Inside AppData prospect.txt file resides.

When running the MVC application, there is a user interface which shows the outputs according to the requirement.

Users are also can enter data and see the inserted recrods in the same screen.
