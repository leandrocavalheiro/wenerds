namespace WeNerds.Services.Interfaces;

public interface IWeCriptography
{
    string Encrypt(string text, string sdalt = null);
    bool CompareText(string text, string encryptedText, string salt = null);
}
