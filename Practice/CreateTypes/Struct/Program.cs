// Default Contructor
Point p1 = new Point();
Console.WriteLine(p1.y);

Point p2 = default;
Console.WriteLine(p2.x);
Console.WriteLine(p2.y);

// p2.x = 10; // should be error because read only

readonly struct Point
{
    public readonly int x = 1;
    public readonly int y;

    public Point() => y = 1;
}
