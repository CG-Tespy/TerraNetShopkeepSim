using Fungus;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

[EventHandlerInfo("SaveSys", "Save Slot Clicked", "Trigger this block when a save slot is clicked.")]
public class SaveSlotClicked : EventHandler
{
    [Tooltip("Whether or not the save slot has any save data linked to it.")]
    [VariableProperty(typeof(BooleanVariable))]
    [SerializeField] protected BooleanVariable slotHasData;

    protected virtual void OnEnable()
    {
        WatchForClicks();
    }

    protected virtual void WatchForClicks()
    {
        Signals.SaveSlotClicked += OnSaveSlotClicked;
    }

    public virtual void OnSaveSlotClicked(SaveSlot slotClicked)
    {
        if (slotHasData != null)
            slotHasData.Value = slotClicked.SaveData != null;

        ExecuteBlock();
    }

    protected virtual void OnDisable()
    {
        UnwatchForClicks();
    }

    protected virtual void UnwatchForClicks()
    {
        Signals.SaveSlotClicked -= OnSaveSlotClicked;
    }

    
}
