using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Department
    {
        private int _doorNumber;
        public int DoorNumber
        {
            get { return _doorNumber; }
            set { _doorNumber = value; }
        }

        public int Rooms
        {
            get { return 0; }
            set { }
        }
    }
}
