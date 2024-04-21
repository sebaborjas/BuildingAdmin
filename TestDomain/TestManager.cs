﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestManager
    {
        private Manager manager;

        [TestInitialize]
        public void setUp()
        {
            manager = new Manager();
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyName()
        {
            manager.Name = "";
        }

        [TestMethod]
        public void CorrectName()
        {
            manager.Name = "NewName";
            Assert.AreEqual("", manager.Name);
        }

    }
}