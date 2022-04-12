var path = GetPath().ToArray();
WriteValues("PATH:", path);
WriteValues("Missing folders in PATH:", path.Where(folder => !Directory.Exists(PathWithExpandedTilde(folder))).ToArray());
WriteValues("Duplicate folders in PATH:", path.Where((item) => path.Count(i => i == item) > 1).Distinct().ToArray());

static string PathWithExpandedTilde(string str)
{
    return str.StartsWith('~')
        ? string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), str.AsSpan(1))
        : str;
}

static IEnumerable<string> GetPath()
{
    var path = Environment.GetEnvironmentVariable("PATH");
    return string.IsNullOrWhiteSpace(path) ? Array.Empty<string>() : path.Split(Path.PathSeparator);
}

static void WriteValues(string header, string[] values)
{
    const ConsoleColor headerColor = ConsoleColor.Green;
    const ConsoleColor infoColor = ConsoleColor.Yellow;
    const ConsoleColor listColor = ConsoleColor.White;
        
    static void WriteValue(string value, ConsoleColor foregroundColor)
    {
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine(value);
        Console.ResetColor();
    }

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
}
