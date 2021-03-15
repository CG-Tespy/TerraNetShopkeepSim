using UnityEngine;
using Fungus;

public abstract class ShopInventoryContentsChanged : EventHandler
{
    [SerializeField] protected ShopInventoryData[] inventories;
    [Tooltip("The added item gets assigned to this variable.")]
    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] protected ItemVariable itemVar;

    protected virtual void OnEnable()
    {
        ListenForChanges();
    }

    protected abstract void ListenForChanges();

    protected virtual void OnChangeHappened(Item item)
    {
        if (itemVar != null)
            itemVar.Value = item;
        ExecuteBlock();
    }

    protected virtual void OnDisable()
    {
        UnlistenForChanges();
    }

    protected abstract void UnlistenForChanges();
}
