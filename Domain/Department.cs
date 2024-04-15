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
        private short _bathrooms;
        
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
            get { return _bathrooms; }
            set { _bathrooms = value; }
        }
    }
}
