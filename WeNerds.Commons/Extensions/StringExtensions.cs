using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WeNerds.Commons.Extensions;

public static partial class StringExtensions
{
    private static readonly string OnlyNumbersRegex = "[^0-9]";

    [GeneratedRegex("[A-Z]")]
    private static partial Regex SnakeCaseRegex();
    [GeneratedRegex("_[a-z]")]
    private static partial Regex CamelCaseRegex();

    /// <summary>
    /// Convert a string value in SnakeCase format
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns>String. Converted value.</returns>
    public static string ToSnakeCase(this string value)
        => SnakeCaseRegex().Replace(value, "_$0").ToLower()[1..];            
    /// <summary>
    /// Convert a string value in CamelCase format
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns>String. Converted value.</returns>
    public static string ToCamelCase(this string value)
    {
        return CamelCaseRegex().Replace(value, delegate (Match m) {
            return m.ToString().TrimStart('_').ToUpper();
        });
    }
    public static bool Filled(this string value)
        => string.IsNullOrWhiteSpace(value) == false;    
    public static Guid ToGuid(this string value)
    {
        _ = Guid.TryParse(value, out var result);
        return result;
    }
    public static Guid? ToGuidNullable(this string value)
    {
        if (Guid.TryParse(value, out var newGuid))
            return newGuid;
        return null;
    }
    public static DateTime ToDateTime(this string value, DateTimeKind? kind)
    {
        kind ??= DateTimeKind.Utc;        
        if(DateTime.TryParse(value, out DateTime result) == false)
            result = DateTime.UtcNow;

        return result.SetKind((DateTimeKind)kind);
    }
    [Obsolete("Use ToDateTimeOffset")]
    public static DateTimeOffset ToDateTime(this string value, bool isUtcTime = true)
    {        
        try
        {
            if (DateTime.TryParse(value, out DateTime dateConverted))            
            {
                if (isUtcTime)
                    dateConverted.SetKind(DateTimeKind.Utc);
                else
                    dateConverted.SetKind(DateTimeKind.Local);
            }

            return dateConverted;
        }
        catch
        {
            if (isUtcTime)
                return DateTimeOffset.UtcNow;

            return DateTimeOffset.Now;
        }        
    }
    public static DateTimeOffset ToDateTimeOffset(this string value, bool isUtcTime = true)
    {
        try
        {
            if (DateTime.TryParse(value, out DateTime dateConverted))
            {
                if (isUtcTime)
                    dateConverted.SetKind(DateTimeKind.Utc);
                else
                    dateConverted.SetKind(DateTimeKind.Local);
            }

            return dateConverted;
        }
        catch
        {
            if (isUtcTime)
                return DateTimeOffset.UtcNow;

            return DateTimeOffset.Now;
        }
    }
    public static decimal ToDecimal(this string value)
    {
        _ = decimal.TryParse(value, out decimal result);
        return result;
    }
    public static double ToDouble(this string value, bool swapCommaForDot = false)
    {
        if (swapCommaForDot)        
            value = value.Replace(".", "").Replace(",", ".");
        
        _ = double.TryParse(value, out double result);
        return result;
    }
    public static int ToInt(this string value)
    {
        _ = int.TryParse(value, out int result);
        return result;
    }
    public static uint ToUInt(this string value)
    {
        _ = uint.TryParse(value, out uint result);
        return result;
    }
    public static bool IsGuid(this string value)
        => Guid.TryParse(value, out _);   
    public static string OnlyNumber(this string value)    
        => (!string.IsNullOrEmpty(value)) ? Regex.Replace(value, OnlyNumbersRegex, "") : value;
    public static string GetValueOrDefault(this string value, string defaultValue = "")
    {
        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;

        return value;        
    }
    public static TResponse Deserialize<TResponse>(this string value)
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase

        };
        return JsonSerializer.Deserialize<TResponse>(value, options);
    }
    public static bool IsEqual(this string value, string valueComparer, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        => string.Equals(value, valueComparer, stringComparison);
}
