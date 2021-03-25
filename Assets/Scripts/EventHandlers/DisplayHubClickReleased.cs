using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

/// <summary>
/// For when the click button is released while on a hub
/// </summary>
public abstract class DisplayHubClickReleased<THub, TDisplayBase> : DisplayHubClicked<THub, TDisplayBase> where THub : DisplayHub<TDisplayBase>

{
    // Best refactor this do it doesn't inherit from DisplayHubClicked... There must be some
    // composition solution we can go with, minimizing copy-pasting betwen this class and DisplayHubClicked
    protected override void Awake()
    {
        DisplayHub.AnyClickRelease.AddListener(OnAnyDisplayHubClicked);
    }

    protected override void OnDestroy()
    {
        DisplayHub.AnyClicked.RemoveListener(OnAnyDisplayHubClicked);
    }
}
