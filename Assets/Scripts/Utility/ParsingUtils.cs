using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class ParsingUtils
{
    public static IEnumerable<string> ParseCsvLine(string line)
    {
        const string QuotePlaceholder = "!!!QUOTE!!!";

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
                        yield return line[startIndex..i].Replace(QuotePlaceholder, "\"").Trim('\"');
                        startIndex = i + 1;
                    }
                    break;
                default:
                    break;
            }
        }
        yield return line[startIndex..];
    }

    public static IEnumerable<string> ParseLocalizationKeys(string line)
    {
        if (line == null)
        {
            return Enumerable.Empty<string>();
        }
        return new Regex(@"!\([^\)]+\)").Matches(line).Select(x => x.Value);
    }
}
