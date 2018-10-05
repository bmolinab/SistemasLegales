using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Models.Utiles
{
    public static class Mensaje
    {
        public static string Satisfactorio { get { return "La acción se ha realizado satisfactoriamente."; } }
        public static string ErrorListado { get { return "Ha ocurrido un error al cargar el listado."; } }
        public static string Excepcion { get { return "Ha ocurrido una excepción."; } }
        public static string ErrorCargarDatos { get { return "Ha ocurrido un error al cargar los datos."; } }
        public static string ErrorUploadFiles { get { return "Ha ocurrido un error al subir la documentación adicional."; } }
        public static string ErrorReporte { get { return "Ha ocurrido un error al generar el reporte."; } }
        public static string ErrorPassword { get { return "Ha ocurrido un error al cambiar la contraseña."; } }
        public static string CredencialesInvalidas { get { return "Credenciales inválidas."; } }
        public static string Informacion { get { return "success"; } }
        public static string Error { get { return "error"; } }
        public static string Aviso { get { return "warning"; } }
        public static string ExisteRegistro { get { return "Existe un registro de igual información."; } }
        public static string ExisteUsuario { get { return "El usuario ya existe."; } }
        public static string BorradoNoSatisfactorio { get { return "No es posible eliminar el registro, existen relaciones que dependen de él."; } }
        public static string RegistroNoEncontrado { get { return "El registro solicitado no se ha encontrado."; } }
        public static string ModeloInvalido { get { return "El modelo es inválido."; } }
        public static string CarpetaDocumento { get { return "Requisitos"; } }
    }
}
