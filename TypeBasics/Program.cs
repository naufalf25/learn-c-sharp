using System.Security.Cryptography;

int x = 400000000;
int y = 900000000;
double z = x + y;
Console.WriteLine(z);

string message = "Hello World";

Console.WriteLine(message);


UnitConverter feetToInchesConverter = new(12);
Console.WriteLine(feetToInchesConverter.Convert(100));


Panda p1 = new("Pan Dee");
Panda p2 = new("Pan Deh");

Console.WriteLine(p1.Name);
Console.WriteLine(p2.Name);
Console.WriteLine(Panda.Population);

Point point = new();
point.X = 5;
Point point2 = point;
Console.WriteLine(point.X);
Console.WriteLine(point2.X);

point.X = 10;
Console.WriteLine(point.X);
Console.WriteLine(point2.X);

Point2 pointNew = new();
pointNew.X = 5;
Point pointNew2 = point;
Console.WriteLine(pointNew.X);
Console.WriteLine(pointNew2.X);

pointNew.X = 10;
Console.WriteLine(pointNew.X);
Console.WriteLine(pointNew2.X);

public class UnitConverter(int unitRatio)
{
    private readonly int ratio = unitRatio;

    public int Convert(int unit)
    {
        return unit * ratio;
    }
}

public class Panda()
{
    public string Name = "";
    public static int Population;

    public Panda(string n) : this()
    {
        Name = n;
        Population = ++Population;
    }
}

public struct Point { public int X, Y; }

public class Point2 { public int X, Y; }

