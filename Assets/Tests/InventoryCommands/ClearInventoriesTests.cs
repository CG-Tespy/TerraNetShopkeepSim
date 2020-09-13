using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ClearInventoriesTests : InventoryTestSuite
    {
        
        [UnityTest]
        public IEnumerator ClearJustOneInventory()
        {
            var firstInventory = inventories[0];
            
            // See how many items the first inventory had before
            var countBefore = firstInventory.Items.Count;
            // Call the command through a block in the flowchart
            string blockName = "ClearOneInventory";
            testFlowchart.ExecuteBlock(blockName);

            yield return new WaitForSeconds(delay);
            // See how many items the inventory then has
            var countAfter = firstInventory.Items.Count;

            // Assert that it has less items... 0, to be exact
            var hasLessItems = countAfter < countBefore;
            var noItemsLeft = countAfter == 0;
            var success = hasLessItems && noItemsLeft;

            Assert.IsTrue(success);
            
        }

        [UnityTest]
        public IEnumerator ClearMultipleInventories()
        {
            int[] itemCountsBefore = GetInventoryItemCounts();
            string blockName = "ClearAllInventories";
            testFlowchart.ExecuteBlock(blockName);
            
            yield return new WaitForSeconds(delay);

            int[] itemCountsAfter = GetInventoryItemCounts();

            var success = HasOnlyPositiveNums(itemCountsBefore) && HasOnlyZeroes(itemCountsAfter);
            Assert.IsTrue(success);
            
        }

        int[] GetInventoryItemCounts()
        {
            int[] itemCounts = new int[inventories.Count];

            for (int i = 0; i < inventories.Count; i++)
            {
                itemCounts[i] = inventories[i].Items.Count;
            }

            return itemCounts;
        }

        bool HasOnlyPositiveNums(int[] arr)
        {
            foreach (var num in arr)
                if (num <= 0)
                    return false;

            return true;
        }

        bool HasOnlyZeroes(int[] arr)
        {
            foreach (var num in arr)
                if (num != 0)
                    return false;

            return true;
        }

    }
}
