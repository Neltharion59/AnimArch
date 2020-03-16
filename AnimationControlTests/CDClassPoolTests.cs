using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class CDClassPoolTests
    {
        [TestMethod]
        public void GetClassInstanceById_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance Class1_Instance1 = Class1.CreateClassInstance("originator1");
            Class1_Instance1.SetAttribute("coord_x", "17.65");
            Class1_Instance1.SetAttribute("coord_y", "-2.22");
            Class1_Instance1.SetAttribute("coord_z", "804.96");

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", Class1_Instance1.UniqueID);

            Dictionary<String, String> ActualOutput = FoundInstance.State;
            Dictionary<String, String> ExpectedOutput = Class1_Instance1.State;

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void GetClassInstanceById_Normal_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance Class1_Instance1 = Class1.CreateClassInstance("originator1");
            Class1_Instance1.SetAttribute("coord_x", "17.65");
            Class1_Instance1.SetAttribute("coord_y", "-2.22");
            Class1_Instance1.SetAttribute("coord_z", "804.96");

            CDClassInstance Class1_Instance2 = Class1.CreateClassInstance("originator2");
            Class1_Instance2.SetAttribute("coord_x", "0.564738291");
            Class1_Instance2.SetAttribute("coord_y", "-503.14");
            Class1_Instance2.SetAttribute("coord_z", "22.458");

            CDClassInstance Class1_Instance3 = Class1.CreateClassInstance("originator3");
            Class1_Instance3.SetAttribute("coord_x", "350.47");
            Class1_Instance3.SetAttribute("coord_y", "-920.166");
            Class1_Instance3.SetAttribute("coord_z", "202.7");

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", Class1_Instance1.UniqueID);

            Dictionary<String, String> ActualOutput = FoundInstance.State;
            Dictionary<String, String> ExpectedOutput = Class1_Instance1.State;

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void GetClassInstanceById_Normal_03()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance Class1_Instance1 = Class1.CreateClassInstance("originator1");
            Class1_Instance1.SetAttribute("coord_x", "17.65");
            Class1_Instance1.SetAttribute("coord_y", "-2.22");
            Class1_Instance1.SetAttribute("coord_z", "804.96");

            CDClassInstance Class1_Instance2 = Class1.CreateClassInstance("originator2");
            Class1_Instance2.SetAttribute("coord_x", "0.564738291");
            Class1_Instance2.SetAttribute("coord_y", "-503.14");
            Class1_Instance2.SetAttribute("coord_z", "22.458");

            CDClassInstance Class1_Instance3 = Class1.CreateClassInstance("originator3");
            Class1_Instance3.SetAttribute("coord_x", "350.47");
            Class1_Instance3.SetAttribute("coord_y", "-920.166");
            Class1_Instance3.SetAttribute("coord_z", "202.7");

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", Class1_Instance2.UniqueID);

            Dictionary<String, String> ActualOutput = FoundInstance.State;
            Dictionary<String, String> ExpectedOutput = Class1_Instance2.State;

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void GetClassInstanceById_Normal_04()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance Class1_Instance1 = Class1.CreateClassInstance("originator1");
            Class1_Instance1.SetAttribute("coord_x", "17.65");
            Class1_Instance1.SetAttribute("coord_y", "-2.22");
            Class1_Instance1.SetAttribute("coord_z", "804.96");

            CDClassInstance Class1_Instance2 = Class1.CreateClassInstance("originator2");
            Class1_Instance2.SetAttribute("coord_x", "0.564738291");
            Class1_Instance2.SetAttribute("coord_y", "-503.14");
            Class1_Instance2.SetAttribute("coord_z", "22.458");

            CDClassInstance Class1_Instance3 = Class1.CreateClassInstance("originator3");
            Class1_Instance3.SetAttribute("coord_x", "350.47");
            Class1_Instance3.SetAttribute("coord_y", "-920.166");
            Class1_Instance3.SetAttribute("coord_z", "202.7");

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", Class1_Instance3.UniqueID);

            Dictionary<String, String> ActualOutput = FoundInstance.State;
            Dictionary<String, String> ExpectedOutput = Class1_Instance3.State;

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void GetClassInstanceById_Bad_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance Class1_Instance1 = Class1.CreateClassInstance("originator1");
            Class1_Instance1.SetAttribute("coord_x", "17.65");
            Class1_Instance1.SetAttribute("coord_y", "-2.22");
            Class1_Instance1.SetAttribute("coord_z", "804.96");

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", Class1_Instance1.UniqueID + 1);

            Assert.IsNull(FoundInstance);
        }
        [TestMethod]
        public void GetClassInstanceById_Bad_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance Class1_Instance1 = Class1.CreateClassInstance("originator1");
            Class1_Instance1.SetAttribute("coord_x", "17.65");
            Class1_Instance1.SetAttribute("coord_y", "-2.22");
            Class1_Instance1.SetAttribute("coord_z", "804.96");

            CDClassInstance Class1_Instance2 = Class1.CreateClassInstance("originator2");
            Class1_Instance2.SetAttribute("coord_x", "0.564738291");
            Class1_Instance2.SetAttribute("coord_y", "-503.14");
            Class1_Instance2.SetAttribute("coord_z", "22.458");

            CDClassInstance Class1_Instance3 = Class1.CreateClassInstance("originator3");
            Class1_Instance3.SetAttribute("coord_x", "350.47");
            Class1_Instance3.SetAttribute("coord_y", "-920.166");
            Class1_Instance3.SetAttribute("coord_z", "202.7");

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", Class1_Instance3.UniqueID + 1);

            Assert.IsNull(FoundInstance);
        }
        [TestMethod]
        public void GetClassInstanceById_Bad_03()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", 1000);

            Assert.IsNull(FoundInstance);
        }
        [TestMethod]
        public void GetClassInstanceById_Bad_04()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClass Class2 = ExecutionSpace.SpawnClass("Memento");
            Class2.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));

            CDClass Class3 = ExecutionSpace.SpawnClass("Pool");
            Class3.AddAttribute(new CDAttribute("unit_count", EXETypes.IntegerTypeName));
            Class3.AddAttribute(new CDAttribute("building_count", EXETypes.IntegerTypeName));

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("Originator", 1000);

            Assert.IsNull(FoundInstance);
        }
        [TestMethod]
        public void GetClassInstanceById_Bad_05()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Originator");
            Class1.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_y", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("coord_z", EXETypes.RealTypeName));

            CDClass Class2 = ExecutionSpace.SpawnClass("Memento");
            Class2.AddAttribute(new CDAttribute("coord_x", EXETypes.RealTypeName));

            CDClass Class3 = ExecutionSpace.SpawnClass("Pool");
            Class3.AddAttribute(new CDAttribute("unit_count", EXETypes.IntegerTypeName));
            Class3.AddAttribute(new CDAttribute("building_count", EXETypes.IntegerTypeName));

            CDClassInstance FoundInstance = ExecutionSpace.GetClassInstanceById("NonExistentClass", 1000);

            Assert.IsNull(FoundInstance);
        }
    }
}