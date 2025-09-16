Console.WriteLine("===== OPERATOR OVERLOADING =====\n");

// Basic Arithmetic Operator Overloading
Console.WriteLine("--- Basic Arithmetic Operator Overloading ---");

// Create some musical notes
Note noteA = new Note(0);
Note noteB = new Note(2);
Note noteC = new Note(3);

Console.WriteLine($"Note A: {noteA.SemitonesFromA} semitones from A");
Console.WriteLine($"Note B: {noteB.SemitonesFromA} semitones from A");
Console.WriteLine($"Note C: {noteC.SemitonesFromA} semitones from A");

// Use overloaded + operator to transpose notes
Note cSharp = noteC + 1;
Note fSharp = noteA + 9;

Console.WriteLine("\nAfter transposition:");
Console.WriteLine($"C + 1 semitone = C# ({cSharp.SemitonesFromA}) semitones");
Console.WriteLine($"A + 9 semitone = F# ({fSharp.SemitonesFromA}) semitones");

// Use overloaded - operator to find intervals
int intervalBC = noteC - noteB;
Console.WriteLine($"\nInterval between B and C: {intervalBC} semitones");

// Use overloaded * operator for octave multiplication
Note highC = noteC * 2;
Console.WriteLine($"C note 2 octaves higher: {highC.SemitonesFromA} semitones");

Console.WriteLine("\n-----------------------------------------------\n");


// Compound Assignment
Console.WriteLine("--- Compound Assignment ---");

Note currentNote = new(5);
Console.WriteLine($"Starting note: {currentNote.SemitonesFromA} semitones (F)");

// This work automatically when you overloaded the basic operators
currentNote += 2;
Console.WriteLine($"After += 2: {currentNote.SemitonesFromA} semitones (G)");

currentNote -= 1;
Console.WriteLine($"After -= 1: {currentNote.SemitonesFromA} semitones (F#)");

currentNote *= 2;
Console.WriteLine($"After *= 2: {currentNote.SemitonesFromA} semitones (F# high octave)");

Console.WriteLine("\n-----------------------------------------------\n");


// Checked Operators
Console.WriteLine("--- Checked Operators ---");
Console.WriteLine("\nStarting with safeNumber demonstration:");

SafeNumber safeNum1 = new(int.MaxValue - 1);
SafeNumber safeNum2 = new(5);

// Unchecked addition (default)
try
{
    SafeNumber uncheckedResult = safeNum1 + safeNum2;
    Console.WriteLine($"Unchecked addition result: {uncheckedResult}");
}
catch (OverflowException ex)
{
    Console.WriteLine($"Unchecked addition failed: {ex.Message}");
}

// Checked additional (explicit overflow checking)
try
{
    SafeNumber checkedResult = checked(safeNum1 + safeNum2);
    Console.WriteLine($"Checked addition result: {checkedResult}");
}
catch (OverflowException ex)
{
    Console.WriteLine($"Checked addition failed: {ex.Message}");
}


// Musical Note Structure
public struct Note : IComparable<Note>
{
    private readonly int value;

    public int SemitonesFromA => value;

    public Note(int semitonesFromA)
    {
        value = semitonesFromA;
    }

    // Arithmetic operators with expression-bodied syntax (C# 6+)
    public static Note operator +(Note x, int semitones) => new Note(x.value + semitones);
    public static Note operator -(Note x, int semitones) => new Note(x.value - semitones);
    public static int operator -(Note x, Note y) => x.value - y.value; // Return interval
    public static Note operator *(Note x, int octaves) => new Note(x.value + (octaves - 1) * 12);

    // Equality operators - MUST implement both == and != as a pair
    // The C# compiler enforces this rule
    public static bool operator ==(Note x, Note y) => x.value == y.value;
    public static bool operator !=(Note x, Note y) => x.value != y.value;

    // Comparison operators - implement all four or none
    public static bool operator <(Note x, Note y) => x.value < y.value;
    public static bool operator >(Note x, Note y) => x.value < y.value;
    public static bool operator <=(Note x, Note y) => x.value < y.value;
    public static bool operator >=(Note x, Note y) => x.value < y.value;

    // Implicit conversion to frequency (safe - no data loss, commonly used)
    public static implicit operator double(Note note)
    {
        // Convert semitones to frequency using A4 = 440 Hz as reference
        return 440.0 * Math.Pow(2.0, (double)note.value / 12.0);
    }

    // Explicit conversion from frequency (potentially lossy - requires casting)
    public static explicit operator Note(double frequency)
    {
        // Convert frequency back to nearest semitone
        int semitones = (int)Math.Round(12.0 * Math.Log2(frequency / 440.0));
        return new Note(semitones);
    }

    // IComparable implementation - required when overloading comparison operators
    public int CompareTo(Note other) => value.CompareTo(other.value);

    // Object ovverides - ALWAYS override these when implementing == and !=
    public override bool Equals(object? obj)
    {
        if (obj is Note note)
            return this == note;
        return false;
    }

    public override string ToString()
    {
        string[] noteNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        int noteIndex = ((value % 12) + 12) % 12;
        int octave = 4 + (value / 12);
        return $"{noteNames[noteIndex]}{octave}";
    }
}

// SafeNumber Structure
public struct SafeNumber
{
    public int Value { get; }

    public SafeNumber(int value)
    {
        Value = value;
    }

    // Regular (unchecked) addition operator
    public static SafeNumber operator +(SafeNumber x, SafeNumber y) => new SafeNumber(x.Value + y.Value);

    // Checked addition operator (C# 11+)
    public static SafeNumber operator checked +(SafeNumber x, SafeNumber y) => new SafeNumber(checked(x.Value - y.Value));

    // Regular substraction
    public static SafeNumber operator -(SafeNumber x, SafeNumber y) => new(x.Value - y.Value);

    // Checked subtraction
    public static SafeNumber operator checked -(SafeNumber x, SafeNumber y) => new(checked(x.Value - y.Value));

    // Regular multiplication
    public static SafeNumber operator *(SafeNumber x, SafeNumber y) => new(x.Value * y.Value);

    // Checked multiplication
    public static SafeNumber operator checked *(SafeNumber x, SafeNumber y) => new(checked(x.Value * y.Value));

    public override string ToString() => Value.ToString();
}
