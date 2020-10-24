using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Remove an item to a collection
    /// </summary>
    [CommandInfo("Collection",
                 "Remove Value",
                     "Remove a item with the specified value from a collection")]
    [AddComponentMenu("")]
    public class CollectionCommandRemoveValue : CollectionBaseVarCommand
    {
        [Tooltip("Should it remove ALL occurances of the value")]
        [SerializeField]
        protected BooleanData allOccurances = new BooleanData(false);

        protected override void OnEnterInner()
        {
            if (allOccurances.Value)
                collection.Value.RemoveAll(variableToUse.GetValue());
            else
                collection.Value.Remove(variableToUse.GetValue());
        }

        public override bool HasReference(Variable variable)
        {
            return allOccurances.booleanRef == variable || base.HasReference(variable);
        }

        public override string GetSummary()
        {
            return base.GetSummary() + (allOccurances.Value ? " ALL" : "");
        }
    }
}