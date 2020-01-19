namespace Kitakun.TagDiary.Services.Infrastructure
{
    using System;
    using System.Linq;
    using System.Text;

    using Kitakun.TagDiary.Core.Services;

    public class EncoderRC4 : IEncrypter
    {
        public string Encrypt(string key, string data)
        {
            Encoding unicode = Encoding.Unicode;

            return Convert
                .ToBase64String(Encrypt(unicode.GetBytes(key), unicode.GetBytes(data)));
        }

        public string Decrypt(string key, string data)
        {
            Encoding unicode = Encoding.Unicode;

            return unicode
                .GetString(Encrypt(unicode.GetBytes(key), Convert.FromBase64String(data)));
        }

        public static byte[] Encrypt(byte[] key, byte[] data) =>
            EncryptOutput(key, data);

        public static byte[] Decrypt(byte[] key, byte[] data) =>
            EncryptOutput(key, data);

        private static byte[] EncryptInitalize(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
              .Select(i => (byte)i)
              .ToArray();

            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + key[i % key.Length] + s[i]) & 255;

                Swap(s, i, j);
            }

            return s;
        }

        private static byte[] EncryptOutput(byte[] key, byte[] data)
        {
            byte[] s = EncryptInitalize(key);

            int i = 0;
            int j = 0;

            var result = new byte[data.Length];
            for (var index = 0; index < data.Length; index++)
            {
                i = (i + 1) & 255;
                j = (j + s[i]) & 255;

                Swap(s, i, j);

                result[index] = (byte)(data[index] ^ s[(s[i] + s[j]) & 255]);
            }

            return result;
        }

        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];

            s[i] = s[j];
            s[j] = c;
        }
    }
}
