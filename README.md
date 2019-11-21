<div align="center">

![Domo logo](domo.png)

# OnCourse.Domo

</div>

<div align="center">

[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://github.com/oncoursesystems/domo-sdk/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/oncoursesystems/domo-sdk/actions/workflows/ci.yml)
[![NuGet Version](https://img.shields.io/nuget/v/OnCourse.Domo)](https://www.nuget.org/packages/OnCourse.Domo/)

</div>

### OnCourse.Domo is a .NET SDK library used to communicate with the Domo API

---

## ✔ Features
This SDK supports the following [Domo platform (OAuth) APIs](https://developer.domo.com/portal/8ba9aedad3679-ap-is):

| API | Description | Supported |
| --- | --- | --- |
| [`Account`](https://developer.domo.com/portal/w8dk0f75hetfk-account-api) | The Account API allows you to create, update, validate and share accounts in Domo. | ❌ |
| [`Activity Log`](https://developer.domo.com/portal/i19jain6fvwjj-activity-log-api) | The Activity Log API enables retrieving activity log entries from your Domo instance. | ❌|
| [`DataSet`](https://developer.domo.com/portal/3b1e3a7d5f420-data-set-api) | The DataSet objects allows you to create, import, export and manage DataSets and manage data permissions for DataSets within Domo. | ✅ |
| [`Embed Token`](https://developer.domo.com/portal/uc9ls4li6ny8s-embed-token-api) | The Embed Token API allows you to automate the creation of embed tokens for use with programmatic filtering. | ❌ |
| [`Group`](https://developer.domo.com/portal/6tw2454j0zttg-group-api) | Group objects allow you to manage a group and users associated to a group. | ✅ |
| [`Page`](https://developer.domo.com/portal/gcl6cvkh1x5nk-page-api) | The Page API allows you to create, delete, retrieve a page or a list of pages, and update page information and content within a page. | ✅ |
| [`Projects and Tasks`](https://developer.domo.com/portal/wnn8cxurat78o-projects-and-tasks-api) | “Projects and Tasks” is a project management tool that helps you take real action with simple planning, assigning, and task-tracking features. | ❌ |
| [`Simple`](https://developer.domo.com/portal/jaqelzzxpee3e-simple-api) | The Simple API allows you to create new DataSets and import data into the DataSets in your Domo instance. | ❌ |
| [`Stream`](https://developer.domo.com/portal/lw7cqi3lqufah-stream-api) | The Stream API allows you to automate the creation of new DataSets in your Domo Warehouse, featuring an accelerated upload Stream. | ❌ |
| [`User`](https://developer.domo.com/portal/v91hopqk7ki3b-user-api) | User objects allow you to manage a user and the user’s attributes such as a department, phone number, employee number, email, and username. | ✅ |

## ⭐ Installation

This project is a class library built for compatibility all the back to .NET Standard 2.0.  It has no external dependencies.

To install the OnCourse.Domo NuGet package, run the following command via the dotnet CLI
```
dotnet add package OnCourse.Domo
```

Or run the following command in the Package Manager Console of Visual Studio
```
PM> Install-Package OnCourse.Domo
```

## 📕 General Usage

> Check the [Domo developer website](https://developer.domo.com/) for the list of all available endpoints and instructions on setting up your Client ID and Client Secret

```Csharp
var config = new DomoConfig()
{
    ClientId = "{Your Domo Client App Id}",
    ClientSecret = "{Your Domo Client App Secret}"
};
var client = new DomoClient(config);
var groups = await client.Groups.ListGroupsAsync(offset, 50);
```
