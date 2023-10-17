# WeNerds Commons

Open-source **Commons Features** developed to facilitate the creation of a new project in .Net, providing some commons features and many extensions methods.

- 👉 [Nuget Package](https://www.nuget.org/packages/WeNerds.Commons) - `nuget page`

<br />

> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/wenerds/issues)..

## ✨ Using the library

> 👉 **Step 1** - Install library into project

- **Package Manager**

```bash
$ Install-Package WeNerds.Commons
```

- **.Net CLI**

```bash
$ dotnet add package WeNerds.Commons
```

<br />

> 👉 **Step 2** - Register Service
In program.cs, add the call for services.AddWeCommons. This register it's necessary for use Criptography Methods.
```bash
IHost host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            services.AddWeCommons();        
                            services.AddHostedService<Worker>();
                        })
                        .Build();
host.Run();
```

> 👉 **Step 3** - Configure your AppSeettings 
Add te group of properties em AppSetting.json file. 
Warning: For Production use differentes values for them.
```bash
    "WeNerds": {
      "Criptography": {
        "Salt": "c19ad735-1cef-4149-80bb-b039f6d527a3",
        "Key": "5fcdbc7a-dfa2-4898-a4cc-62f3e82769ac",
        "Interations": 1000
      }
    }
```

> 👉 **Step 4** - Using Criptography Methods
```bash
    private readonly ILogger<Worker> _logger;
    private readonly ICriptographyMethods _criptographyMethods; //private variable for use in object

    public Worker(ILogger<Worker> logger, ICriptographyMethods criptographyMethods)
    {
        _logger = logger;
        _criptographyMethods = criptographyMethods;  //Private variable receive te value from Dependency Injection 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
            var myPlainText = "123456";
            var myCriptoText = _criptographyMethods.Encrypt(myPlainText); //text encryption
            var isEqual = _criptographyMethods.CompareText(myPlainText, myPlainText); //Plain text versus ciphertext comparison
    }
```

> 👉 Complete Documentation WIP


## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
