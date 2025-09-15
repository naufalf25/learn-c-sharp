using Exception = System.Exception;

// Try Statement and Exception
Console.WriteLine
("\n============Try Statement and Exception===========");

// Example: Handling Division by Zero
Console.WriteLine("---Example: Handling Division by Zero---");
int Calc(int x) => 10 / x;

try
{
    int y = Calc(0);
    Console.WriteLine(y);
}
catch (DividedByZeroException ex)
{
    Console.WriteLine("Error: x cannot be zero.");
}
Console.WriteLine("Program completed.");
