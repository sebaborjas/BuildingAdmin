using IImporter;
using Domain;

namespace IServices
{
    public interface IImportService
    {
        List<ImporterInterface> GetAllImporters();

        List<Building> ImportBuildings(string importerName, string path);
    }
}
