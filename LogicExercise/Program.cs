static void FooBar(int n)
{
    for (int i = 1; i < n + 1; i++)
    {
        if (i % 3 == 0 && i % 5 == 0)
            Console.Write("foobar" + LastCharacterCheck(i, n));
        else if (i % 3 == 0)
            Console.Write("foo" + LastCharacterCheck(i, n));
        else if (i % 5 == 0)
            Console.Write("bar" + LastCharacterCheck(i, n));
        else
            Console.Write(i + LastCharacterCheck(i, n));
    }
}

static string LastCharacterCheck(int target, int length)
{
    return target != length ? ", " : "";
}

FooBar(15);
