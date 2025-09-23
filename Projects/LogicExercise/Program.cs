static void FooBar(int n)
{
    Dictionary<int, string> divisibleValue = new()
    {
        { 3, "foo"},
        { 4, "baz"},
        { 5, "bar"},
        { 7, "jazz" },
        { 9, "huzz"}
    };

    List<object> results = [];

    for (int i = 1; i <= n; i++)
    {
        results.Add(i);

        foreach (var kvp in new[] { 3, 5, 7, 9 })
        {
            if (i % kvp == 0)
                if (results[i - 1] is int)
                    results[i - 1] = divisibleValue[kvp];
                else
                    results[i - 1] += divisibleValue[kvp];
        }
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
Console.WriteLine("\nFor using parameter 315:");
FooBar(315);