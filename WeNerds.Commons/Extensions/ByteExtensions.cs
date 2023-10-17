namespace WeNerds.Commons.Extensions;

public static class ByteExtensions
{
    public static int ToInt(this byte[] value, int defaultValue = 0)
    {
		try
		{
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);

            return BitConverter.ToInt32(value);
        }
		catch
		{
            return defaultValue;
        }
    }
    public static int? ToIntNullable(this byte[] value, int? defaultValue = null)
    {
        try
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);

            return BitConverter.ToInt32(value);
        }
        catch
        {
            return defaultValue;
        }
    }
    public static int BitToInt(this byte[] value, int startAt, int lenght, int defaultResult = -1)
    {
        try
        {
            if (value == null)
                return defaultResult;

            if (startAt < 0)
                return defaultResult;

            if (lenght < 1)
                return defaultResult;

            var totalBits = value.Length * 8;

            var bitsConcatenated = 0;
            foreach (var currentByte in value)
                bitsConcatenated = (bitsConcatenated << 8) | currentByte;


            return (bitsConcatenated >> (totalBits - startAt - lenght)) & ((1 << lenght) - 1);
        }
        catch (Exception)
        {

            return defaultResult;
        }


    }
    public static int? BitToIntNullable(this byte[] value, int startAt, int lenght, int? defaultResult = null)
    {
        try
        {
            if (value == null)
                return defaultResult;

            if (startAt < 0)
                return defaultResult;

            if (lenght < 1)
                return defaultResult;

            var totalBits = value.Length * 8;

            var bitsConcatenated = 0;
            foreach (var currentByte in value)
                bitsConcatenated = (bitsConcatenated << 8) | currentByte;


            return (bitsConcatenated >> (totalBits - startAt - lenght)) & ((1 << lenght) - 1);
        }
        catch (Exception)
        {

            return defaultResult;
        }


    }
    public static int BitToInt(this byte value, int startAt, int lenght, int defaultResult = -1)
    {
        try
        {
            if (startAt < 0)
                return defaultResult;

            if (lenght < 1)
                return defaultResult;

            return (value >> (8 - startAt - lenght)) & ((1 << lenght) - 1);
        }
        catch
        {
            return defaultResult;
        }

    }
    public static int? BitToIntNullable(this byte value, int startAt, int lenght, int? defaultResult = null)
    {
        try
        {
            if (startAt < 0)
                return defaultResult;

            if (lenght < 1)
                return defaultResult;

            return (value >> (8 - startAt - lenght)) & ((1 << lenght) - 1);
        }
        catch
        {
            return defaultResult;
        }

    }

}
