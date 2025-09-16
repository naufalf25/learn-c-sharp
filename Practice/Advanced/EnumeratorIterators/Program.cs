using System.Collections;

Console.WriteLine("=====Enumerator and Iterators=====\n");

// Basic Enumeration
Console.WriteLine("--- Basic Enumeration ---");

// The foreach statement is the high-level way to iterate
string word = "house";
Console.WriteLine($"Iterating through the string '{word}':");

foreach (char c in word)
{
    Console.WriteLine($"  Character: {c}");
}

Console.WriteLine("\n-------------------------------------------\n");

// Manual Enumeration
Console.WriteLine("--- Manual Enumeration (what foreach does behind) ---");

Console.WriteLine($"Manually iterating through '{word}' using GetEnumerator():");

// This is waht the compiler generates for foreach statements
using (var enumerator = word.GetEnumerator())
{
    while (enumerator.MoveNext())
    {
        var element = enumerator.Current;
        Console.WriteLine($"  Character: {element}");
    }
} // Dispose is called automatically due to 'using'

Console.WriteLine("\n-------------------------------------------\n");

// Countdown Enumerable and Enumerator
Console.WriteLine("--- Custom Enumerable and Enumerator ---");

var countdown = new CountdownSequence(10);
Console.WriteLine("Custom countdown sequence from 10 to 1:");

foreach (int number in countdown)
{
    Console.WriteLine($"  Count: {number}");
}

Console.WriteLine("\n-------------------------------------------\n");

// Collection Initializers and Expressions
Console.WriteLine("--- Collection Initializers ---");

// Collection initializer syntax - syntatic sugar for calling Add() method
var numbers = new List<int> { 1, 2, 3, 4, 5 };
Console.WriteLine("List created with collection initializer:");
foreach (int num in numbers)
{
    Console.Write($"{num} ");
}
Console.WriteLine();

// Dictionary with collection initializer
var colors = new Dictionary<string, string>
{
    {"red", "#FF0000"},
    { "green", "#00FF00"},
    { "blue", "#0000FF"}
};

// Dictionary with indexer syntax
var moreColors = new Dictionary<string, string>
{
    ["yellow"] = "#FFFF00",
    ["purple"] = "#800080",
    ["orange"] = "#FFA500"
};

Console.WriteLine("Dictionary colors:");
foreach (var kvp in colors)
{
    Console.WriteLine($"  {kvp.Key}: {kvp.Value} (Collection Initializer)");
}
foreach (var kvp in moreColors)
{
    Console.WriteLine($"  {kvp.Key}: {kvp.Value} (Indexer Syntax)");
}

Console.WriteLine("\n-------------------------------------------\n");

// Collection Expression
Console.WriteLine("--- Collection Expression (C# 12++) ---");

// Collection expressions use square bracket and are target-typed
List<int> list = [1, 2, 3, 4, 5];
int[] array = [10, 20, 30];

Console.WriteLine("List created with collection expression:");
foreach (int num in list)
{
    Console.Write($"{num} ");
}
Console.WriteLine();

Console.WriteLine("Array created with collection expression:");
foreach (int num in array)
{
    Console.Write($"{num} ");
}

Console.WriteLine("\n-------------------------------------------\n");

// Iterator Methods
Console.WriteLine("--- Iterator Methods with yield ---");

Console.WriteLine("Fibonacci sequence (first 16 numbers):");
foreach (int fib in GenerateFibonacci(16))
{
    Console.Write($"{fib} ");
}
Console.WriteLine();

Console.WriteLine("Squares of numbers 1-10:");
foreach (int square in GenerateSquares(10))
{
    Console.Write($"{square} ");
}

Console.WriteLine("\n-------------------------------------------\n");


// Iterator method that generates Fibonacci sequence
// Notice: this method doesn't execute immediately when called
// It returns an IEnumerable that produces values on-demand
static IEnumerable<int> GenerateFibonacci(int count)
{
    if (count <= 0) yield break; // Early termination with yield break

    int previous = 0, current = 1;

    for (int i = 0; i < count; i++)
    {
        yield return current; // Yields current value and pauses execution

        // Calculate next Fibonacci number
        int next = previous + current;
        previous = current;
        current = next;
    }
}

// Another iterator example
static IEnumerable<int> GenerateSquares(int count)
{
    for (int i = 1; i <= count; i++)
    {
        yield return i * i; // Yields the square of i
    }
}

// Composing Sequences
Console.WriteLine("--- Composing Sequences ---");

// Chain iterators together for powerful data processing
var fibonacci = GenerateFibonacci(15);
var evenFibs = FilterEvenNumbers(fibonacci);
var limitedEvenFibs = TakeFirst(evenFibs, 4);

Console.WriteLine("First 4 even Fibonacci numbers:");
foreach (int num in limitedEvenFibs)
{
    Console.Write($"{num} ");
}

Console.WriteLine("\n-------------------------------------------\n");

// Iterator that filters for even number only
static IEnumerable<int> FilterEvenNumbers(IEnumerable<int> source)
{
    foreach (int number in source)
    {
        if (number % 2 == 0)
        {
            yield return number; // Only yield even numbers
        }
    }
}

// Iterator that takes only the first N elements
static IEnumerable<T> TakeFirst<T>(IEnumerable<T> source, int count)
{
    int taken = 0;
    foreach (T item in source)
    {
        if (taken >= count)
            yield break; // Stop when we've taken enough

        yield return item;
        taken++;
    }
}

// Iterator with try-finally (Resource Management)
Console.WriteLine("--- Iterator with try-finally (Resource Management) ---");

Console.WriteLine("Processing numbers with cleanup:");
foreach (string result in ProcessNumbersWithCleanUp([1, 2, 3]))
{
    Console.WriteLine($"  {result}");
}

Console.WriteLine("\n-------------------------------------------\n");

// Iterator that demonstrate proper resource management
// The finally block executes when enumeration completes or is disposed
static IEnumerable<string> ProcessNumbersWithCleanUp(IEnumerable<int> numbers)
{
    Console.WriteLine("  Setup: Initializing resources...");

    try
    {
        foreach (int number in numbers)
        {
            // yield return can appear in try block with finally
            yield return $"Processed: {number * 2}";
        }
    }
    finally
    {
        // This executes when enumeration ends or enumerator is disposed
        Console.WriteLine("  Cleanup: Resources released");
    }
}


#region Helper

// Custom enumerable class
public class CountdownSequence : IEnumerable<int>
{
    private readonly int _start;

    public CountdownSequence(int start)
    {
        _start = start;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return new CountdownEnumerator(_start);
    }

    // Non-generic version
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// Custom enumerator that implements the cursor logic
public class CountdownEnumerator : IEnumerator<int>
{
    private readonly int _start;
    private int _current;
    private bool _started = false;

    public CountdownEnumerator(int start)
    {
        _start = start;
        _current = start + 1; // Start one above so first MoveNext() gives correct value
    }

    // Current element at cursor position
    public int Current { get; private set; }

    // Non-generic version
    object IEnumerator.Current => Current;

    // Move cursor to next position, return true if successful
    public bool MoveNext()
    {
        if (!_started)
        {
            _started = true;
            _current = _start;
        }
        else
        {
            _current--;
        }

        if (_current >= 1)
        {
            Current = _current;
            return true;
        }

        return false; // End of sequence reached
    }

    // Reset cursor to beginning
    public void Reset()
    {
        _current = _start + 1;
        _started = false;
    }

    // Clean up resources when enumeration is done
    public void Dispose()
    {
        // No need cleanup in this example
        // This where release resources like file handles, database connections, etc.
    }
}

#endregion