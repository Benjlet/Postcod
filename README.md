# Postcod

Treat yourself to a .NET library that saves a few quid on UK longitude/latitude lookups by using the marvellous Postcodes.IO postcode search.

[![nuget](https://badgen.net/nuget/v/Postcod?icon=nuget)](https://www.nuget.org/packages/Postcod)

# How to use

You can inject the `IPostcodeLookupClient` client into your `IServiceCollection` through the `AddPostcodesIOLookup` extension:

```
public IServiceCollection ConfigureServices(IServiceCollection services)
{
        return services.AddPostcodesIOLookup();
}
```

Referencing `IPostcodeLookupClient` will then yield a mouthwatering client, ripe for looking up postcodes - below is an example of a postcode search with an Azure HttpTrigger function:

```
class GetPostcodeData
{
        private readonly IPostcodeLookupClient postcodeLookupClient;

        public GetPostcodeData(IPostcodeLookupClient postcodeLookupClient)
        {
                _postcodeLookupClient = postcodeLookupClient;
        }

        [FunctionName("GetPostcodeData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "postcode/{postcode}")] HttpRequest req, string postcode)
        {
                var result = await _postcodeLookupClient.Search(postcode);
                return new OkObjectResult($"{result.Latitude},{result.Longitude}");
        }
}
```

Looking up a postcode will return a `Location` object:

```
public class Location
{
        public string Postcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
}
```

You can additionally compare two locations to get the distance 'as the crow flies' between two points:

```
var distance = _postcodeLookupClient.GetDistanceBetween(new Location()
{
        Latitude = 51.462139,
        Longitude = -2.118380
},
new Location()
{
        Latitude = 51.187592,
        Longitude = -2.232880
}, DistanceMeasure.Miles);
```

You can find further examples in the 'Example' projects of the Postcod GitHub repo.
