using System;
using System.Text;

static int Factorial(int x)
{
    if (x == 0) return 1;
    return x * Factorial(x - 1);
}

Console.WriteLine(Factorial(7));

StringBuilder ref1 = new("object1");
Console.WriteLine(ref1);

StringBuilder ref2 = new("object2");
StringBuilder ref3 = ref2;

Console.WriteLine(ref3);

string a, b;
Split("Muhammad Naufal Farras", out a, out b);
Console.WriteLine(a);
Console.WriteLine(b);

void Split(string name, out string firstName, out string lastName)
{
    int i = name.LastIndexOf(" ");
    firstName = name.Substring(0, i);
    lastName = name.Substring(i + 1);
}

Console.WriteLine(new Test());

class Test()
{
    static int x;
    static void Main() { Foo(out x); }
    static void Foo(out int y)
    {
        Console.WriteLine(x);
        y = 1;
        Console.WriteLine(y);
    }
}
