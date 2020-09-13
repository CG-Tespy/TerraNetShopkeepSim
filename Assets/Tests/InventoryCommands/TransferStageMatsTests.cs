using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TransferStageMatsTests : InventoryTestSuite
    {
        string blockName = "TransferStageMats";
        string stageVarName = "testStage";
        Stage testStage = null;
        IList<Item> stageMats = null;

        public override void TestSetup()
        {
            base.TestSetup();
            var stageVar = testFlowchart.GetVariable<StageVariable>(stageVarName);
            testStage = stageVar.Value;
            stageMats = testStage.MatsGatherable;
        }

        [UnityTest]
        public IEnumerator TransferStageMatsSuccess()
        {
            var items = inventories[0].Items;
            var countBefore = items.Count;
            // See that the inventory gained as many items as there were mats
            // in the stage, and that those last items are said mats
            testFlowchart.ExecuteBlock(blockName);

            yield return new WaitForSeconds(delay);

            var countAfter = items.Count;

            var gainedRightAmount = countAfter == countBefore + stageMats.Count;
            bool gainedRightItems = true;

            for (int i = 0; i < stageMats.Count; i++)
            {
                var mat = stageMats[i];
                var itemAdded = items[items.Count + i - stageMats.Count];
                if (itemAdded != mat)
                {
                    gainedRightItems = false;
                    break;
                }
            }

            var success = gainedRightAmount && gainedRightItems;
            Assert.IsTrue(success);
        }
    }
}
