using System;

namespace SistemasLegales.Services
{
    public class EncriptarServicio : IEncriptarServicio
    {
        public string Desencriptar(string cadenaDesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadenaDesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public string Encriptar(string cadenaEncriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaEncriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
    }
}
