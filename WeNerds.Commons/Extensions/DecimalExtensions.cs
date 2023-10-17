using WeNerds.Commons.Enumarators;


namespace WeNerds.Commons.Extensions;

public static class DecimalExtensions
{
    public static decimal ToRound(this decimal value, RoundType roundType = RoundType.Round, int precision = 2, decimal onErrorReturn = -1m)
    {
        decimal result;
        try
        {
            switch (roundType)
            {
                case RoundType.Floor:
                    result = Math.Floor(value);
                    break;

                case RoundType.Ceiling:
                    result = Math.Ceiling(value);
                    break;


                case RoundType.Truncate:
                    var convertedPrecision = (int)Math.Pow(10, precision);
                    result = Math.Floor(value  * convertedPrecision) / convertedPrecision;
                    break;

                default:
                    result = Math.Round(value, precision);
                    break;
            }
        }
        catch
        {
            result = onErrorReturn;
        }

        return result;
    }
    public static decimal ToRound(this decimal? value, RoundType roundType = RoundType.Round, int precision = 2, decimal onErrorReturn = -1m)
    {
        decimal result;
        try
        {
            switch (roundType)
            {
                case RoundType.Floor:
                    result = Math.Floor(value ?? 0);
                    break;

                case RoundType.Ceiling:
                    result = Math.Ceiling(value ?? 0);
                    break;


                case RoundType.Truncate:
                    var convertedPrecision = (int)Math.Pow(10, precision);
                    result = Math.Floor((value ?? 0) * convertedPrecision) / convertedPrecision;
                    //result = Math.Floor((value ?? 0) * 100) / 100;
                    break;

                default:
                    result = Math.Round(value ?? 0, precision);
                    break;
            }
        }
        catch
        {
            result = onErrorReturn;
        }

        return result;
    }
    public static decimal ToDecimal(this decimal? value, decimal onErrorOrNullReturn = 0m)
    {
        if (value == null)         
            return onErrorOrNullReturn;

        if (decimal.TryParse(value.ToString(), out var newValue))
            return newValue;

        return onErrorOrNullReturn;

    }
    public static string ToFormattedString(this decimal? value, ushort precision = 2, uint maxLength = 0, string defaultValue = "-", RoundType roundType = RoundType.Truncate)
    {
        if (value == null)
            return defaultValue;

        var newValue = value.ToRound(roundType, precision);

        var precisionFormat = "";
        if (precision > 0)
            precisionFormat = $".{"".PadRight(precision, '0')}";


        var newValueString = ((decimal)newValue).ToString($"0{precisionFormat}");

        if (newValueString.Length > maxLength)
            newValueString = $"_{newValueString.Substring(1, newValueString.Length - 1)}";

        return newValueString;
    }
    public static string ToFormattedString(this decimal value, ushort precision = 2, uint maxLength = 0, RoundType roundType = RoundType.Truncate)
    {
        var newValue = value.ToRound(roundType, precision);

        var precisionFormat = "";
        if (precision > 0)
            precisionFormat = $".{"".PadRight(precision, '0')}";


        var newValueString = ((decimal)newValue).ToString($"0{precisionFormat}");

        if (newValueString.Length > maxLength)
            newValueString = $"_{newValueString.Substring(1, newValueString.Length - 1)}";

        return newValueString;
    }
    public static string ToFixedFormattedString(this decimal? value, uint maxLength, ushort precision = 2, AlignmentEnum alignment = AlignmentEnum.Left, string defaultValue = "-", RoundType roundType = RoundType.Truncate)
    {

        var length = (int)(maxLength == 0 ? 1 : maxLength - 1);
        var newValue = value.ToFormattedString(precision, maxLength, defaultValue, roundType);
        var stringLength = newValue.Length;
        if (stringLength == length)
            return newValue;

        switch (alignment)
        {
            case AlignmentEnum.Left:
                return $"{(newValue.PadRight(length, ' ')).Substring(0, length)}";
            case AlignmentEnum.Center:
                var spaces = (int)Math.Ceiling((decimal)((maxLength - stringLength) / 2));
                return $"{"".PadLeft(spaces, ' ')}{newValue}{"".PadRight(spaces, ' ')}"[..length];
            case AlignmentEnum.Right:
                return $"{(newValue.PadLeft(length, ' ')).Substring(0, length)}";
            default:
                return newValue;
        }
    }
    public static string ToFixedFormattedString(this decimal value, uint maxLength, ushort precision = 2, AlignmentEnum alignment = AlignmentEnum.Left, RoundType roundType = RoundType.Truncate)
    {
        var length = (int)(maxLength == 0 ? 1 : maxLength - 1);
        var newValue = value.ToFormattedString(precision, maxLength, roundType);
        var stringLength = newValue.Length;
        if (stringLength == length)
            return newValue;

        switch (alignment)
        {
            case AlignmentEnum.Left:
                return $"{(newValue.PadRight(length, ' ')).Substring(0, length)}";
            case AlignmentEnum.Center:
                var spaces = (int)Math.Ceiling((decimal)((maxLength - stringLength) / 2));
                return $"{"".PadLeft(spaces, ' ')}{newValue}{"".PadRight(spaces, ' ')}"[..length];
            case AlignmentEnum.Right:
                return $"{(newValue.PadLeft(length, ' ')).Substring(0, length)}";
            default:
                return newValue;
        }
    }

}
