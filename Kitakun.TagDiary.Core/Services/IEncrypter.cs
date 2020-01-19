namespace Kitakun.TagDiary.Core.Services
{
    public interface IEncrypter
    {
        string Encrypt(string key, string data);

        string Decrypt(string key, string data);
    }
}
