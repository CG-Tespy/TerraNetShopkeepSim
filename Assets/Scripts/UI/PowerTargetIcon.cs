using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles displaying an icon for the target a controller was set with.
/// </summary>
[RequireComponent(typeof(Image))]
public class PowerTargetIcon : MonoBehaviour
{
    [SerializeField] BattlePowerController controller = null;
    FighterController Target {  get { return controller.Target; } }
    Image pic = null;

    protected virtual void Awake()
    {
        pic = GetComponent<Image>();
    }

    protected virtual void OnEnable()
    {
        ListenForPowerEvents();
    }

    protected virtual void ListenForPowerEvents()
    {
        controller.TargetSet += UpdateAppearance;
    }

    protected virtual void UpdateAppearance(FighterController target)
    {
        if (target == null)
            HideThis();
        else
            ShowThis();

        UpdatePicSource();
    }

    protected virtual void HideThis()
    {
        pic.color = Color.clear;
    }

    protected virtual void ShowThis()
    {
        pic.color = Color.white; // <- Setting it like so just renders the pic's colors normally
    }

    protected virtual void UpdatePicSource()
    {
        Sprite newSource = null;

        if (Target != null)
            newSource = Target.Mugshot;

        pic.sprite = newSource;
    }

    protected virtual void OnDestroy()
    {
        UnlistenForPowerEvents();
    }

    protected virtual void UnlistenForPowerEvents()
    {
        controller.TargetSet -= UpdateAppearance;
    }
}
