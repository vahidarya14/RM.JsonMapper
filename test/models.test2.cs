namespace test;

public record Source2
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public x2 x { get; set; }
    public List<arr2> arr { get; set; }
}
public record x2
{
    public y2 y { get; set; }
}
public record y2
{
    public z2 z { get; set; }
}
public record z2
{
    public string title { get; set; }
}
public record arr2
{
    public string a { get; set; }
    public List<arr2c> c { get; set; }
}
public record arr2c
{
    public string a { get; set; }
    public string b { get; set; }
}

public record Dest2
{
    public string f { get; set; }
    public string t { get; set; }
    public p2 p { get; set; }
    public arr2 a1 { get; set; }
    public List<arr2c> a1c { get; set; }
    public arr2c a1c2 { get; set; }
    public string a1c1b { get; set; }
    public List<arr2> arr2 { get; set; }
}
public record p2
{
    public q2 q { get; set; }
}
public record q2
{
    public r2 r { get; set; }
}
public record r2
{
    public string f { get; set; }
    public string l { get; set; }
}