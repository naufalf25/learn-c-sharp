char[] test = ['a', 'i', 'u', 'e', 'o'];

Console.WriteLine(string.Join(", ", test));
Console.WriteLine(test.GetType());
Console.WriteLine(test[2]);
Console.WriteLine(test[..2]);
Console.WriteLine(test[1..4]);
Console.WriteLine(test[^2..]);
Console.WriteLine(test[^2]);
Console.WriteLine();

List<string> test2 = ["a", "i", "u", "e", "o"];

Console.WriteLine(string.Join(", ", test2));
Console.WriteLine(test2.GetType());
Console.WriteLine(test2[2]);
Console.WriteLine(string.Join("", test2[..2].ToArray()));
Console.WriteLine(string.Join("", test2[1..4].ToArray()));
Console.WriteLine(string.Join("", test2[^2..].ToArray()));
Console.WriteLine(test2[^2]);