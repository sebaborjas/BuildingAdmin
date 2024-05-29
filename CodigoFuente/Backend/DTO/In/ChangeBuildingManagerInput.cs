using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class ChangeBuildingManagerInput
    {
        public int ManagerId { get; set; }

        public ChangeBuildingManagerInput ToEntity()
        {
            return new ChangeBuildingManagerInput
            {
                ManagerId = ManagerId
            };
        }
    }

}
