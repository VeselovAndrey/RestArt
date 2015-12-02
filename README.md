# RestArt
RestArt is the .NET library that allows easily send REST API commands and get responses.

## Build status ##
[![AppVeyor](https://ci.appveyor.com/api/projects/status/gfhvnmsrd6t8cbk8/branch/master?svg=true&passingText=branch:%20master%20-%20OK&failingText=branch:%20master%20-%20Failed&pendingText=branch:%20master%20-%20In%20progress)](https://ci.appveyor.com/project/VeselovAndrey/restart/branch/master)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/gfhvnmsrd6t8cbk8/branch/dev?svg=true&passingText=branch:%20dev%20-%20OK&failingText=branch:%20dev%20-%20Failed&pendingText=branch:%20dev%20-%20In%20progress)](https://ci.appveyor.com/project/VeselovAndrey/restart/branch/dev)

## Stable Packages ##
[![NuGet - RestArt](https://img.shields.io/nuget/v/RestArt.svg?label=NuGet:%20RestArt&style=flat-square)](https://www.nuget.org/packages/RestArt/)

## Latest Packages ##
[![NuGet - RestArt](https://img.shields.io/nuget/vpre/RestArt.svg?label=NuGet:%20RestArt&style=flat-square)](https://www.nuget.org/packages/RestArt/)
[![MyGet - RestArt](https://img.shields.io/myget/restart/vpre/RestArt.svg?label=MyGet:%20RestArt&style=flat-square)](https://www.myget.org/feed/restart/package/nuget/RestArt)

## Simple example ##
This is very basic example of RestArt usage.

```C#
// Define data transfer class (that will contain REST API response data).
// This class should contains same properties as response JSON.
public class MyRestApiCommandResponse 
{
    public int Id { get; set; }
    public string Content { get; set; }
}

...

// Getting data from the REST API
public async Task<ApiCommandResponse> GetApiDataAsync () 
{
    // Create client.
    IRestArtClient client = new RestArtClient("http://www.someserver.com/api/v1");

    // Create request. Headers and parameters are optional and can be null.
    var headers = new Dictionary<string, string>() {
        ["Content-Language"] = "en-US"
    };

    var parameters = new Dictionary<string, object>() {
        ["searchQuery"] = "Some string",
        ["maxItems"] = 42
    };

    // This will be GET request to the "GetItems" command.
    var request = new RestRequest(HttpVerb.Get, "GetItems", headers, parameters);

    // Execute request. 
    // Response will be converted to MyRestApiCommandResponse class instance in the case of success.
    RestResponse<MyRestApiCommandResponse> response = await client.ExecuteAsync<MyRestApiCommandResponse>(request);

    return response.Value;
}
```
_ExecuteAsync()_ method will return _RestResponse&lt;TResponse&gt;_ object that contains:

* _StatusCode_ - The response HTTP status codes;
* _Value_ - The object with response data;
* _Raw_ - The raw response.

More details are available in the [project Wiki](https://github.com/VeselovAndrey/RestArt/wiki).