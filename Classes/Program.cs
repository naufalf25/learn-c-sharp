using System.Text.Json;

User test = new User("Naufal");
Console.WriteLine(test.DisplayName());
test.DisplayRange(20);
test.DisplayRange(50.5);

User test2 = new User("Farras", 24);
Console.WriteLine(test2.DisplayName());
Console.WriteLine(test2.DisplayAge());


Rectangle rect = new Rectangle(20, 10);
(float width, float height) = rect;
Console.WriteLine($"The rectangle has width {width} and height {height}");


Bunny b1 = new Bunny { Name = "Bo", LikesCarrots = true, LikesHumans = false };
Bunny b2 = new Bunny("Yo") { LikesCarrots = true, LikesHumans = false };
Console.WriteLine(b1.Name);
Console.WriteLine(b2.Name);


Property property = new Property("Naufal");
property.propertyCount = 10;
property.DisplayOwnProperty();


public class User
{
    private readonly string _name;
    private readonly int _age;

    public User(string n)
    {
        this._name = n;
    }

    public User(string name, int age) : this(name) => _age = age;

    public string DisplayName()
    {
        return _name;
    }

    public int DisplayAge()
    {
        return _age;
    }

    public void DisplayRange(int range)
    {
        Console.WriteLine($"The range in {range.GetType().Name} is {range}");
    }

    public void DisplayRange(double range)
    {
        Console.WriteLine($"The range in {range.GetType().Name} is {range}");
    }
}

public class Rectangle
{
    private readonly float _width;
    private readonly float _height;

    public Rectangle(float width, float height)
    {
        this._width = width;
        this._height = height;
    }

    public void Deconstruct(out float width, out float height)
    {
        width = this._width;
        height = this._height;
    }
}

public class Bunny
{
    public string Name;
    public bool LikesCarrots;
    public bool LikesHumans;

    public Bunny() { }
    public Bunny(string n) => Name = n;
}

public class Property
{
    private string _ownerName;
    public int propertyCount { get; set; }

    public Property(string name)
    {
        this._ownerName = name;
    }

    public void DisplayOwnProperty()
    {
        var obj = new { name = _ownerName, propertyCount };
        Console.WriteLine("The results of Property:");

        // Console.WriteLine($@"{{""name"":""{obj.name}"",""propertyCount""{obj.propertyCount}}}"); 
        // Displaying object not effective using this method. Better using JSON format using package

        string json = JsonSerializer.Serialize(obj); // Better result
        Console.WriteLine(json);
    }
}
