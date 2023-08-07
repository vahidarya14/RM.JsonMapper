# JsonMapper 

## Description
Map json object to another json dynamiclly using config  
download source and more example : https://github.com/vahidarya14/RM.JsonMapper

#### sample usage source:
with this config rule
```csharp
        string _config = @"
f: FirstName,
l: LastName,
t:x.y.z.title,
n.m.f:FirstName,
p.q.r.f:FirstName,
p.q.r.l:LastName,
c.c:ContactDetails.Country,
a2:arr[2],
a2b:arr[2].b,
arr:arr
";
```
there are two ways to map jsons

using this keeps [mapping config] as rule and does all  mapping using this config. no need to pass config as parameter every time
```csharp
 var mapper = new JsonMapper(_config);
```
or using **static function** without makeing instance of JsonMapper.it doen't keep any states and just fires and forgets
```csharp
    JsonMapper.Map<Source, Dest>(sourceObject, _config);
```

### more sample
```csharp
        var sourceObject=new Source()
        {
            //...
        };

        var destObj = new JsonMapper(_config).Map<Source, Dest>(sourceObject);
        var destObjUsingStatic = JsonMapper.Map<Source, Dest>(sourceObject, _config);
        var destObjUsingWithConfig = new JsonMapper().WithConfig(_config).Map<Source, Dest>(sourceObject);
```
or
```csharp
        string _json = @"{
        FirstName: ""Audrey"",
        ""LastName"": ""Spencer"",
        ""ContactDetails"": {
            ""Country"": ""Spain""
        },
        x:{
            y:{
                z:{
                    title:""t_54""
                }
            }
        },
        arr:[{a:'a1',b:'b1'},{a:'a2',b:'b2'},{a:'a3',b:'b3'}]
    }";

    var destObj = new JsonMapper(_config).Map< Dest>(_json);
    var destinationObject = JsonMapper.Map<Dest>(_json,_config);
```
for inline list mapping config
```
ArrTarget:ArrSource{
        AgeOfVehicle:Age,
        FullName:Name
    }
```

here is the complete sample
```csharp
 string _config = @"
Year: Year,
ArrTarget:ArrSource{
        AgeOfVehicle:Age,
        FullName:Name
    }
";
    _source =new Source3{
            Year=1986,
            ArrSource=new List<Arr3>
            {
                new Arr3(){Age=20,Name="ford"},
                new Arr3(){Age=30,Name="benz"}
            }
        };

 var destObj = new JsonMapper(_config).Map<Source3, Dest3>(_source);

```
