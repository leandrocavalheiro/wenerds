# WE Models

Open-source **Commons Features** developed to facilitate the creation of a new project in .Net, providing some commons models.

- 👉 [Nuget Package](https://www.nuget.org/packages/WeNerds.Models) - `nuget page`

> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/wenerds.models/issues)..

## ✨ Using the library

> 👉 **Step 1** - Install library into project

- **Package Manager**

```bash
$ Install-Package WeNerds.Models
```

- **.Net CLI**

```bash
$ dotnet add package WeNerds.Models
```

> 👉 **Step 2** - Register Service
In program.cs, add the call for services.AddWeModels.
```bash
IHost host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            services.AddWeModels();        
                            services.AddHostedService<Worker>();
                        })
                        .Build();
host.Run();
```

The model WeAccessor will be scope registered. Use IWeAccessor for Dependency Injection.

> 👉 **Step 3** - Use the Models
Complete Documentation WIP


All other existing extensions methods and those that may be added in the future will be listed on the Wiki.

## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
