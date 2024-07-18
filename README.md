# Postcod

Treat yourself to a .NET library that saves a few quid on UK longitude/latitude lookups by using the marvellous [Postcodes.IO](https://postcodes.io/) geolocation APIs.

[![nuget](https://badgen.net/nuget/v/Postcod?icon=nuget)](https://www.nuget.org/packages/Postcod)

## How to use

Initialise a new `PostcodeLookupClient` and call the `Search` method with a postcode:

```
IPostcodeLookupClient client = new PostcodeLookupClient();
Location location = await client.Search("SN15 1HH");
```

The `Search` result will return a `Location` object, including the below details:

```
public class Location
{
        public string Postcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
	...
}
```

You can find full Console and Azure Function examples in the [Postcod GitHub repo](https://github.com/Benjlet/Postcod).
