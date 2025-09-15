static void FooBar(int n)
{
    List<object> results = [];

    for (int i = 1; i < n + 1; i++)
    {
        if (i % 3 == 0 && i % 5 == 0)
            results.Add("foobar");
        else if (i % 3 == 0)
            results.Add("foo");
        else if (i % 5 == 0)
            results.Add("bar");
        else
            results.Add(i);

        if (i == n && i % 7 == 0)
            results[i - 1] += "jazz";
    }

    Console.WriteLine(string.Join(", ", results));
}

Console.WriteLine("---Logic Exercise---");
Console.WriteLine("\nFor using parameter 15:");
FooBar(15);
Console.WriteLine("\nFor using parameter 21:");
FooBar(21);
Console.WriteLine("\nFor using parameter 35:");
FooBar(35);
Console.WriteLine("\nFor using parameter 105:");
FooBar(105);
