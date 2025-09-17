using System.Text;

namespace StringAndTextHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== STRING AND TEXT HANDLING DEMONSTRATION =====\n");

            DemonstrateCharacterType();
            DemonstrateStringBasics();
            DemonstrateStringSearching();
            DemonstrateStringManipulation();
            DemonstrateStringSplittingAndJoining();
            DemonstrateStringInterpolationAndFormating();
            DemonstrateStringBuilder();
            DemonstrateTextEncoding();

            Console.WriteLine("\n===== END OF DEMONSTRATION =====");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void DemonstrateCharacterType()
        {
            Console.WriteLine("--- Character Type Demonstration ---\n");

            // Basic Character
            Console.WriteLine($"Basic character: A");
            Console.WriteLine($"Unicode character \\u0041: {'\u0041'}");
            Console.WriteLine($"Special characters exist: newline={'\n'}, tab={'\t'}");

            // Character manipulation method
            Console.WriteLine($"Uppercase 'c': {char.ToUpper('c')}");
            Console.WriteLine($"Lowercase 'C': {char.ToLower('C')}");
            Console.WriteLine($"Is tab whitespace? {char.IsWhiteSpace('\t')}");

            // Culture invariant methods
            Console.WriteLine($"Culture-invariant uppercase 'i': {char.ToUpperInvariant('i')}");
            Console.WriteLine($"Regular uppercase 'i': {char.ToUpper('i')}");

            // Character categorization
            Console.WriteLine($"Is 'A' a letter? {char.IsLetter('A')}");
            Console.WriteLine($"Is '5' a digit? {char.IsDigit('5')}");
            Console.WriteLine($"Is '!' punctuation? {char.IsPunctuation('!')}");
            Console.WriteLine($"Is ' ' whitespace? {char.IsWhiteSpace(' ')}");

            // Unicode categorization for advanced text processing
            char testChar = 'A';
            Console.WriteLine($"Unicode category of '{testChar}': {char.GetUnicodeCategory(testChar)}");

            Console.WriteLine();
        }

        static void DemonstrateStringBasics()
        {
            Console.WriteLine("--- String Basics ---");

            string literal = "Hello, World!";
            string multiline = "First Line\r\nSecond Line";
            string verbatim = @"C:\Path\File.txt";
            string repeated = new string('*', 10);
            char[] charArray = { 'H', 'E', 'L', 'L', 'O' };
            string fromArray = new string(charArray);
            string fromSubset = new string(charArray, 1, 3);

            Console.WriteLine($"Literal string: {literal}");
            Console.WriteLine($"Multiline string: {multiline}");
            Console.WriteLine($"Verbatim string: {verbatim}");
            Console.WriteLine($"Repeated string: {repeated}");
            Console.WriteLine($"From char array: {fromArray}");
            Console.WriteLine($"From char subset: {fromSubset}");

            Console.WriteLine();
        }

        static void DemonstrateStringSearching()
        {
            Console.WriteLine("--- String Searching ---");

            string text = "The quick brown fox jumps over the lazy dog";

            // Basic search methods
            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"Starts with 'The': {text.StartsWith("The")}");
            Console.WriteLine($"Ends with 'dog': {text.EndsWith("dog")}");
            Console.WriteLine($"Contains 'brown': {text.Contains("brown")}");

            // Finding position
            Console.WriteLine($"Index of 'fox': {text.IndexOf("fox")}");
            Console.WriteLine($"Index of 'cat' (not found): {text.IndexOf("cat")}");
            Console.WriteLine($"Last index of 'the': {text.LastIndexOf("the")}");

            // Finding any of multiple character
            string sample = "apple,banana;orange:grape";
            char[] delimiters = { ',', ';', ':' };
            int firstDelimiter = sample.IndexOfAny(delimiters);
            Console.WriteLine($"Sample: '{sample}'");
            Console.WriteLine($"First delimiter position: {firstDelimiter}");

            Console.WriteLine();
        }

        static void DemonstrateStringManipulation()
        {
            Console.WriteLine("--- String Manipulation ---");

            string original = "Hello World!";

            // Substring extraction
            string left5 = original.Substring(0, 5);
            string right5 = original.Substring(6);
            Console.WriteLine($"Original: '{original}'");
            Console.WriteLine($"Left 5 characters: '{left5}'");
            Console.WriteLine($"From index 6 to the end: '{right5}'");

            // Insert and remove ops
            string inserted = original.Insert(5, ",");
            string removed = original.Remove(5, 0);
            Console.WriteLine($"After inserting comma: '{inserted}'");
            Console.WriteLine($"After removing comman: '{removed}'");

            // Padding
            string number = "123";
            Console.WriteLine($"Right-padded: '{number.PadRight(10, '*')}'");
            Console.WriteLine($"Left-padded: '{number.PadLeft(10, '0')}'");

            // Trimming whitespace
            string messy = "     Hello World      \t\r\n";
            Console.WriteLine($"Original length: {messy.Length}");
            Console.WriteLine($"Trimmed length: {messy.Trim().Length}");
            Console.WriteLine($"Trimmed result: '{messy.Trim()}'");

            // String replacement
            string sentence = "I like cats and cats like me";
            string replaced = sentence.Replace("cats", "dogs");
            Console.WriteLine($"Original: '{sentence}'");
            Console.WriteLine($"Replaced: '{replaced}'");

            Console.WriteLine();
        }

        static void DemonstrateStringSplittingAndJoining()
        {
            Console.WriteLine("--- String Splitting and Joining ---");

            // Splitting strings
            string sentence = "The quick brown fox jumps";
            string[] words = sentence.Split();

            Console.WriteLine($"Original sentence: '{sentence}'");
            Console.Write("Words: ");
            foreach (string word in words)
            {
                Console.Write($"'{word}' ");
            }
            Console.WriteLine();

            // Splitting with custom delimiters
            string csvData = "apple,banana,cherry,date";
            string[] fruits = csvData.Split(',');
            Console.WriteLine($"CSV data: '{csvData}'");
            Console.WriteLine($"Number of fruits: {fruits.Length}");

            // Joining strings back together
            string rejoined = string.Join(" ", words);
            string csvRejoined = string.Join(" | ", fruits);

            Console.WriteLine($"Rejoined with spaces: '{rejoined}'");
            Console.WriteLine($"Fruits joined with pipes: '{csvRejoined}'");

            Console.WriteLine();
        }

        static void DemonstrateStringInterpolationAndFormating()
        {
            Console.WriteLine("--- String Interpolation and Formatting ---");

            // String interpolation
            string name = "Naufal";
            int age = 24;
            DateTime today = DateTime.Now;

            string interpolated = $"Hello, my name is {name} and I'm {age} years old.";
            string withDate = $"Today is {today.DayOfWeek}, {today:dd MMMM yyyy}";

            Console.WriteLine(interpolated);
            Console.WriteLine(withDate);

            // Traditional string formatting
            string template = "It's {0} degress in {1} on this {2} morning";
            string formatted = string.Format(template, 25, "Jakarta", today.DayOfWeek);
            Console.WriteLine(formatted);

            // Format specifiers for numbers and dates
            double price = 19.99;
            Console.WriteLine($"Price: {price:C}"); // Currency format
            Console.WriteLine($"Percentage: {0.85:P}"); // Percentage format
            Console.WriteLine($"Date: {today:dddd, MMMM dd, yyyy}"); // Long date format

            Console.WriteLine();
        }

        static void DemonstrateStringBuilder()
        {
            Console.WriteLine("--- StringBuilder ---");

            Console.WriteLine("\n--- Basic StringBuilder Operations ---");

            StringBuilder sb = new();
            Console.WriteLine($"Initial capacity: {sb.Capacity}");
            Console.WriteLine($"Initial length: {sb.Length}");

            // Building strings efficiently
            for (int i = 0; i < 10; i++)
            {
                sb.Append($"Item {i}, ");
            }

            Console.WriteLine($"After appending 10 items:");
            Console.WriteLine($"Length: {sb.Length}, Capacity: {sb.Capacity}");
            Console.WriteLine($"Content: {sb.ToString()}");

            // StringBuilder initial capacity
            Console.WriteLine("\n--- Capacity Management ---");
            StringBuilder sbWithCapacity = new(100); // Pre-allocate capacity
            Console.WriteLine($"StringBuilder with initial capacity 100: {sbWithCapacity.Capacity}");

            // Various StringBuilder methods
            Console.WriteLine("\n--- StringBuilder Methods ---");
            sb.Clear();
            sb.AppendLine("First Line");
            sb.AppendLine("Second Line");
            sb.Insert(0, "Header: ");
            sb.Replace("First", "Primary");
            sb.AppendFormat("Formatted number: {0:N2}", 12345, 67);
            sb.AppendLine();

            Console.WriteLine("StringBuilder after various operations:");
            Console.WriteLine(sb.ToString());

            // Performance comparison
            Console.WriteLine("\n--- Performance Insights ---");
            StringBuilder chained = new StringBuilder()
                .Append("Method ")
                .Append("chaining ")
                .Append("works ")
                .AppendLine("great!");
            Console.WriteLine($"Method chaining result: {chained}");

            Console.WriteLine();
        }

        static void DemonstrateTextEncoding()
        {
            Console.WriteLine("--- Text Encoding ---");

            // Text encoding is crucial for file I/O, network communication, and data storage
            string originalText = "Hello, World! 🌍 Café résumé";
            Console.WriteLine($"Original text: {originalText}");
            Console.WriteLine($"Character count: {originalText.Length}");

            Console.WriteLine("\n--- Different Encoding Schemes ---");

            // UTF-8
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(originalText);
            Console.WriteLine($"UTF-8 byte count: {utf8Bytes.Length}");
            Console.WriteLine($"UTF-8 first 20 bytes: {BitConverter.ToString([.. utf8Bytes.Take(20)])}");

            // UTF-16
            byte[] utf16Bytes = Encoding.Unicode.GetBytes(originalText);
            Console.WriteLine($"UTF-16 byte count: {utf8Bytes.Length}");
            Console.WriteLine($"UTF-16 first 20 bytes: {BitConverter.ToString([.. utf16Bytes.Take(20)])}");

            // UTF-32
            byte[] utf32Bytes = Encoding.UTF32.GetBytes(originalText);
            Console.WriteLine($"UTF-32 byte count: {utf8Bytes.Length}");
            Console.WriteLine($"UTF-32 first 20 bytes: {BitConverter.ToString([.. utf32Bytes.Take(20)])}");


            // ASCII can't handle extended characters
            try
            {
                byte[] asciiWithEmoji = Encoding.ASCII.GetBytes("Hello 🌍");
                string asciiDecoded = Encoding.ASCII.GetString(asciiWithEmoji);
                Console.WriteLine($"ASCII with emoji: '{asciiDecoded}' (emoji lost!)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ASCII encoding issue: {ex.Message}");
            }


            // Encoding Roundtrip
            Console.WriteLine("\n--- Encoding/Decoding Roundtrip ---");

            string[] testEncodings = { "UTF-8", "UTF-16", "UTF-32" };
            Encoding[] encodings = { Encoding.UTF8, Encoding.Unicode, Encoding.UTF32 };

            for (int i = 0; i < testEncodings.Length; i++)
            {
                byte[] encoded = encodings[i].GetBytes(originalText);
                string decoded = encodings[i].GetString(encoded);
                bool isIdentical = originalText == decoded;
                Console.WriteLine($"{testEncodings[i]}: {encoded.Length} bytes, roundtrip successfull: {isIdentical}");
            }

            // Practical file operations
            Console.WriteLine("\n--- Practical File I/O Example ---");

            string tempFile = Path.GetTempFileName();

            try
            {
                // Write with UTF-8 (default)
                File.WriteAllText(tempFile, originalText);
                var utf8FileInfo = new FileInfo(tempFile);
                Console.WriteLine($"UTF-8 file size: {utf8FileInfo.Length} bytes");

                // Write with UTF-16
                File.WriteAllText(tempFile, originalText, Encoding.Unicode);
                var utf16FileInfo = new FileInfo(tempFile);
                Console.WriteLine($"UTF-16 file size: {utf16FileInfo.Length} bytes");

                // Read back and verify
                string readBack = File.ReadAllText(tempFile, Encoding.Unicode);
                Console.WriteLine($"Read back successfully: {originalText == readBack}");
            }
            finally
            {
                File.Delete(tempFile);
            }

            Console.WriteLine();
        }
    }
}