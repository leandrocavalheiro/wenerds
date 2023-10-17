using WeNerds.Commons.Enumarators;

namespace WeNerds.Commons.Extensions;

public static class DoubleExtensions
{
    public static double ToRound(this double value, RoundType roundType = RoundType.Round, int precision = 2, double onErrorReturn = -1)
    {
        double result;
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
                    result = Math.Floor(value * convertedPrecision) / convertedPrecision;
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
    public static short ToShort(this double value, RoundType roundType = RoundType.Round, short onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (short)value.ToRound(roundType, 0, defaultValue);
    }
    public static int ToInt(this double value, RoundType roundType = RoundType.Round, int onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (int)value.ToRound(roundType, 0, defaultValue);
    }
    public static long ToLong(this double value, RoundType roundType = RoundType.Round, long onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (long)value.ToRound(roundType, 0, defaultValue);
    }
    public static ushort ToUShort(this double value, RoundType roundType = RoundType.Round, ushort onErrorReturn = 0)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (ushort)value.ToRound(roundType, 0, defaultValue);
    }
    public static uint ToUInt(this double value, RoundType roundType = RoundType.Round, uint onErrorReturn = 0)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (uint)value.ToRound(roundType, 0, defaultValue);
    }
    public static ulong ToULong(this double value, RoundType roundType = RoundType.Round, uint onErrorReturn = 0)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (ulong)value.ToRound(roundType, 0, defaultValue);
    }
    public static decimal ToDecimal(this double value, RoundType roundType = RoundType.Round, int precision = 2, decimal onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (decimal)value.ToRound(roundType, precision, defaultValue);
    }
    public static float ToFloat(this double value, RoundType roundType = RoundType.Round, int precision = 2, float onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (float)value.ToRound(roundType, precision, defaultValue);
    }
    public static TResult ToGenericType<TResult>(this double value, RoundType roundType = RoundType.Round, int precision = 2, TResult onErrorReturn = default)
    {
        try
        {
            switch (typeof(TResult).ToString())
            {
                case "System.Int16":
                    return (TResult)(object)value.ToShort(roundType);
                case "System.Int32":
                    return (TResult)(object)value.ToInt(roundType);
                case "System.Int64":
                    return (TResult)(object)value.ToLong(roundType);
                case "System.UInt16":
                    return (TResult)(object)value.ToUShort(roundType);
                case "System.UInt32":
                    return (TResult)(object)value.ToUInt(roundType);
                case "System.UInt64":
                    return (TResult)(object)value.ToULong(roundType);
                case "System.Single":
                    return (TResult)(object)value.ToFloat(roundType, precision);
                case "System.Decimal":
                    return (TResult)(object)value.ToDecimal(roundType, precision);
                default:
                    return (TResult)(object)value.ToRound(roundType, precision);
            }
        }
        catch
        {
            return onErrorReturn;
        }
    }
}
