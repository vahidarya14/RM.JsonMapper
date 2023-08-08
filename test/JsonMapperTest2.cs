using RM.JsonMapper;

namespace test;

public class JsonMapperTest2
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
    Source2 _source;
    List<Source2> _sourceList;

    [SetUp]
    public void Setup()
    {
        _source = new Source2
        {
            FirstName = "vahid",
            LastName = "arya",
            x=new x2
            {
                y = new y2
                {
                    z=new z2 { title= "t_54" }
                }
            },
            arr = new List<arr2>
            {
                new arr2{a="a1",c=new List<arr2c>{new arr2c{a="a1",b="b1"}, new arr2c { a = "a2", b = "b2" }, new arr2c { a = "a3", b = "b3" } } },
                new arr2{a="a2",c=new List<arr2c>{new arr2c{a="a1",b="b1"}, new arr2c { a = "a2", b = "b2" }, new arr2c { a = "a3", b = "b3" } } }
            }
        };

        _sourceList = new List<Source2>{
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
        var destObj = new JsonMapper(_config).Map< Dest2>(_source);
        Assert.Multiple(() =>
        {
            Assert.That(destObj.p.q.r.f, Is.EqualTo(_source.arr[1].c[1].a));
            Assert.That(destObj.p.q.r.l, Is.EqualTo(_source.arr[1].c[1].b));
            Assert.That(destObj.a1.a, Is.EqualTo(_source.arr[1].a));
            Assert.That(destObj.a1.c.Count, Is.EqualTo(_source.arr[1].c.Count));
            Assert.That(destObj.a1c1b, Is.EqualTo(_source.arr[1].c[1].b));
            Assert.That(destObj.arr2.Count, Is.EqualTo(_source.arr.Count));
        });
    }


    //[Test]
    //public void list_mapping()
    //{
    //    var destObj = new JsonMapper(_config).MapList<Source2, Dest2>(_sourceList);


    //    Assert.That(_sourceList.Count, Is.EqualTo(destObj.Count));
    //}

    [Test]
    public void list_mapping2()
    {
        var destObj = new JsonMapper(_config).MapList<Dest2>(_sourceList);

        Assert.That(_sourceList.Count, Is.EqualTo(destObj.Count));
    }

}
