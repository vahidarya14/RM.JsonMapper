# JsonMapper 

## Description

Map json object to another json dynamiclly using config  

#### sample usage source:
```
        var json =
    @"{
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
        var config = @"
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


        var jRes =new JsonMapper().Map(json,config);
```