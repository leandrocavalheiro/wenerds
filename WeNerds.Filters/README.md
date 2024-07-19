# WE Filters

Open-source **Commons Filters** developed to facilitate the creation of a new project in .Net, providing some commons middlewares.

- 👉 [Nuget Package](https://www.nuget.org/packages/WeNerds.Filters) - `nuget page`

> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/wenerds/issues)..

## ✨ Using the library

> 👉 **Step 1** - Install library into project

- **Package Manager**

```bash
$ Install-Package WeNerds.Filters
```

- **.Net CLI**

```bash
$ dotnet add package WeNerds.Filters
```
> 👉 **Step 2** - Using the Filters<br>
**In the topics of each filter available in the package, how to register will be explained.**

## ✨ About the Filters
### **WeResultHandlerFilter**
This filter will be responsible for ensuring that in each response, if there is a message in WeNotification, the API response is modified.  
Therefore, if there is any record in WeNotification, regardless of the value in the response, it will be manipulated following the following rule:  
- Will always return an object of type **WeResponse<IEnumerable<WeNotification>>**  
- The **Success** field will have the value **false**  
- The **Data** field will contain a list of **WeNotification**  
- The request StatusCode may be as follows: 403, 401, 404, 400  
    - The priority is exactly this, that is, if the message list has at least one Status403Forbidden message, the request status will be 403, otherwise it checks if there is at least one Status401Unauthorized message, if it has the request status it will be 401, and so on until it reaches status 400.

#### Important: 
For correct operation, this filter depends on the **WeNotification** service also being used.

#### How to use?

How this filter works is very simple, just register it in program.cs and that's it.
```csharp
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<WeResultHandlerFilter>();
});
```

## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
