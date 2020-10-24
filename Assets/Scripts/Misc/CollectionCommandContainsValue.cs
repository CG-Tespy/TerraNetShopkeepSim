using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Does the collection contain the given variable
    /// </summary>
    [CommandInfo("Collection",
                 "Contains Value",
                     "Does the collection contain the value in the given variable")]
    [AddComponentMenu("")]
    public class CollectionCommandContainsValue : CollectionBaseVarCommand
    {
        [VariableProperty(typeof(BooleanVariable))]
        [SerializeField] protected BooleanVariable result;

        protected override void OnEnterInner()
        {
            if (result == null)
            {
                Debug.LogWarning("No result var set");
            }
            else
            {
                result.Value = collection.Value.Contains(variableToUse.GetValue());
            }
        }

        public override bool HasReference(Variable variable)
        {
            return result == variable || base.HasReference(variable);
        }
    }
}