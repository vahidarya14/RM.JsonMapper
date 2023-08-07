using RM.JsonMapper;

namespace test;

public class JsonMapperTest3
{
    string _config = @"
f: FirstName,
t:x.y.z.title,
p.q.r.f:arr[1].c[1].a,
p.q.r.l:arr[1].c[1].b,
a1:arr[1],
a1c:arr[1].c,
a1c2:arr[1].c[2],
a1c1b:arr[1].c[1].b,
arr2:arr
";
    List<Source2> _source;

    [SetUp]
    public void Setup()
    {
        _source = new List<Source2>{
            new Source2
            {
                FirstName = "vahid",
                LastName = "arya",
                x = new x2
                {
                    y = new y2
                    {
                        z = new z2 { title = "t_54" }
                    }
                },
                arr = new List<arr2>
                {
                    new arr2{a="a1",c=new List<arr2c>{new arr2c{a="a1",b="b1"}, new arr2c { a = "a2", b = "b2" }, new arr2c { a = "a3", b = "b3" } } },
                    new arr2{a="a2",c=new List<arr2c>{new arr2c{a="a1",b="b1"}, new arr2c { a = "a2", b = "b2" }, new arr2c { a = "a3", b = "b3" } } }
                }
            }
        };
    }

    [Test]
    public void destObj_equals_destObjUsingStatic()
    {
        var destObj = new JsonMapper(_config).MapList<Source2, Dest2>(_source);


        Assert.That (_source.Count, Is.EqualTo(destObj.Count));
    }


}