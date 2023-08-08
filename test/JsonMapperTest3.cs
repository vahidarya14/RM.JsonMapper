using RM.JsonMapper;

namespace test;

public class JsonMapperTest3
{
    string _config = @"
Year: Year,
Arr:Arr{
        AgeOfVehicle:Age,
        FullName:Name
    }
";
    Source3 _source;

    [SetUp]
    public void Setup()
    {
        _source =new Source3{
            Year=1986,
            Arr=new List<Arr3>
            {
                new Arr3(){Age=20,Name="ford"},
                new Arr3(){Age=30,Name="benz"}
            }
        };
    }


    [Test]
    public void inner_list_cinfig_support()
    {
        var destObj = new JsonMapper(_config).Map< Dest3>(_source);
        Assert.Multiple(() =>
        {
            Assert.That(destObj.Year, Is.EqualTo(_source.Year));
            for (int i = 0; i < destObj.Arr.Count; i++)
            {
                Assert.That(destObj.Arr[i].AgeOfVehicle, Is.EqualTo(_source.Arr[i].Age));
                Assert.That(destObj.Arr[i].FullName, Is.EqualTo(_source.Arr[i].Name));
            }
        });
    }

}

class Source3
{
    public int Year { get; set; }
    public List<Arr3> Arr { get; set; }
}
class Arr3
{
    public int Age { get; set; }
    public string Name { get; set; }
}

class Dest3
{
    public int Year { get; set; }
    public List<ArrDest3> Arr { get; set; }
}
class ArrDest3
{
    public int AgeOfVehicle { get; set; }
    public string FullName { get; set; }
}