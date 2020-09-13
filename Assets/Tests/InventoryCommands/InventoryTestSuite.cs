using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Fungus;

namespace Tests
{
    public class InventoryTestSuite
    {
        protected float delay = 1f;
        string testScenePrefabPath = "Prefabs/Scenes/InventoryTestScene";
        Transform testScenePrefab = null;
        protected Transform testScene = null;

        string testFlowchartName = "InventoryTestFlowchart";
        protected Flowchart testFlowchart = null;

        protected List<ShopInventory> inventoryBackups = new List<ShopInventory>();
        protected List<ShopInventory> inventories = new List<ShopInventory>();

        [SetUp]
        public virtual void TestSetup()
        {
            SetUpScene();
            SetUpFlowchart();
            SetUpInventories();
        }

        protected virtual void SetUpScene()
        {
            testScenePrefab = Resources.Load<Transform>(testScenePrefabPath);
            testScene = MonoBehaviour.Instantiate(testScenePrefab);
        }

        protected virtual void SetUpFlowchart()
        {
            var flowchartGo = GameObject.Find(testFlowchartName);
            testFlowchart = flowchartGo.GetComponent<Flowchart>();
        }

        protected virtual void SetUpInventories()
        {
            // Go through the test flowchart's variables, register the ones with 
            // inventories
            foreach (var fcVar in testFlowchart.GetVariables<ShopInventoryVariable>())
            {
                inventories.Add(fcVar.Value);
                inventoryBackups.Add(MonoBehaviour.Instantiate(fcVar.Value));
            }
        }

        [TearDown]
        public virtual void AfterTest()
        {
            ResetInventoryStates();
        }

        protected virtual void ResetInventoryStates()
        {
            for (int i = 0; i < inventories.Count; i++)
            {
                var actualInventory = inventories[i];
                var backupInventory = inventoryBackups[i];
                actualInventory.Items.AddRange(backupInventory.Items);
            }
        }
        
    }
}
