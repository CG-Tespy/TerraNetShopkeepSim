using System.Collections;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/UI",
    "Display Battle Power Loadout",
    @"Displays the specified battle power loadout with the specified UI elements. 
If the loadout is already being displayed,
this refreshes the display.")]
[AddComponentMenu("")]
public class DisplayBattlePowerLoadout : Command
{
    [SerializeField] ObjectData loadout;
    [SerializeField] BattlePowerDisplayHub displayPrefab = null;
    [Tooltip("The part of the UI that will hold instances of the display prefab.")]
    [SerializeField] RectTransform displayHolder = null;
    [Tooltip("The delay between each battle power getting added.")]
    [SerializeField] float powerAddDelay = 0.01f;


    public override void OnEnter()
    {
        base.OnEnter();

        ClearContents();
        StartCoroutine(PopulateContents());

        Continue();
    }

    protected virtual void ClearContents()
    {
        Transform[] contents = displayHolder.GetChildren();

        displayHolder.DetachChildren();

        foreach (var child in contents)
            Destroy(child.gameObject);
    }

    protected virtual IEnumerator PopulateContents()
    {
        BattlePowerLoadout loadout = this.loadout.Value as BattlePowerLoadout;

        foreach (var power in loadout.Contents)
        {
            BattlePowerDisplayHub newDisplay = Instantiate(displayPrefab, displayHolder);
            newDisplay.gameObject.SetActive(true);
            newDisplay.DisplayBase = power;
            newDisplay.name = power.DisplayName;
            newDisplay.Loadout = loadout;
            Canvas.ForceUpdateCanvases();
            if (powerAddDelay > 0)
                yield return new WaitForSeconds(powerAddDelay);
        }
    }

}
