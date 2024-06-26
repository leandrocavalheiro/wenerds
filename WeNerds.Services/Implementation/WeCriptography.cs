using Microsoft.Extensions.Configuration;
using WeNerds.Services.Interfaces;
using WeNerds.Commons.Extensions;
using System.Security.Cryptography;
using System.Text;
using WeNerds.Commons;
namespace WeNerds.Services.Implementation;

public class WeCriptography(IConfiguration configuration) : IWeCriptography
{    
    private readonly string _salt = configuration[WeConstants.EV_NAME_CRYPT_SALT].GetValueOrDefault(WeConstants.DEFAULT_CRYPT_SALT);
    private readonly string _cryptoKey = configuration[WeConstants.EV_NAME_CRYPT_KEY].GetValueOrDefault(WeConstants.DEFAULT_CRYPT_KEY);    
    private readonly int _interations = configuration[WeConstants.EV_NAME_CRYPT_INTERATIONS].GetValueOrDefault(WeConstants.DEFAULT_CRYPT_INTERATIONS.ToString()).ToInt();

    public string Encrypt(string text, string salt = null)
    {
        if (string.IsNullOrWhiteSpace (salt))
            salt = _salt;

        var key = new Rfc2898DeriveBytes(_cryptoKey, Encoding.UTF8.GetBytes(salt), _interations, HashAlgorithmName.SHA512).GetBytes(32);

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
    public bool CompareText(string text, string encryptedText, string salt = null)
    {
        text = Encrypt(text, salt);
        return text == encryptedText;
    }
}
