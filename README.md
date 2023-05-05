# Domo API SDK - .NET Standard 2.0

Domo API library helps to generate requests for following services:

 * groups
 * users
 * datasets
 * accounts*
 * activity*
 * pages
 * streams*
 * projects and tasks*

_* Not implemented in current version of the SDK_
 
#### Domo SDK Documentation Website
https://developer.domo.com/

## Installation

This project is a class library built for compatibility with .NET Standard 2.0.  It has no external dependencies.

To install the Domo NuGet package, run the following command in the Package Manager Console
```
PM> Install-Package Domo

## General Usage

Check the developer website for the list of available endpoints

```Csharp
var config = new DomoConfig()
{
    ClientId = "{Your Domo Client App Id}",
    ClientSecret = "{Your Domo Client App Secret}"
};
var client = new DomoClient(config);
var groups = await client.Groups.ListGroupsAsync(offset, 50);
```