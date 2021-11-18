# Domo API SDK (OnCourse.Domo.Sdk) - .NET Standard 2.0

Domo API library helps to generate requests for following services:

 * groups
 * users
 * datasets
 * accounts*
 * activity*
 * pages*
 * streams*
 * projects and tasks*

_* Not implemented in current version of the SDK_
 
#### Domo SDK Documentation Website
https://developer.domo.com/

## Installation

This project is a class library built for compatibility with .NET Standard 2.0.  It has no external dependencies.

To install the OnCourse.Domo.Sdk NuGet package, run the following command in the Package Manager Console
```
PM> Install-Package OnCourse.Domo.Sdk
```

##  Usage

## Initialize Client
```Csharp
var config = new DomoConfig()
{
    ClientId = "{Your Domo Client App Id}",
    ClientSecret = "{Your Domo Client App Secret}"
};
var domo = new DomoClient(config);
```