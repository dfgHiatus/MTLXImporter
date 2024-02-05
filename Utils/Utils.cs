using Elements.Core;
using System.Text.RegularExpressions;

namespace MTLXImporter;

internal static class Utils
{
    internal static float2 ExtractFloatsFromString(string input)
    {
        const string pattern = @"([-+]?\d*\.\d+|\d+)";

        MatchCollection matches = Regex.Matches(input, pattern);
        float2 output = new float2(0f, 0f);

        if (matches.Count >= 2)
        {
            float firstFloat = float.Parse(matches[0].Value);
            float secondFloat = float.Parse(matches[1].Value);
            output = new float2(firstFloat, secondFloat);
        }

        return output;
    }
}
