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
    }

    Console.WriteLine(string.Join(", ", results));
}

FooBar(15);
