using SistemasLegales.Models.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Services
{
    public interface IUploadFileService
    {
        Task<bool> UploadFile(byte[] file, string folder, string fileName);
        Task<bool> UploadFiles(DocumentoRequisitoTransfer documentoRequisitoTransfer);
        bool DeleteFile(string url);
        string FileExtension(string fileName);
        DocumentoRequisitoTransfer GetFileDocumentoRequisito(string folder, int idDocumentoRequisito, string fileName);
        Task<DocumentoRequisitoTransfer> GetFileDocumentoRequisito(int idDocumentoRequisito);
        Task<bool> DeleteDocumentoRequisito(int idDocumentoRequisito);
    }
}
