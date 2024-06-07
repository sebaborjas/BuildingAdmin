using DTO.In;
using System.Collections.Generic;

namespace IImporter
{
    public interface ImporterInterface
    {
        string GetName();
        List<CreateBuildingInput> ImportFile(ImporterInput input);
    }
}
