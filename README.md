# TaxCalc
> The Tax Calculator microservice using TaxJar as Provider

## Installing / Getting started

### Using Docker

```shell
cd TaxCalc/src
sudo docker build .

...
> Removing intermediate container f21478be663b
> ---> 4b222385d8e4
> Successfully built 4b222385d8e4
...

sudo docker run 4b222385d8e4

```

### Using Client Package

We can publish the client to the nuget repository, the project folder `src/TaxCalc.Client`

```
dotnet nuget push TaxCalc.Client.1.0.0.nupkg --api-key {APY_KEY} --source https://api.nuget.org/v3/index.json
```

If we have the nuget package, then we can import the package in our project and if we have the TaxCalc microservice runnign in the URL `https://taxcalcmicroservice.com`

```
using TaxCalc.Client;
using System;
using System.Threading.Tasks;

namespace TryTaxCalcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new TaxCalcClient("https://taxcalcmicroservice.com");
            var rate = await client.GetTaxRateForLocationAsync("99921");
            
            ...
        }
    }
}
```

Noted: `TaxCalc.Client` project is using `netstandard2.0`, that means that we can use this client on `.NET Framework 4.6.1` or higher and `.NET Core 2.0` or higher

## Third-Party Libraries

[https://automapper.org/](https://automapper.org/)

[https://fluentvalidation.net/](https://fluentvalidation.net/)

[https://restsharp.dev/](https://restsharp.dev/)
