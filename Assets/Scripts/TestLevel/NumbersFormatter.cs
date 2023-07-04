using System;
using System.Collections.Generic;

public static class NumbersFormatter
{
    private static readonly Dictionary<long, Func<float, string>> Suffixes = new Dictionary<long, Func<float, string>>
    {
        { 99L, val => val.ToString("0.##") },
        { 999L, val => val.ToString("0") },
        { 999_999L, val => GetFormattedValue(val, 1_000f, "0.##k") },
        { 999_999_999L, val => GetFormattedValue(val, 1_000_000f, "0.##m") },
        { 999_999_999_999L, val => GetFormattedValue(val, 1_000_000_000f, "0.##B") }
    };

    public static string Format(float price)
    {
        foreach (var suffix in Suffixes)
        {
            if (price < suffix.Key)
                return suffix.Value(price);
        }

        return price.ToString("0.#");
    }

    private static string GetFormattedValue(float value, float divisor, string format)
    {
        return (value / divisor).ToString(format);
    }
}
