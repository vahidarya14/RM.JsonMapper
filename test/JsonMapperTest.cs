using RM.JsonMapper;
using System.Text.Json;

namespace test;

public class JsonMapperTest
{
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
arr2:arr
";
    string _json =@"{
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
    Source _source;

    [SetUp]
    public void Setup()
    {
        _source = JsonSerializer.Deserialize<Source>(_json);
    }

    [Test]
    public void destObj_equals_destObjUsingStatic()
    {
        var destObj = new JsonMapper(_config).Map<Source, Dest>(_source);
        var destObjUsingStatic = JsonMapperBase.Map<Source, Dest>(_source, _config);

        Assert.That(destObj.c.c, Is.EqualTo(_source.ContactDetails.Country));
        Assert.That(destObj.n.m.f, Is.EqualTo(_source.FirstName));
        Assert.That(destObj.p.q.r.l, Is.EqualTo(_source.LastName));
        Assert.That(destObj.a2, Is.EqualTo(_source.arr[2]));
        Assert.That(destObj.a2b, Is.EqualTo(_source.arr[2].b));

        Assert.That(destObjUsingStatic.c, Is.EqualTo(destObj.c));
        Assert.That(destObjUsingStatic.n, Is.EqualTo(destObj.n));
        Assert.That(destObjUsingStatic.p, Is.EqualTo(destObj.p));
        Assert.That(destObjUsingStatic.a2, Is.EqualTo(destObj.a2));
    }

    [Test]
    public void destObj_equals_destObjUsingWithConfig()
    {
        var destObj = new JsonMapper(_config).Map<Source, Dest>(_source);
        var destObjUsingWithConfig = new JsonMapper("--").WithConfig(_config).Map<Source, Dest>(_source);


        Assert.AreEqual(destObj.c, destObjUsingWithConfig.c);
        Assert.AreEqual(destObj.n, destObjUsingWithConfig.n);
        Assert.AreEqual(destObj.p, destObjUsingWithConfig.p);
        Assert.AreEqual(destObj.a2, destObjUsingWithConfig.a2);
    }


    [Test]
    public void fromJson_of_Source_equals_Source_object()
    {
        var destObj = new JsonMapper(_config).Map< Dest>(_json);
        var destObjUsingWithConfig = new JsonMapper("--").WithConfig(_config).Map<Source, Dest>(_source);

        Assert.AreEqual(destObj.c, destObjUsingWithConfig.c);
        Assert.AreEqual(destObj.n, destObjUsingWithConfig.n);
        Assert.AreEqual(destObj.p, destObjUsingWithConfig.p);
        Assert.AreEqual(destObj.a2, destObjUsingWithConfig.a2);
    }

    [Test]
    public void fromJson_of_Source_equals_static_version()
    {
        var destObj = new JsonMapper(_config).Map<Dest>(_json);
        var destObjUsingStaticFunction = JsonMapperBase.Map<Dest>(_json, _config);

        Assert.That(destObjUsingStaticFunction.c, Is.EqualTo(destObj.c));
        Assert.That(destObjUsingStaticFunction.n, Is.EqualTo(destObj.n));
        Assert.That(destObjUsingStaticFunction.p, Is.EqualTo(destObj.p));
        Assert.That(destObjUsingStaticFunction.a2, Is.EqualTo(destObj.a2));
    }


    [Test]
    public void invalid_config_throw_execption()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new JsonMapper("--").Map<Source, Dest>(_source));

        Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentOutOfRangeException)));
        Assert.That(() => new JsonMapper("--").Map<Source, Dest>(_source),
                    Throws.Exception
                        .TypeOf<ArgumentOutOfRangeException>()
                        .With.Property("ParamName")
                        .EqualTo("index"));
    }

}
