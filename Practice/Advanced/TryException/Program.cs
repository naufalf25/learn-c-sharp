// Try Statement and Exception
using System.Collections.ObjectModel;
using System.Net;

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
catch (DivideByZeroException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occured: {ex.Message}");
}
Console.WriteLine("Program completed.");

Console.WriteLine("\n==================================================\n");

// Multiple Catch Block
Console.WriteLine("---Multiple Catch Block---");

string[] testArgs = { "300" };
Console.WriteLine($"Testing with argument: '{testArgs[0]}'");

try
{
    byte b = byte.Parse(testArgs[0]);
    Console.WriteLine($"Successfully parsed: {b}");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Error: Please provide at least one argument");
}
catch (FormatException)
{
    Console.WriteLine("Error: That's not a valid number!");
}
catch (OverflowException)
{
    Console.WriteLine("Error: The number is too large to fit in a byte (max: 255)!");
}
catch (Exception ex) // General catch-all (should be last)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}

// Test different scenarios
Console.WriteLine("\nTesting different error scenarios:");
TestParsingScenarios();
Console.WriteLine();

static void TestParsingScenarios()
{
    string[] testCases = { "100", "abc", "500", "" };

    foreach (string testCase in testCases)
    {
        Console.WriteLine($"    Testing '{testCase}':");
        try
        {
            byte result = byte.Parse(testCase);
            Console.WriteLine($"      Success: {result}");
        }
        catch (FormatException)
        {
            Console.WriteLine("      Error: Invalid format");
        }
        catch (OverflowException)
        {
            Console.WriteLine("      Error: Number too large for byte");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("      Error: Empty string");
        }
    }
}

Console.WriteLine("\n==================================================\n");

// Exception Filters
Console.WriteLine("---Exception Filters---");

Console.WriteLine("Testing exception filters with 'when' keyword:");

SimulateWebException(WebExceptionStatus.Timeout);
SimulateWebException(WebExceptionStatus.SendFailure);
SimulateWebException(WebExceptionStatus.ConnectFailure);

static void SimulateWebException(WebExceptionStatus status)
{
    try
    {
        var ex = new WebException("Simulate web error", status);
        throw ex;
    }
    catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
    {
        Console.WriteLine("  Handled: Request timout - retrying with longer timeout");
    }
    catch (WebException ex) when (ex.Status == WebExceptionStatus.SendFailure)
    {
        Console.WriteLine("  Handled: Send failure - checking network connection");
    }
    catch (WebException ex) when (ex.Status == WebExceptionStatus.ConnectFailure)
    {
        Console.WriteLine("  Handled: Connection failure - server might be down");
    }
    catch (WebException ex)
    {
        Console.WriteLine($"  Handled: Other web exception - {ex.Status}");
    }
}

Console.WriteLine("\n==================================================\n");

// Finally block
Console.WriteLine("---Finally Block---");
Console.WriteLine("Testing finally block execution:");

// Test scenario 1: No exception
Console.WriteLine("Scenario 1: Normal execution");
TestFinallyBlock(false);

// Test scenario 2: With exception
Console.WriteLine("\nScenario 2: With exception");
TestFinallyBlock(true);

static void TestFinallyBlock(bool throwException)
{
    string resource = null;

    try
    {
        Console.WriteLine("  Acquiring resource...");
        resource = "Important Resource";

        if (throwException)
        {
            throw new InvalidOperationException("Simulated error");
        }

        Console.WriteLine("  Using resource successfully");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"  Caugh exception: {ex.Message}");
    }
    finally
    {
        // This ALWAYS runs, regardless of exceptions
        if (resource != null)
        {
            Console.WriteLine("  Finally block: Cleaning up resource");
            resource = null; // Simulate cleanup
        }
        else
        {
            Console.WriteLine("  Finally block: No resource to clean up");
        }

        Console.WriteLine("  Method completed");
    }
}

Console.WriteLine("\n==================================================\n");

// Using Statement
Console.WriteLine("---Using Statement---");
Console.WriteLine("Comparing manual resource management vs using statement:");

// Manual way
Console.WriteLine("\nManual resource management:");
ReadFileManually();

// Using statement way
Console.WriteLine("\nUsing statement approach:");
ReadFileWithUsing();

static void ReadFileManually()
{
    StreamWriter writer = null;
    try
    {
        writer = new StreamWriter("manual_test.txt");
        writer.WriteLine("This file was created manually");
        Console.WriteLine("  File written successfully (manual way)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  Error writing file: {ex.Message}");
    }
    finally
    {
        // Must remember to dispose manually
        if (writer != null)
        {
            writer.Dispose();
            Console.WriteLine("  Resource disposed manually in finally block");
        }
    }
}

static void ReadFileWithUsing()
{
    try
    {
        // Using statement automatically calls Dispose()
        using (StreamWriter writer = new StreamWriter("using_text.txt"))
        {
            writer.WriteLine("This file was created with using statement");
            Console.WriteLine("  File written successfully (using statement)");
            // No need for manual cleanup with Dispose()
        }
        Console.WriteLine("  Resource automatically disposed by using statement");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  Error writing file: {ex.Message}");
    }
}

Console.WriteLine("\n==================================================\n");

// Using declaration (C# 8+)
Console.WriteLine("---Using Declarations (C# 8++)---");
Console.WriteLine("Demonstrating using declaration:");
DemonstrateUsingDeclaration();

Console.WriteLine("\n==================================================\n");

static void DemonstrateUsingDeclaration()
{
    string tempFile = "using_declaration_demo.txt";

    try
    {
        if (File.Exists(tempFile))
        {
            // Using declaration - resource disposed when leaving the 'if' block
            using var reader = File.OpenText(tempFile);
            Console.WriteLine("  ✔ File opened with using declaration");
            string? firstLine = reader.ReadLine();
            Console.WriteLine($"  ✔ Read line: {firstLine ?? "empty"}");
        }
        else
        {
            using var writer = new StreamWriter(tempFile);
            writer.WriteLine("Demo content for using declaration");
            Console.WriteLine("  ✔ File created with using declaration");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  ❌ Error: {ex.Message}");
    }
    finally
    {
        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
            Console.WriteLine("  ✔ Demo file cleaned up");
        }
    }
}

// Throw Expression
Console.WriteLine("---Throw Expression (C# 7++)---");
Console.WriteLine("Testing throw expressions in different contexts:");

// Test expression-bodied method that throws
try
{
    string result = GetNotImplementedFeature();
    Console.WriteLine(result);
}
catch (NotImplementedException ex)
{
    Console.WriteLine($"  ✔ Caught from expression-bodied method: {ex.Message}");
}

// Test throw in ternary conditional
try
{
    string result = ProperCase(null);
    Console.WriteLine(result);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  ✔ Caught from ternary expression: {ex.Message}");
}

// Test with valid input
try
{
    string result = ProperCase("hello world");
    Console.WriteLine($"  ✔ ProperCase result: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"  ❌ Unexpected error: {ex.Message}");
}

Console.WriteLine("\n==================================================\n");

static string GetNotImplementedFeature() =>
    throw new NotImplementedException("This feature is planned for version 2.0");

static string ProperCase(string? value) =>
    value == null ? throw new ArgumentException("Value cannot be null") :
    value == "" ? "" :
    char.ToUpper(value[0]) + value.Substring(1).ToLower();

// Common Exception
Console.WriteLine("---Commond Exception Types---");

DemonstrateArgumentException();
DemonstrateOperationExceptions();
DemonstrateSystemExceptions();

Console.WriteLine("\n==================================================\n");

static void DemonstrateArgumentException()
{
    Console.WriteLine("\nArgumentException Family:");
    Console.WriteLine("-------------------------");

    // ArgumentNullException
    try
    {
        ValidateUserInput(null, 25);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine($"  ✔ ArgumentNullException: {ex.ParamName} - {ex.Message}");
    }

    // ArgumentException (general)
    try
    {
        ValidateUserInput("", 25);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"  ✔ ArgumentException: {ex.ParamName} - {ex.Message}");
    }

    // ArgumentOutOfRangeException
    try
    {
        ValidateUserInput("John", -5);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"  ✔ ArgumentOutOfRangeException: {ex.ParamName} - {ex.Message}");
    }

    static void ValidateUserInput(string? name, int age)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name), "Name parameter cannot be null");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or whitespace", nameof(name));

        if (age < 0 || age > 150)
            throw new ArgumentOutOfRangeException(nameof(age), age, "Age must be between 0 and 150");

        Console.WriteLine($"  ✔ Valid input: {name}, age {age}");
    }
}

static void DemonstrateOperationExceptions()
{
    Console.WriteLine("\nOperation State Exceptions:");
    Console.WriteLine("---------------------------");

    var demoObject = new DisposableDemo();

    // InvalidOperationException
    try
    {
        demoObject.PerformOpertion(false);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"  ✔ InvalidOperationException: {ex.Message}");
    }

    // ObjectDisposedException
    demoObject.Dispose();
    try
    {
        demoObject.PerformOpertion(true);
    }
    catch (ObjectDisposedException ex)
    {
        Console.WriteLine($"  ✔ ObjectDisposedException: {ex.Message}");
    }

    // NotSupportedException
    try
    {
        var list = new List<string> { "item1", "item2" };
        var readOnlyCollection = list.AsReadOnly();

        // Attempting to modify a read-only collection
        ((ICollection<string>)readOnlyCollection).Add("item3");
    }
    catch (NotSupportedException ex)
    {
        Console.WriteLine($"  ✔ NotSupportedException: {ex.Message}");
    }
}

static void DemonstrateSystemExceptions()
{
    Console.WriteLine("\nSystem Exceptions:");
    Console.WriteLine("-------------------");

    // NullReferenceException (usually indicates a programming bug)
    try
    {
        string? nullString = null;
        int length = nullString!.Length; // null-forgiving operator for demo purposes
    }
    catch (NullReferenceException ex)
    {
        Console.WriteLine($"  ✔ NullReferenceException: {ex.Message}");
        Console.WriteLine("    Note: This usually indicates a programming bug - always validate for null");
    }

    // FormatException
    try
    {
        int number = int.Parse("not_a_number");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"  ✔ FormatException: {ex.Message}");
    }
}

// Throwing Exception
Console.WriteLine("---Throwing Exceptions---");
Console.WriteLine("Testing custom exception throwing:");

// Test with valid input
try
{
    DisplayName("John Doe");
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"  ❌ Caught: {ex.Message}");
}

// Test with null input
try
{
    DisplayName(null!); // null-forgiving operator for demo
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"  ✔ Caught ArgumentNullException: {ex.ParamName} - {ex.Message}");
}

// Test with empty input
try
{
    DisplayName("");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  ✔ Caught ArgumentException: {ex.ParamName} - {ex.Message}");
}

Console.WriteLine("\n==================================================\n");

static void DisplayName(string? name)
{
    // ArgumentNullException for null values
    if (name == null)
        throw new ArgumentNullException(nameof(name), "Name cannot be null");

    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Name cannot be empty or whitespace", nameof(name));

    Console.WriteLine($"  ✔ Hello, {name}!");
}

// Rethrowing Exception
Console.WriteLine("---Rethrowing Exception---");
Console.WriteLine("Testing exception rethrowing and wrapping:");

try
{
    ProcessDataWithLogging();
}
catch (InvalidDataException ex)
{
    Console.WriteLine($"  ✔ Final catch - InvalidDataException: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"  ✔ Inner exception preserved: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
    }
}

Console.WriteLine("\nDemonstrating simple rethrow (preserving stack trace):");
try
{
    MethodThatRetrows();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  ✔ Caught rethrown exception: {ex.Message}");
    Console.WriteLine($"    Exception was logged and rethrown from intermediate method");
}

Console.WriteLine("\n==================================================\n");

static void ProcessDataWithLogging()
{
    try
    {
        ParseCriticalData("invalid_data");
    }
    catch (FormatException ex)
    {
        // Log the error for debugging
        Console.WriteLine("  ➡ Logging original error for debugging purposes...");
        Console.WriteLine($"    Original error: {ex.GetType().Name} - {ex.Message}");

        // Wrap the original exception in a domain-specific exception
        // Original exception becomes the InnerException
        throw new InvalidDataException("Failed to process critical business data", ex);
    }
}

static void ParseCriticalData(string data)
{
    // Simulate parsing that can fail
    if (data == "invalid_data")
    {
        throw new FormatException("Data format is not recognized by the parser");
    }

    Console.WriteLine($"  ✔ Successfully parsed: {data}");
}

static void MethodThatRetrows()
{
    try
    {
        // Simulate an operation that fails
        throw new InvalidOperationException("Original operation failed");
    }
    catch (InvalidOperationException)
    {
        Console.WriteLine("  ➡ Logging in intermediate method...");
        // Use 'throw;' to rethrow the same exception with preserved stack trace
        // Never use 'throw ex;' as it resets the stack trace
        throw;
        throw;
    }
}

// TryXXX Pattern
Console.WriteLine("---TryXXX Pattern---");
Console.WriteLine("Comparing exception-based vs TryParse approaches:");

string[] testInputs = { "123", "abc", "999999999999", "45.67" };

foreach (string input in testInputs)
{
    Console.WriteLine($"\nTesting input: '{input}'");

    // Exception-based approach - expensive when failures are common
    Console.WriteLine("  Exception-based approach:");
    try
    {
        int result = int.Parse(input);
        Console.WriteLine($"    ✔ Success: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"    ❌ Failed: {ex.GetType().Name}");
    }

    // TryParse approach - efficient, no exceptions thrown
    Console.WriteLine("  TryParse approach:");
    if (int.TryParse(input, out int tryResult))
    {
        Console.WriteLine($"    ✔ Success: {tryResult}");
    }
    else
    {
        Console.WriteLine("    ❌ Failed: Invalid format or overflow");
    }
}

// Demonstrate custom TryXXX method
Console.WriteLine("\nCustom TryDivide method demonstration:");
TestCustomTryMethod();

Console.WriteLine("\n==================================================\n");

static void TestCustomTryMethod()
{
    int[][] testCases = {
        new int[] {10, 2},
        new int[] {15, 3},
        new int[] {7, 0}, // Division by zero case
        new int[] {-20, 4}
    };

    foreach (var testCase in testCases)
    {
        int numerator = testCase[0];
        int denominator = testCase[1];

        Console.WriteLine($"  Testing {numerator} / {denominator}:");

        // Using cutom TryDivide method
        if (TryDivide(numerator, denominator, out int result))
        {
            Console.WriteLine($"    ✔ Success: {result}");
        }
        else
        {
            Console.WriteLine("    ❌ Failed: Division by zero not allowed");
        }
    }
}

// Cutom TryXXX method implementation
static bool TryDivide(int numerator, int denominator, out int result)
{
    if (denominator == 0)
    {
        result = 0; // Set a default value
        return false; // Indicates failure
    }

    result = numerator / denominator;
    return true; // Indicates success
}

// Real World Scenario
Console.WriteLine("---Real World Screnario - File Processing System---");

var processor = new FileProcessor();

Console.WriteLine("\nProcessing various file scenarios:\n");

processor.ProcessFile("valid_data.txt");
processor.ProcessFile(""); // Empty path
processor.ProcessFile("nonexistent.txt"); // File not found
processor.ProcessFile("corrupted.txt"); // Simulate corruption

// Demonstrate the TryProcess alternative approach
Console.WriteLine("\n Demonstrating TryProcess alternative:");
if (processor.TryProcessFile("test.txt", out string? errorMessage))
{
    Console.WriteLine("  ✔ File processed successfully using TryProcess");
}
else
{
    Console.WriteLine($"  ❌ TryProcess failed: {errorMessage}");
}

Console.WriteLine("\n==================================================\n");


// Helper class for demonstrating object state exceptions
class DisposableDemo : IDisposable
{
    private bool _isReady = false;
    private bool _disposed = false;

    public void PerformOpertion(bool setReady)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(DisposableDemo));

        if (setReady)
            _isReady = true;

        if (!_isReady)
            throw new InvalidOperationException("Object is not in a ready state for this operation");

        Console.WriteLine("  ✔ Operation completed successfully");
    }
    public void Dispose()
    {
        _disposed = true;
        Console.WriteLine("  ✔ Object disposed");
    }
}

// Custom exception for business logic
public class InvalidDataException : Exception
{
    public InvalidDataException(string message) : base(message) { }
    public InvalidDataException(string message, Exception innerException) : base(message, innerException) { }
}

// Real World File Processor
public class FileProcessor
{
    public void ProcessFile(string filePath)
    {
        Console.WriteLine($"Processing file: '{filePath}'");
        FileStream? fileStream = null;
        StreamReader? reader = null;

        try
        {
            // Validate input parameter
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty", nameof(filePath));

            // Simulate different file conditions for demonstration
            if (filePath == "nonexistent.txt")
                throw new FileNotFoundException($"Could not find file: {filePath}");

            if (filePath == "corrupted.txt")
                throw new InvalidDataException("File appears to be corrupted or unreadable");

            // Simulate successful file processing
            Console.WriteLine("  ✔ File validation passed");
            Console.WriteLine("  ✔ File opened successfully");
            Console.WriteLine("  ✔ Data processed and validated");
            Console.WriteLine("  ✔ Processing completed successfully");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ❌ Input validation Error: {ex.Message}");
            LogError("ARGUMENT_ERROR", ex);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"  ❌ File System Error: {ex.Message}");
            LogError("FILE_NOT_FOUND", ex);
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine($"  ❌ Data Processing Error: {ex.Message}");
            LogError("DATA_CORRUPTION", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"  ❌ Access Denied: {ex.Message}");
            LogError("ACCESS_DENIED", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ❌ Unexpected Error: {ex.Message}");
            LogError("UNEXPECTED_ERROR", ex);
            // In production, may rethrow unexpected exceptions
            // throw;
        }
        finally
        {
            // Resource cleanup
            Console.WriteLine("  ➡ Performing cleanup operations...");

            // Dispose resource in reverse order of acquisition
            reader?.Dispose();
            fileStream?.Dispose();

            Console.WriteLine("  ➡ Resource cleanup completed");
        }

        Console.WriteLine();
    }

    private void LogError(string errorType, Exception ex)
    {
        // Write loggin framework
        // Like SeriLog, NLog, or Microsoft.Extension.Logging
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine($"  ➡ [LOG] {timestamp} - {errorType}: {ex.Message}");

        // Could also log stack trace for debugging
        Console.WriteLine($"  → [LOG] Stack Trace: {ex.StackTrace}");
    }

    // Alternative TryProcess method - no exception thrown
    // Return success/failure and provides error details vio out parameter
    public bool TryProcessFile(string filePath, out string? errorMessage)
    {
        errorMessage = null;

        try
        {
            ProcessFile(filePath);
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }
}
