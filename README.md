<div align="center">
# Domo API SDK 
</div>

<div align="center">
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://github.com/oncoursesystems/domo-sdk/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/oncoursesystems/domo-sdk/actions/workflows/ci.yml) 
[![NuGet Version](https://img.shields.io/nuget/v/OnCourse.Domo)](https://www.nuget.org/packages/OnCourse.Domo/) 
</div>

Domo API library helps to generate requests for following services:

 * groups
 * users
 * datasets
 * pages
 * accounts*
 * activity*
 * streams*
 * projects and tasks*

_* Not implemented in current version of the SDK_
 
#### Domo SDK Documentation Website
https://developer.domo.com/

## Installation

This project is a class library built for compatibility with .NET Standard 2.0.  It has no external dependencies.

To install the Domo NuGet package, run the following command in the Package Manager Console
```
PM> Install-Package OnCourse.Domo
```

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