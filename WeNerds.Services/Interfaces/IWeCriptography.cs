namespace WeNerds.Services.Interfaces;

public interface IWeCriptography
{
    string Encrypt(string text);
    bool CompareText(string text, string encryptedText);
}
