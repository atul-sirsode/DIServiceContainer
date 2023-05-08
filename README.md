# Overview

**The package is a .NET library written in C# that helps to register services in Dependency Injection (DI).**


## Features

- Simplifies the process of registering services in DI container.
- Makes it easy to manage dependencies across the application.
- Supports different scopes for registered services.
- Default Service Lifetime scope is Singleton.

## Installation
The package can be installed from the NuGet Package Manager or via the command line:

`
Install-Package DIServiceContainer
`

## Usage
To use the package, add the following code to your application:
using DIRegistration;

Let say you have Services in Your Project 
for ex : 
```csharp
Public Interface IEmailService {void SendEmail();}
Public Interface IMessageService { String SendMessage();}

Public class EmailService : IEmailService 
{
 //TODO :: implementation
}

Public class MessageService : IMessageService 
{
 //TODO :: implementation
}
```

Now, instead of registering services individually like 

```csharp
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleTon<IEmailService,EmailService>();
    builder.Services.AddSingleTon<IMessageService,MessageService>();
}
```
you can register those service with single line


```csharp
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices(new[] { typeof(Program).Assembly });
}
```

# Disclaimer

This is an educational project. The source code is licensed under the MIT license.

# License
This package is licensed under the MIT License.