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
        private int _rooms;
        
        public int DoorNumber
        {
            get { return _doorNumber; }
            set { _doorNumber = value; }
        }

        public int Rooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }

        public short Bathrooms
        {
            get { return 0; }
            set { }
        }
    }
}
