// static void FooBar(int n)
// {
//     Dictionary<int, string> divisibleValue = new()
//     {
//         { 3, "foo"},
//         { 4, "baz"},
//         { 5, "bar"},
//         { 7, "jazz" },
//         { 9, "huzz"}
//     };

//     List<object> results = [];

//     for (int i = 1; i <= n; i++)
//     {
//         results.Add(i);

//         foreach (var kvp in new[] { 3, 5, 7, 9 })
//         {
//             if (i % kvp == 0)
//                 if (results[i - 1] is int)
//                     results[i - 1] = divisibleValue[kvp];
//                 else
//                     results[i - 1] += divisibleValue[kvp];
//         }
//     }

//     Console.WriteLine(string.Join(", ", results));
// }

// Console.WriteLine("---Logic Exercise---");
// Console.WriteLine("\nFor using parameter 15:");
// FooBar(15);
// Console.WriteLine("\nFor using parameter 21:");
// FooBar(21);
// Console.WriteLine("\nFor using parameter 35:");
// FooBar(35);
// Console.WriteLine("\nFor using parameter 105:");
// FooBar(105);
// Console.WriteLine("\nFor using parameter 315:");
// FooBar(315);

LogicExercise logicExercise = new();
logicExercise.AddRule(3, "foo");
logicExercise.AddRule(4, "baz");
logicExercise.AddRule(5, "bar");
logicExercise.AddRule(7, "jazz");
logicExercise.AddRule(9, "huzz");

Console.WriteLine("---Logic Exercise---");
Console.WriteLine("\nFor using parameter 15:");
logicExercise.Divisible(15);
Console.WriteLine("\nFor using parameter 21:");
logicExercise.Divisible(21);
Console.WriteLine("\nFor using parameter 35:");
logicExercise.Divisible(35);
Console.WriteLine("\nFor using parameter 105:");
logicExercise.Divisible(105);
Console.WriteLine("\nFor using parameter 315:");
logicExercise.Divisible(315);

class LogicExercise
{
    private readonly Dictionary<int, string> _divisibleValues = [];

    public LogicExercise() { }

    public void AddRule(int number, string output)
    {
        _divisibleValues.Add(number, output);
    }

    public void Divisible(int number)
    {
        List<object> results = [];

        for (int i = 1; i <= number; i++)
        {
            results.Add(i);

            foreach (var kvp in new[] { 3, 5, 7, 9 })
            {
                if (i % kvp == 0)
                    if (results[i - 1] is int)
                        results[i - 1] = _divisibleValues[kvp];
                    else
                        results[i - 1] += _divisibleValues[kvp];
            }
        }

        Console.WriteLine(string.Join(", ", results));
    }
}