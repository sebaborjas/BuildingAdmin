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
        Building CreateBuilding(Building building);

        void DeleteBuilding(int id);

        Building ModifyBuilding(int id,  Building modifiedBuilding);
    }
}
