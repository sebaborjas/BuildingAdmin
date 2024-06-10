using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IBuildingService
    {
        Building CreateBuilding(Building building, string? managerEmail = null);

        void DeleteBuilding(int buildingId);

        Building ModifyBuilding(int buildingId, Building modifiedBuilding);

        List<Building> Get(int? buildingId);

        String GetManagerName(int buildingId);

        public void ChangeBuildingManager(int buildingId, int managerId);
    }
}
