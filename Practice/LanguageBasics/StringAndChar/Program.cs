char copyRightSymbol = '\u00A9';
string newLine = "This will be a new line\n";
char omegaSymbol = '\u03A9';
string a = "Here is a tab:\t";
string a2 = @"\\server\fileshare\helloworld.cs";

Console.WriteLine(copyRightSymbol);
Console.WriteLine(newLine);
Console.WriteLine(omegaSymbol);
Console.WriteLine(a);
Console.WriteLine(a2);


string escaped = "First Line\r\nSecond Line";
string verbatism = @"First Line
Second Line";
string xml = @"<root attribute=""value"">";

Console.WriteLine(escaped);
Console.WriteLine(verbatism);
Console.WriteLine(xml);
