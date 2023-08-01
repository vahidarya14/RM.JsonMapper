using RM.JsonMapper;
using System.Text.Json;

namespace test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var json =
@"{
        ""FirstName"": ""Audrey"",
        ""LastName"": ""Spencer"",
        ""ContactDetails"": {
            ""Country"": ""Spain""
        },
        ""x"":{
            ""y"":{
                ""z"":{
                    ""title"":""t_54""
                }
            }
        },
        ""arr"":[{""a"":""a1"",""b"":""b1""},{""a"":""a2"",""b"":""b2""},{""a"":""a3"",""b"":""b3""}]
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
arr2:arr
";
        var s = JsonSerializer.Deserialize<Source>(json);
        var aaa = new JsonMapper().Map<Source, Dest>(s, config);

        Assert.Pass();
    }
}


public class Source
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ContactDetail ContactDetails { get; set; }
    public x x { get; set; }
    public List<arr> arr { get; set; }
}
public class ContactDetail
{
    public string Country { get; set; }
}
public class x
{
    public y y { get; set; }
}
public class y
{
    public z z { get; set; }
}
public class z
{
    public string title { get; set; }
}
public class arr
{
    public string a { get; set; }
    public string b { get; set; }
}

public class Dest
{
    public string f { get; set; }
    public string l { get; set; }
    public string t { get; set; }
    public c2 c { get; set; }
    public n n { get; set; }
    public p p { get; set; }
    public arr a2 { get; set; }
    public string a2b { get; set; }
    public List<arr> arr2 { get; set; }
}
public class c2
{
    public string c { get; set; }
}
public class n
{
    public m m { get; set; }
}
public class m
{
    public string f { get; set; }
}
public class p
{
    public q q { get; set; }
}
public class q
{
    public r r { get; set; }
}
public class r
{
    public string f { get; set; }
    public string l { get; set; }
}