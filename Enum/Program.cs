Console.WriteLine("---Basic of Enum---");
Console.Write("Is this the top?");
BorderSide topSide = BorderSide.Top;
string isTop = topSide == BorderSide.Top ? "YES" : "NO";
Console.WriteLine(" " + isTop);
Console.WriteLine($"\nDefault Hash Code of Left is: {BorderSide.Left.GetHashCode()}");
Console.WriteLine($"Default Hash Code of of Right is: {BorderSide.Right.GetHashCode()}");

Console.WriteLine("\n---Enum Conversion---");
Console.WriteLine("The right value will be 2");
Console.Write("Actual value of right is: ");
Console.WriteLine((int)HorizontalAlignment.Right.GetHashCode());
Console.WriteLine("Center will get 3 value");
Console.Write("Actual value of Center is: ");
Console.WriteLine((int)HorizontalAlignment.Center);

Console.WriteLine("\n---Flags Enums---");
BorderSides leftRight = BorderSides.Left | BorderSides.Right;
if ((leftRight & BorderSides.Left) != 0)
    Console.WriteLine("Includes Left");
string formatted = leftRight.ToString(); // Thanks to [Flags]
Console.WriteLine(formatted);
BorderSides s = BorderSides.Left;
s |= BorderSides.Right;
Console.WriteLine(s == leftRight);
s ^= BorderSides.Right;
Console.WriteLine(s);

public enum BorderSide : byte { Left = 1, Right, Top = 10, Bottom };

public enum HorizontalAlignment
{
    Left = BorderSide.Left,
    Right = BorderSide.Right,
    Center,
}

[Flags]
enum BorderSides
{
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8,
}
