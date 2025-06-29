using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class ParsingUtils
{
    private const string QuotePlaceholder = "!!!QUOTE!!!";

    public static IEnumerable<string> ParseCsvLine(string line)
    {
        var isInsideQuotes = false;
        var startIndex = 0;
        line = line
            .Replace("\"\"", QuotePlaceholder)
            .Replace("\\n", "\n");
        for (int i = 0; i < line.Length ; i++)
        {
            switch (line[i])
            {
                case '"':
                    if (isInsideQuotes)
                    {
                        isInsideQuotes = false;
                    }
                    else
                    {
                        isInsideQuotes = true;
                        continue;
                    }
                    break;
                case ',':
                    if (!isInsideQuotes)
                    {
                        yield return ProcessParsedValue(line[startIndex..i]);
                        startIndex = i + 1;
                    }
                    break;
                default:
                    break;
            }
        }
        yield return ProcessParsedValue(line[startIndex..]);
    }

    private static string ProcessParsedValue(string parsedValue)
    {
        return parsedValue.Replace(QuotePlaceholder, "\"").Trim('\"');
    }

    /// <summary>
    /// Parses all Localization keys in format !(key)
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public static IEnumerable<string> ParseLocalizationKeys(string line)
    {
        if (line == null)
        {
            return Enumerable.Empty<string>();
        }
        return new Regex(@"!\([^\)]+\)").Matches(line).Select(x => x.Value);
    }
}
