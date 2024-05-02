using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Apartment
    {
        private int _id;
        private int _doorNumber;
        private short _rooms;
        private short _bathrooms;
        private Owner _owner;
        public short Floor { get; set; }
        public bool HasTerrace { get; set; }

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new InvalidDataException();
                }
                _id = value;
            }
        }

        public int DoorNumber
        {
            get => _doorNumber;
            set
            {
                if (value < 0)
                {
                    throw new InvalidDataException();
                }
                _doorNumber = value;
            }
        }

        public short Rooms
        {
            get => _rooms;
            set
            {
                if (value < 0)
                {
                    throw new InvalidDataException();
                }
                _rooms = value;
            }
        }

        public short Bathrooms
        {
            get => _bathrooms;
            set
            {
                if (value < 0)
                {
                    throw new InvalidDataException();
                }
                _bathrooms = value;
            }
        }


        public Owner Owner
        {
            get => _owner;
            set
            {
                if (value == null) throw new InvalidDataException();

                _owner = value;
            }
        }

    }
}
