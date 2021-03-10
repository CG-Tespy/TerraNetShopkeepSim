﻿using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/UI",
                 "Display Stages",
                 @"Displays the specified stage collection with the specified UI elements. 
If the inventory is already being displayed,
this refreshes the display.")]
[AddComponentMenu("")]
public class DisplayStages : Fungus.Command
{
    [SerializeField] StageGroup stages;
    [SerializeField] StageDisplayHub displayPrefab = null;
    [Tooltip("The part of the UI that will hold instances of the display prefab.")]
    [SerializeField] RectTransform displayHolder = null;

    public override void OnEnter()
    {
        base.OnEnter();

        ClearContents();
        PopulateContents();

        Continue();
    }

    protected virtual void ClearContents()
    {
        Transform[] contents = displayHolder.GetChildren();

        displayHolder.DetachChildren();

        foreach (var child in contents)
            Destroy(child.gameObject);
    }

    protected virtual void PopulateContents()
    {
        foreach (Stage stage in stages.Contents)
        {
            StageDisplayHub newDisplay = Instantiate(displayPrefab, displayHolder);
            newDisplay.Stage = stage;
            Canvas.ForceUpdateCanvases();
        }
    }
}
