﻿using System;
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
        private short _floor;
        private bool _hasTerrace;
        
        public int DoorNumber
        {
            get { return _doorNumber; }
            set {
                if (value < 0)
                {
                    throw new InvalidDataException();
                }
                _doorNumber = value;
            }
        }

        public int Rooms
        {
            get { return _rooms; }
            set { 
                if(value < 0)
                {
                    throw new InvalidDataException();
                }
                _rooms = value; 
            }
        }

        public short Bathrooms
        {
            get { return _bathrooms; }
            set {
                if(value < 0)
                {
                    throw new InvalidDataException();
                }
                _bathrooms = value; 
            }
        }

        public short Floor
        {
            get { return _floor; }
            set { _floor = value; }
        }

        public bool HasTerrace
        {
            get { return _hasTerrace; }
            set { _hasTerrace = value; }
        }
    }
}