﻿using System.Security.Cryptography;
using System.Text;

namespace Ark.SharedLib.Common.Helpers;

/// <summary>
///     Class for encrypt decrypt string
/// </summary>
public static class StringCipherHelper
{
    #region Private Fields

    // This constant determines the number of iterations for the password bytes generation function.
    private const int DERIVATION_ITERATIONS = 1000;

    // This constant is used to determine the keysize of the encryption algorithm in bits.
    // We divide this by 8 within the code below to get the equivalent number of bytes.
    private const int KEY_SIZE = 128;

    private const int KEY_BLOCK_SIZE = 128;
    private const string SALT_STATIC = "6b0a8357eadc112454a7ad96666f8c36";

    private const int DEFAULT_SALT_LENGTH = 8;

    #endregion Private Fields

    #region Private Methods

    private static byte[] Generate256BitsOfRandomEntropy() => RandomNumberGenerator.GetBytes(32);

    private static byte[] Generate128BitsOfRandomEntropy() => RandomNumberGenerator.GetBytes(16);

    #endregion Private Methods

    #region Public Methods

    /// <summary>
    ///     Decrypts an encrypted string based on the given salt
    /// </summary>
    /// <param name="encryptedText">Encrypted text</param>
    /// <param name="salt">Salt</param>
    /// <returns>Decrypted text</returns>
    public static string Decrypt(string encryptedText, string salt)
    {
        // Get the complete stream of bytes that represent:
        // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(encryptedText);
        // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KEY_SIZE / 8).ToArray();
        // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KEY_SIZE / 8).Take(KEY_SIZE / 8).ToArray();
        // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(KEY_SIZE / 8 * 2)
                                                          .Take(cipherTextBytesWithSaltAndIv.Length - KEY_SIZE / 8 * 2)
                                                          .ToArray();

        using (var password = new Rfc2898DeriveBytes(salt, saltStringBytes, DERIVATION_ITERATIONS))
        {
            var keyBytes = password.GetBytes(KEY_SIZE / 8);
            using (var symmetricKey = Aes.Create())
            {
                symmetricKey.BlockSize = KEY_BLOCK_SIZE;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                using (var memoryStream = new MemoryStream(cipherTextBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    var plainTextBytes = new byte[cipherTextBytes.Length];
                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    memoryStream.Close();
                    cryptoStream.Close();
                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                }
            }
        }
    }

    /// <summary>
    ///     Decrypts an encrypted string based on autogenerated salt based on private salt size
    /// </summary>
    /// <param name="encryptedText">Encrypted text</param>
    /// <returns>Decrypted text</returns>
    public static string DecryptWithRandomSalt(string encryptedText)
    {
        var salt = encryptedText.Substring(0, DEFAULT_SALT_LENGTH);
        var valueToDecrypt = encryptedText.Substring(DEFAULT_SALT_LENGTH);
        return Decrypt(valueToDecrypt, salt);
    }

    /// <summary>
    ///     Decrypts an encrypted string based on static salt declared to a private field
    /// </summary>
    /// <param name="encryptedText">Encrypted text</param>
    /// <returns>Decrypted text</returns>
    public static string DecryptWithStaticSalt(string encryptedText) => Decrypt(encryptedText, SALT_STATIC);

    /// <summary>
    ///     Encrypts a string based on given salt
    /// </summary>
    /// <param name="textToEncrypt">Plain text</param>
    /// <param name="salt">Salt</param>
    /// <returns>Encrypted text</returns>
    public static string Encrypt(string textToEncrypt, string salt)
    {
        // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
        // so that the same Salt and IV values can be used when decrypting.
        var saltStringBytes =
            KEY_BLOCK_SIZE == 128 ? Generate128BitsOfRandomEntropy() : Generate256BitsOfRandomEntropy();
        var ivStringBytes = KEY_BLOCK_SIZE == 128 ? Generate128BitsOfRandomEntropy() : Generate256BitsOfRandomEntropy();
        var plainTextBytes = Encoding.UTF8.GetBytes(textToEncrypt);
        using (var password = new Rfc2898DeriveBytes(salt, saltStringBytes, DERIVATION_ITERATIONS))
        {
            var keyBytes = password.GetBytes(KEY_SIZE / 8);
            using (var symmetricKey = Aes.Create())
            {
                symmetricKey.BlockSize = KEY_BLOCK_SIZE;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                    var cipherTextBytes = saltStringBytes;
                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                    memoryStream.Close();
                    cryptoStream.Close();
                    return Convert.ToBase64String(cipherTextBytes);
                }
            }
        }
    }

    /// <summary>
    ///     Encrypts a string based on autogenerated salt based on private salt size
    /// </summary>
    /// <param name="textToEncrypt">Plain text</param>
    /// <returns>Encrypted text</returns>
    public static string EncryptWithRandomSalt(string textToEncrypt)
    {
        var salt = GetRandomSalt(DEFAULT_SALT_LENGTH);
        var encryptedText = Encrypt(textToEncrypt, salt);

        return $"{salt}{encryptedText}";
    }

    /// <summary>
    ///     Encrypts a string based on static salt declared to a private field
    /// </summary>
    /// <param name="textToEncrypt">Plain text</param>
    /// <returns>Encrypted text</returns>
    public static string EncryptWithStaticSalt(string textToEncrypt) => Encrypt(textToEncrypt, SALT_STATIC);

    /// <summary>
    ///     Created random salt of given length
    /// </summary>
    /// <param name="length">Salt length</param>
    /// <returns>Salt</returns>
    public static string GetRandomSalt(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var res = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var data = RandomNumberGenerator.GetBytes(4);
            var value = BitConverter.ToUInt32(data, 0);
            res.Append(valid[(int)(value % (uint)valid.Length)]);
        }

        return res.ToString();
    }

    #endregion Public Methods
}