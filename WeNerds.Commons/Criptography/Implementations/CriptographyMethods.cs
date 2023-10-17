using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using WeNerds.Commons.Criptography.Interfaces;
using WeNerds.Commons.Extensions;

namespace WeNerds.Commons.Criptography.Implementations;

public class CriptographyMethods : ICriptographyMethods
{
    private readonly string _salt;
    private readonly string _cryptoKey;
    private readonly int _interations;    

    public CriptographyMethods(IConfiguration configuration)
    {
        _salt = configuration.GetSection("WeNerds:Criptography:Salt").Value.GetValueOrDefault("b7d72229-5d7b-4889-893d-635ebadd05cc");
        _cryptoKey = configuration.GetSection("WeNerds:Criptography:Key").Value.GetValueOrDefault("ce3b2eea-6677-4446-90cc-798c0443216d");
        _interations = configuration.GetSection("WeNerds:Criptography:Interations").Value.GetValueOrDefault("1000").ToInt();        
    }

    public string Encrypt(string text)
    {
        var key = new Rfc2898DeriveBytes(_cryptoKey, Encoding.UTF8.GetBytes(_salt), _interations, HashAlgorithmName.SHA512).GetBytes(32);

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Mode = CipherMode.ECB;

            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
    public bool CompareText(string text, string encryptedText)
    {
        text = Encrypt(text);
        return text == encryptedText;
    }
}
