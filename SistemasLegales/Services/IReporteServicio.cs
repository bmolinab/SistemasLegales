using System.Collections.Generic;

namespace SistemasLegales.Services
{
    public interface IReporteServicio
    {
        Dictionary<string, string> GetDefaultParameters(string FolderAndNameReport);
        Dictionary<string, string> AddParameters(string Key, string Value, Dictionary<string, string> parameters);
        string GenerateUri(string ProjectReportUrl, Dictionary<string, string> parameters);
        string GenerateUri(Dictionary<string, string> parameters);
    }
}
