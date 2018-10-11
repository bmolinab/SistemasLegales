namespace SistemasLegales.Services
{
    public interface IEncriptarServicio
    {
        string Encriptar(string CadenaEncriptar);
        string Desencriptar(string cadenaDesencriptar);
    }
}
