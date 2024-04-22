using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Manager : User
        
    {
        private List<Building> _buildings;
        public List<Building> Buildings
        {
            get { return _buildings; }
            set { _buildings = value; }
        }
    }
}
