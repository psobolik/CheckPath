var path = GetPath(); 
WriteValues("PATH:", path);
// Normalize the folders in the path
var normalPath = path.Select(folder => Path.TrimEndingDirectorySeparator(PathWithExpandedTilde(folder)));
WriteValues("\nInvalid folders in PATH:",
    normalPath.Distinct().Where(folder => !Directory.Exists(folder)));
WriteValues("\nDuplicate folders in PATH:",
    normalPath
        .Where(item =>
            normalPath.Count(i => i == item) > 1)
        .Distinct()
        .ToArray());
return;

static string PathWithExpandedTilde(string str)
{
    return str.StartsWith('~')
        ? string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), str.AsSpan(1))
        : str;
}

static IEnumerable<string> GetPath()
{
    var path = Environment.GetEnvironmentVariable("PATH");
    return string.IsNullOrWhiteSpace(path)
        ? Array.Empty<string>()
        : path.Split(Path.PathSeparator).Where(path => path != string.Empty);
}

static void WriteValues(string header, IEnumerable<string> values)
{
    const ConsoleColor headerColor = ConsoleColor.Green;
    const ConsoleColor infoColor = ConsoleColor.Yellow;
    const ConsoleColor listColor = ConsoleColor.White;

    WriteValue(header, headerColor);
    if (values.Any())
    {
        foreach (var value in values)
        {
            WriteValue(value, listColor);
        }
    }
    else
    {
        WriteValue("[None]", infoColor);
    }

    return;

    static void WriteValue(string value, ConsoleColor foregroundColor)
    {
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine(value);
        Console.ResetColor();
    }
}