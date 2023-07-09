using System;
using System.Collections.Generic;
using UnityEngine;

public static class NumbersFormatter
{

    private const string _formatString = "0.##";
    private const double _maxFractional = 0.99;

    private static readonly string[] _prefixes = { "K", "M", "B", "T", "aa", "ab", "ac", "ad"
            , "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq"
            , "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd"
            , "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq"
            , "br" };

    public static string Format(double number)
    {
        if (number < 1000.0)
        {
            return number.ToString("0");
        }

        ScientificNotation scientificNotation = ScientificNotation.FromDouble(number);
        ushort adjustedExponent = (ushort)((scientificNotation.Exponent / 3) - 1);

        if (adjustedExponent < _prefixes.Length)
        {
            string prefix = _prefixes[adjustedExponent];
            double adjustedSignificand = scientificNotation.Significand * Math.Pow(10, scientificNotation.Exponent % 3);
            double integralPart = Math.Truncate(adjustedSignificand);
            double formattedValue = ((adjustedSignificand - integralPart) > _maxFractional) ? integralPart + _maxFractional : adjustedSignificand;
            return $"{formattedValue.ToString(_formatString)} {prefix}";
        }

        return $"{scientificNotation.Significand.ToString(_formatString)}";
    }

    public static double Format(string number)
    {
        if (double.TryParse(number, out double parsedNumber))
        {
            return parsedNumber;
        }

        int spaceIndex = number.IndexOf(' ');
        if (spaceIndex != -1 && double.TryParse(number.Substring(0, spaceIndex), out double value))
        {
            string prefix = number.Substring(spaceIndex + 1);
            int prefixIndex = Array.BinarySearch(_prefixes, prefix);
            if (prefixIndex >= 0)
            {
                double multiplier = Math.Pow(10, (prefixIndex + 1) * 3);
                return value * multiplier;
            }
        }

        return 0.0;
    }




}

public struct ScientificNotation
{
    public ushort Exponent { get; set; }
    public double Significand { get; set; }

    public static ScientificNotation FromDouble(double number)
    {
        ushort exponent = (ushort)Math.Log10(number);
        return new ScientificNotation
        {
            Exponent = exponent,
            Significand = number * Math.Pow(0.1, exponent)
        };
    }
}
