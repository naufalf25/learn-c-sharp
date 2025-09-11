// Generics Type
using System.Text;

Console.WriteLine("---Generic Type---");
var stack = new Stack<int>();
stack.Push(5);
stack.Push(10);
int x = stack.Pop();
int y = stack.Pop();
Console.WriteLine(x);
Console.WriteLine(y);
// stack.Push("hello"); // Will got error because this is not int

// Generic Methods
Console.WriteLine("\n---Generic Methods---");
int x2 = 5;
int y2 = 10;
Swap(ref x2, ref y2);
static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
Console.WriteLine(x2);
Console.WriteLine(y2);

// Default Generic Value
Console.WriteLine("\n---Default Generic Value---");
Zap<int>([1, 2, 3, 4, 5, 6]);
static void Zap<T>(T[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        array[i] = default(T);
        Console.WriteLine(array[i]);
    }
}

// Static Data in Generic Types
Console.WriteLine("\n---Static Data in Generic Types---");
Console.WriteLine(++Bob<int>.Count); // 1
Console.WriteLine(++Bob<int>.Count); // 2
Console.WriteLine(++Bob<string>.Count); // 1
Console.WriteLine(++Bob<double>.Count); // 1

// Type Parameters and Conversions
Console.WriteLine("\n---Type Parameters and Conversions---");
static StringBuilder? Foo<T>(T arg)
{
    // return (StringBuilder)arg; // Will Compile-time error
    StringBuilder? sb = arg as StringBuilder; // OK
    if (sb != null) return sb;
    return null;

    // return (StringBuilder)(object)arg; // It's also OK
}

public class Stack<T>
{
    int position;
    T[] data = new T[100];

    public void Push(T obj) => data[position++] = obj;
    public T Pop() => data[position--];
}

// Self-Referencing Generic Declarations
public interface IEquatable<T> { bool Equals(T obj); }
public class Ballon : IEquatable<Ballon>
{
    public string Color { get; set; } = "";
    public int CC { get; set; }
    public bool Equals(Ballon b)
    {
        if (b == null) return false;
        return b.Color == Color && b.CC == CC;
    }
}

class Bob<T> { public static int Count; }
