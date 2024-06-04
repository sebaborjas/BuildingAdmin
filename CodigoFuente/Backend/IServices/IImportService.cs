using IImporter;
using Domain;
using DTO.Out;

namespace IServices
{
    public interface IImportService
    {
        List<ImporterInterface> GetAllImporters();

        ImporterOutput ImportBuildings(string importerName, string path);
    }
}
