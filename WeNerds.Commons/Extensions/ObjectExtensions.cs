using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeNerds.Commons.Extensions;

public static class ObjectExtensions
{
    public static string GetEnumDescription<T>(this T enumerationValue)
    {
        if (enumerationValue is null)
            return null;

        var type = enumerationValue.GetType();

        //TODO Usar arquivos de resources para mensagem
        if (!type.IsEnum)
            throw new ArgumentException("Argumento deve ser um Enumerador.", nameof(enumerationValue));

        var memberInfo = type.GetMember(enumerationValue.ToString());

        if (memberInfo is not null && memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs is not null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }

        return enumerationValue.ToString();
    }

    public static string Serialize(this object value, bool formatted = false)
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = formatted,

        };
        return JsonSerializer.Serialize(value, options);
    }


    public static Guid NewGuidV7(this object _, DateTimeOffset? dateTimeOffset = null)
    {
        // bytes [0-5]: datetimeoffset yyyy-MM-dd hh:mm:ss fff
        // bytes [6]: 4 bits dedicated to guid version (version: 7)
        // bytes [6]: 4 bits dedicated to random part
        // bytes [7-15]: random part

        if (dateTimeOffset == null)
            dateTimeOffset = DateTimeOffset.UtcNow;

        var uuidAsBytes = new byte[16];
        FillTimePart(ref uuidAsBytes, (DateTimeOffset) dateTimeOffset);
        var random_part = uuidAsBytes.AsSpan().Slice(6);
        RandomNumberGenerator.Fill(random_part);
        // add mask to set guid version
        uuidAsBytes[6] &= 0x0F;
        uuidAsBytes[6] += 0x70;
        return new Guid(uuidAsBytes);
    }
    private static void FillTimePart(ref byte[] uuidAsBytes, DateTimeOffset dateTimeOffset)
    {
        long currentTimestamp = dateTimeOffset.ToUnixTimeMilliseconds();
        byte[] current = BitConverter.GetBytes(currentTimestamp);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(current);
        current[2..8].CopyTo(uuidAsBytes, 0);
    }
}
