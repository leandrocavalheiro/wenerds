# WE Middlewares

Open-source **Commons FeatuMiddlewaresres** developed to facilitate the creation of a new project in .Net, providing some commons middlewares.

- 👉 [Nuget Package](https://www.nuget.org/packages/WeNerds.Middlewares) - `nuget page`

> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/wenerds/issues)..

## ✨ Using the library

> 👉 **Step 1** - Install library into project

- **Package Manager**

```bash
$ Install-Package WeNerds.Middlewares
```

- **.Net CLI**

```bash
$ dotnet add package WeNerds.Middlewares
```
> 👉 **Step 2** - Using the Middleware<br>
After build the app in the **program.cs**,  call the **use** method of the middleware:<br>
```code
app.UseExceptionHandlerMiddleware(app.Configuration, app.Environment, app.Logger);
```
- app.Configuration: Used for get the environment variable
- app.Environment: Used for get the current environment
- app.Logger: Used for log error

## ✨ About the Middleware
- **Exception Handler**

This middleware aims to facilitate the handling of exceptions generated in the application.
Any exception not handled by the application will be intercepted by this middleware and the following actions will be taken:

**Production environment**

> If the environment variable **WN_EXCEPTION_HANDLER_SHOW_LOG_PRODUCTION** is t**rue**, a log will be generated using the ILogger service (Microsoft.Extensions.Logging).<br>
This log contains the ErrorId and the generated exception data,and will be generated following the application's ILogger settings, so it can be generated for AWS, Console and others.<br>
The HttpContext Response will not display anything regarding the exception that occurred. Only a Code Message, a Message containing the ErrorId and a StatusCode 500 will be sent to the requester.<br>

> If the environment variable **WN_EXCEPTION_HANDLER_SHOW_LOG_PRODUCTION** is t**false**, the log generation will be ignored and the HttpContext will receive the same response as when the variable is true, but as no log was generated, there will be no way to check the error that occurred.

**Development environment**
> In this case, the HttpContext will receive the detailed error that occurred, thus not needing to generate logs

**Important** <br>
The environment variable **WN_EXCEPTION_HANDLER_SHOW_LOG_PRODUCTION**, can be created at appSettings.json or at environment variable of the Operational System. 

## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
