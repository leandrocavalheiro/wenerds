# WeNerds Services

Open-source **Commons Services** developed to facilitate the creation of a new project in .Net, providing some commons services.

- 👉 [Nuget Package](https://www.nuget.org/packages/WeNerds.Services) - `nuget page`


> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/wenerds.services/issues)..


## ✨ Using the library

> 👉 **Step 1** - Install library into project

- **Package Manager**

```bash
$ Install-Package WeNerds.Services
```

- **.Net CLI**

```bash
$ dotnet add package WeNerds.Services
```

> 👉 **Step 2** - Register Service<br>
**In the topics of each service available in the package, how to register will be explained.**

## ✨ Services availables on the library
### WeJwtService
This service provides methods for JWT manipulation.
- GenarateJwt: Generate a JWT Token. You can specify, a TenantId, a AccountId, a UserId, a UserEmail and custom claims.
**WeNotificationService**
- Service for Notification Pattern


### WeBabel
This service is a helper for internationalization. It provides methods to get the translation of a key in a specific language.

#### Configuration:
To configure the service, we can create some environment variables (or add them to AppSettings)
- WN_BABEL_LIST_LANGUAGES
    - List of languages ​​supported by your application.  
      **Important**: You must separate each language with a comma.  
      **Example**: "WN_BABEL_LIST_LANGUAGES": "en,pt-BR"
- WN_BABEL_RESOURCES_PATH
    - Path where the Json created with the dictionary to be used will be located.  
      **Suggestion**: Create a folder called Resources in the project root and use this folder to create the Json files.  
      **Example**: "WN_BABEL_RESOURCES_PATH": "Resources"  
- WN_BABEL_DEFAULT_LANGUAGE  
    - What is the default language of the application.  
      **Example** "WN_BABEL_DEFAULT_LANGUAGE": "pt-BR"  
- WN_BABEL_RESOURCE_NAME  
    - Base name of the Json file. Just the base name of the files, it is not necessary to include the language or the extension.  
      **Example**: Assuming that we will have 2 languages **​​pt-BR** and **en**, two files will be created in the Resources folder: **AppMessages.pt-BR.json** and **AppMessages.en.json**.  
      In this case our variable will have the value of: **AppMessages**.  
      "WN_BABEL_RESOURCE_NAME": "AppMessages"  
- WN_BABEL_DEFAULT_CONTEXT  
    - The WeBabel service supports separation of dictionaries by context. This means that you can create smaller dictionaries, so that when using the service it is not necessary to load a large number of items.  
      In this variable we will then inform the default context of the application.  
      **Example**: "WN_BABEL_DEFAULT_CONTEXT": "main"  

To read environment variables, there are 5 extension methods for IConfiguration:  
- GetWeBabelResourcesPath  
- GetWeBabelResourceName  
- GetWeBabelListLanguages  
- GetWeBabelDefaultLanguage  
- GetWeBabelDefaultContext  


#### Registering:
After creating the variables, you have to **register** the service.  
For this there is an extension method for the IServiceCollecion called AddWeBabel, so just call the method and pass the IConfiguration as a parameter and the service is already registered and ready to use.  
**Example of registering a WorkerService**:
```csharp
IHost host = Host.CreateDefaultBuilder(args)
 .ConfigureServices(services =>
 {
     services.AddWeBabel(config); 
     services.AddHostedService<Worker>();
 })
 .Build();
 ```

 **Important**: By default, the service is registered as Transient, but if you are not using contexts, the service can be registered as Scoped or Singleton, so simply pass the desired ServiceLifetime. Example:
```csharp
IHost host = Host.CreateDefaultBuilder(args)
 .ConfigureServices(services =>
 {
    services.AddWeBabel(config, ServiceLifetime.Singleton);
    services.AddHostedService<Worker>();
 })
 .Build();
 ```

 #### Using:
Now with the registration done, we can inject WeBabel wherever we want to use the service.  
In the constructor where we are going to use the service, we do the injection
 ```csharp
 private readonly IWeBabel _babel;
 public Worker(IWeBabel babel)
 {
        _babel = babel.SetContext("account");
 }
 ```

Note that there is a private variable that will receive the injected object.  
And it is also in the constructor that we will use the Setcontext method to change the context, if used.

Json key we are looking for. Below we have an example of a command to write something to the application console:
```csharp
Console.WriteLine(_babel["blocked"]);
``` 

I'm using pt-BR as the default language, so we should have the following result in our console:
>Conta bloqueada

### Json  files examples:
**AppMessages.pt-BR.json**: In this example, context is not being used. Therefore, when using WeBabel, all keys and their values ​​will be loaded.
```json
{
  "blocked": "Conta bloqueada",
  "unblocked": "Conta desbloqueada"
}
```

**AppMessages.en.json**: In this example, context is being used. Therefore when using WeBabel, only keys and values ​​within the specified context will be loaded.
```json
{
  "account": {
      "blocked": "Account blocked",
      "notFound": "Account not found"
  },
  "person": {
      "blocked": "Peson blocked",
      "notFound": "Person not found"
  }
}
```



> 👉 Complete Documentation WIP

## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
