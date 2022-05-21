using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace _5._DataBase.Scripts.Encryption
{
    public class EncryptionSystem
    {
        public static void EncryptTextToFile(string data, string pathFile,byte[] key, byte[] iv)
        {
            try
            {
                using var fStream = File.Open(pathFile, FileMode.OpenOrCreate);
                using var deSalg = DES.Create();
                var cStream = new CryptoStream(fStream, deSalg.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                var sWriter = new StreamWriter(cStream);

                sWriter.WriteLine(data);

                sWriter.Close();
                cStream.Close();
                fStream.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
            }
        }

        public static string DecryptTextFromFile(string pathFile,byte[] key, byte[] iv)
        {
            try
            {
                Debug.Log(pathFile);
                using var fStream = File.Open(pathFile, FileMode.OpenOrCreate);
                using var deSalg = DES.Create();
                var cStream = new CryptoStream(fStream, deSalg.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                var sReader = new StreamReader(cStream);

                var result = sReader.ReadToEnd();

                sReader.Close();
                cStream.Close();
                fStream.Close();

                return result;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}