// Define and Using Delegate
Console.WriteLine("\n---Define and Using Delegate---");

static int Square(int x) => x * x; // method that match the delegate

Transformer t = Square; // t nw points to the Square method
// shorthand of Transformer t = new Transformer(Square);

// invoke delegated instance
int answer = t(3);
Console.Write("The answer from t will be: ");
Console.WriteLine(answer); // Output: 9

Console.WriteLine("\n==================================================\n");


// Write Plug-in Methods with Delegates
Console.WriteLine("---Write Plug-in Methods with Delegates---");
int Cube(int x) => x * x * x; // new method following the delegate

void Transform(int[] values, Transformer t)
{
    for (int i = 0; i < values.Length; i++)
        values[i] = t(values[i]); // Invoke the plug-in method
}

int[] values = { 1, 2, 3 };

Transform(values, Square); // Use square method as the plug-in
Console.WriteLine("The output of Square as plug-in method is:");
foreach (int i in values)
    Console.Write(i + "  ");
Console.WriteLine();

Transform(values, Cube);
Console.WriteLine("The output of Cube as plug-in method is:");
foreach (int i in values)
    Console.Write(i + "  ");
Console.WriteLine();

Console.WriteLine("\n==================================================\n");


// Instance and Static Method Targets
Console.WriteLine("---Instance and Static Method Targets---");

// Static method
Transformer t2 = Test.Square; // Referencing a static method
Console.Write("The output of referencing static method with param 10 is: ");
Console.WriteLine(t2(10)); // Output: 100

// Instance method
Test2 test2 = new Test2();
Transformer t3 = test2.Square; // Referencing an instance method on a specific 'test2' object;
Console.Write("The output of referencing instance method with param 10 is: ");
Console.WriteLine(t3(10)); // Output same as t2

Console.WriteLine("\n==================================================\n");


// Multicast delegates
Console.WriteLine("---Multicast Delegates---");

void SomeMethod1() { Console.WriteLine("Method 1"); }
void SomeMethod2() { Console.WriteLine("Method 2"); }

Console.WriteLine("Both Method1 and Method2 will be called after invoke:");
SomeDelegated d = SomeMethod1; // d point to SomeMethod1
d += SomeMethod2; // d now points to both Method1 and Method2
d.Invoke(); // Invoke both method

Console.WriteLine("\nAdd new point to Method1 and will be called after Method1 and Method2 before calling:");
d += SomeMethod1; // add new point to Method1
d.Invoke();

Console.WriteLine("\nRemove point to Method2 and after invoke will only call Method1 twice:");
d -= SomeMethod2;
d.Invoke();

Console.WriteLine("\n\nExample Progress Reporter Delegate");
void WriteProgressToConsole(int percentComplete) => Console.WriteLine($"Console: {percentComplete}%");
void WriteProgressToFile(int percentComplete) => File.WriteAllText("progress.txt", percentComplete.ToString());

ProgressReporter p = WriteProgressToConsole; // Start with console reporting
p += WriteProgressToFile; // Add file reporting

Util.HardWork(p); // Both methods will be called

Console.WriteLine("\n==================================================\n");


// Generic Delegate Types
Console.WriteLine("---Generic Delegate Types---");
int[] values2 = { 1, 2, 3 };
int[] values3 = { 1, 2, 3 };

Console.WriteLine("The result of Generic Delegate Types is:");
Util2.Transform(values2, Square); // using Square and values from above
// Func delegate
static void Transform2<T>(T[] values, Func<T, T> transformer)
{
    for (int i = 0; i < values.Length; i++)
    {
        Console.Write(values[i]);
        Console.WriteLine(" (Before)");
        values[i] = transformer(values[i]);
        Console.Write(values[i]);
        Console.WriteLine(" (After)");
    }
}
Console.WriteLine("For using func delegate:");
Transform2(values3, Square);

Console.WriteLine("\n==================================================\n");

// Delegates vs Interfaces
Console.WriteLine("---Delegates vs Interfaces---");
int[] values4 = { 1, 2, 3 };

Console.WriteLine("The results of using interfaces instead delegate is:");
Util3.TransformAll(values4, new Squarer()); // Use an object implementing the interface

Console.WriteLine("\n==================================================\n");

// Delegate Compatibility (Variance)
Console.WriteLine("---Delegate Compatibility (Variance)---");
void Method3() => Console.WriteLine("This is Method3");

D1 d1 = Method3;
// D2 d2 = d1; // Will result Compile-time error: different types of D1 and D2
D2 d2 = new D2(d1); // OK: Can contruct a new D2 from an exciting D1 delegate instance
Console.Write("Result from d1: ");
d1.Invoke();
Console.Write("Result from d2 (Same as d1 because contruct new D2 from exciting D1): ");
d2.Invoke();

Console.WriteLine("\n==================================================\n");

// Parameter Compatibility (Contravariance)
Console.WriteLine("---Parameter Compatibility (Contravariance)---");
void ActOnObject(object o) => Console.WriteLine(o);

StringAction sa = new(ActOnObject); // Legal: string (more specific) can be passed to object
Console.Write("The result of StringAction is: ");
sa("hello");

Console.WriteLine("\n==================================================\n");

// Return Type Compatibility (Covariance)
Console.WriteLine("---Return Type Compatibility---");
string RetrieveString() => "hello";

ObjectRetriever o = new(RetrieveString); // Legal: object (more general) can receive string (more specific)
object result = o();
Console.Write("The result of ObjectRetriever is: ");
Console.WriteLine(result);


delegate int Transformer(int x); // Defines a delegate type named Transformer

class Test { public static int Square(int x) => x * x; } // For static method target
class Test2 { public int Square(int x) => x * x; } // For instance method target

delegate void SomeDelegated(); // For multicast delegates
public delegate void ProgressReporter(int percentComplete); // Example delegate for multicast
public class Util
{
    public static void HardWork(ProgressReporter p)
    {
        for (int i = 0; i < 11; i++)
        {
            p(i * 10); // Invoke the delegate to report progress
            System.Threading.Thread.Sleep(100); // Simulate work
        }
    }
}

public delegate TResult Transformer2<Targ, TResult>(Targ arg); // Generic delegate type
public class Util2 // For Generic Delegate Types
{
    public static void Transform<T>(T[] values, Transformer2<T, T> t) // Uses generic delegate
    {
        for (int i = 0; i < values.Length; i++)
        {
            Console.Write(values[i]);
            Console.WriteLine(" (Before)");
            values[i] = t(values[i]);
            Console.Write(values[i]);
            Console.WriteLine(" (After)");
        }
    }
}

// Delegates vs Interfaces
public interface ITransformer
{
    int Transform(int x);
}
public class Util3
{
    public static void TransformAll(int[] values, ITransformer t)
    {
        for (int i = 0; i < values.Length; i++)
        {
            Console.Write(values[i]);
            Console.WriteLine(" (Before)");
            values[i] = t.Transform(values[i]);
            Console.Write(values[i]);
            Console.WriteLine(" (After)");
        }
    }
}
class Squarer : ITransformer
{
    public int Transform(int x) => x * x;
}

// Delegate Compability (Variance)
delegate void D1();
delegate void D2();

// Parameter Compatibility (Contravariance)
delegate void StringAction(string s);

// Return Type Compatibility (Covariance)
delegate object ObjectRetriever();
