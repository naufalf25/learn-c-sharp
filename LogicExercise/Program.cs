static void FooBar(int n)
{
    for (int i = 1; i < n + 1; i++)
    {
        if (i % 3 == 0 && i % 5 == 0)
            Console.Write("foobar" + LastCharCheck(i, n));
        else if (i % 3 == 0)
            Console.Write("foo" + LastCharCheck(i, n));
        else if (i % 5 == 0)
            Console.Write("bar" + LastCharCheck(i, n));
        else
            Console.Write(i + LastCharCheck(i, n));
    }
}

static string LastCharCheck(int target, int length)
{
    return target != length ? ", " : "";
}

FooBar(15);
