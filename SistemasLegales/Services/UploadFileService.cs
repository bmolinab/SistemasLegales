using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Utiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SistemasLegalesContext db;

        public UploadFileService(IHostingEnvironment environment, SistemasLegalesContext db)
        {
            _hostingEnvironment = environment;
            this.db = db;
        }

        #region Métodos internos
        public async Task<bool> UploadFile(byte[] file, string folder, string fileName)
        {
            try
            {
                var stream = new MemoryStream(file);
                var targetDirectory = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                var targetFile = Path.Combine(targetDirectory, fileName);

                if (!Directory.Exists(targetDirectory))
                    Directory.CreateDirectory(targetDirectory);

                using (var fileStream = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
                    await stream.CopyToAsync(fileStream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFile(string url)
        {
            try
            {
                var targetDirectory = Path.Combine(_hostingEnvironment.WebRootPath, url);
                if (File.Exists(targetDirectory))
                {
                    File.Delete(targetDirectory);
                    return true;
                }
            }
            catch (Exception)
            { }
            return false;
        }

        public string FileExtension(string fileName)
        {
            try
            {
                string[] arr = fileName.Split('.');
                return $".{arr[arr.Length - 1]}";
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        private byte[] GetBytesFromFile(string folder, string fileName)
        {
            try
            {
                var targetDirectory = Path.Combine(_hostingEnvironment.WebRootPath, $"{folder}\\{fileName}");
                var file = new FileStream(targetDirectory, FileMode.Open, FileAccess.Read);

                byte[] data;
                using (var br = new BinaryReader(file))
                    data = br.ReadBytes((int)file.Length);

                return data;
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        public DocumentoRequisitoTransfer GetFileDocumentoRequisito(string folder, int idDocumentoRequisito, string fileName)
        {
            try
            {
                string extensionFile = FileExtension(fileName);
                byte[] data = GetBytesFromFile(folder, $"{idDocumentoRequisito}{extensionFile}");
                if (data.Length > 0)
                {
                    return new DocumentoRequisitoTransfer
                    {
                        Nombre = fileName,
                        Fichero = data
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Métodos externos (se utilizan para llamarlos desde los controladores)
        public async Task<bool> UploadFiles(DocumentoRequisitoTransfer documentoRequisitoTransfer)
        {
            try
            {
                var documentoRequisito = new DocumentoRequisito
                {
                    Nombre = documentoRequisitoTransfer.Nombre,
                    Fecha = DateTime.Now,
                    IdRequisito = documentoRequisitoTransfer.IdRequisito
                };
                db.DocumentoRequisito.Add(documentoRequisito);
                await db.SaveChangesAsync();

                string extensionFile = FileExtension(documentoRequisitoTransfer.Nombre);
                await UploadFile(documentoRequisitoTransfer.Fichero, Mensaje.CarpetaDocumento, $"{documentoRequisito.IdDocumentoRequisito}{extensionFile}");

                var seleccionado = await db.DocumentoRequisito.FindAsync(documentoRequisito.IdDocumentoRequisito);
                seleccionado.Url = $"{Mensaje.CarpetaDocumento}/{documentoRequisito.IdDocumentoRequisito}{extensionFile}";
                db.DocumentoRequisito.Update(seleccionado);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                Debug.Write(ex.Message);

            }
            return false;
        }

        public async Task<DocumentoRequisitoTransfer> GetFileDocumentoRequisito(int idDocumentoRequisito)
        {
            try
            {
                var documentoRequisito = await db.DocumentoRequisito.FirstOrDefaultAsync(c => c.IdDocumentoRequisito == idDocumentoRequisito);
                return GetFileDocumentoRequisito(Mensaje.CarpetaDocumento, idDocumentoRequisito, documentoRequisito.Nombre);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteDocumentoRequisito(int idDocumentoRequisito)
        {
            try
            {
                var respuesta = await db.DocumentoRequisito.SingleOrDefaultAsync(m => m.IdDocumentoRequisito == idDocumentoRequisito);
                if (respuesta == null)
                    return false;

                var respuestaFile = DeleteFile(respuesta.Url);
                db.DocumentoRequisito.Remove(respuesta);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}