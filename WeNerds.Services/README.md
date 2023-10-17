# WeNerds Services

Open-source **Commons Services** developed to facilitate the creation of a new project in .Net, providing some commons services.

- 👉 [Nuget Package](https://www.nuget.org/packages/WeNerds.Services) - `nuget page`


> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/wenerds.services/issues)..

## ✨ Services availables on the library
**WeJwtService**
This service provides methods for JWT manipulation.
- GenarateJwt: Generate a JWT Token. You can specify, a TenantId, a AccountId, a UserId, a UserEmail and custom claims.
**WeNotificationService**
- Service for Notification Pattern
- 
**Complete doc coming soon**

> WeNotificationService

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

> 👉 **Step 2** - Register Service wanterd
In program.cs, add the call for: 
- services.AddWeJwt
- services.AddWeNotification
- 
```bash
IHost host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                            services.AddWeJwt(configuration);
                            services.AddWeNotification();
                            services.AddHostedService<Worker>();
                        })
                        .Build();
host.Run();
```
> 👉 **Step 3** - Configure your AppSeettings 
Add te group of properties em AppSetting.json file. 
Warning: For Production use differentes values for them.
```bash
        "Jwt": {
          "Issuer": "We.Jwt",
          "TokenExpiresInHours": 1,
          "RefreshTokenExpiresInHours": 48,
          "Secret": "70ee751e-ab08-47a0-a02a-e667a7e75ffd",
          "Audiance": "http://localhost"
        }
```

> 👉 Complete Documentation WIP

## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
