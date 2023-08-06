public record Source
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ContactDetail ContactDetails { get; set; }
    public x x { get; set; }
    public List<arr> arr { get; set; }
}
public record ContactDetail
{
    public string Country { get; set; }
}
public record x
{
    public y y { get; set; }
}
public record y
{
    public z z { get; set; }
}
public record z
{
    public string title { get; set; }
}
public record arr
{
    public string a { get; set; }
    public string b { get; set; }
}

public record Dest
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
public record c2
{
    public string c { get; set; }
}
public record n
{
    public m m { get; set; }
}
public record m
{
    public string f { get; set; }
}
public record p
{
    public q q { get; set; }
}
public record q
{
    public r r { get; set; }
}
public record r
{
    public string f { get; set; }
    public string l { get; set; }
}