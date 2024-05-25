﻿using Domain;
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

        void DeleteBuilding(int buildingId);

        Building ModifyBuilding(int buildingId,  Building modifiedBuilding);
        
        List<Building> GetAllBuildingsForUser();

        Building Get(int id);
    }
}