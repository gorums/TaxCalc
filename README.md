# TaxCalc
> The Tax Service microservice using TaxJar as Provider

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

### Using Client

We need to publish the client to the nuget repository

```
dotnet nuget push TaxCalc.Client.1.0.0.nupkg --api-key {APY_KEY} --source https://api.nuget.org/v3/index.json
```

If we have the nuget package, then we can import the package in our project and if we have the TaxCalc microservice runnign in the URl `https://taxcalcmicroservice.com`

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