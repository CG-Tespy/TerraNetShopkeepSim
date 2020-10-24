﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays info for the last battle card clicked in the scene.
/// </summary>
public class BattlePowerInfoDisplay : MonoBehaviour
{
    BattlePower power = null;
    [SerializeField] Image art = null;
    [SerializeField] TextMeshProUGUI elementText = null, damageText = null, healingText = null;

    BattlePower Power
    {
        set
        {
            power = value;
            UpdateDisplays();
        }
    }

    protected virtual void Awake()
    {
        DisplayHub.AnyClicked.AddListener(OnAnyDisplayHubClicked);
        ClearDisplays();
    }

    void ClearDisplays()
    {
        elementText.text = "";
        damageText.text = "";
        healingText.text = "";
    }

    protected virtual void OnDestroy()
    {
        DisplayHub.AnyClicked.RemoveListener(OnAnyDisplayHubClicked);
    }

    void OnAnyDisplayHubClicked(IDisplayHub hub)
    {
        BattlePowerDisplayHub powerHub = hub as BattlePowerDisplayHub;

        bool powerNotClicked = powerHub == null;
        if (powerNotClicked)
            return;

        Power = powerHub.DisplayBase;
    }

    protected virtual void UpdateDisplays()
    {
        DisplayPic();
        DisplayElements();
        DisplayDamage();
        DisplayHealing();
    }

    void DisplayPic()
    {
        art.sprite = power.FullArt;
    }

    void DisplayElements()
    {
        string elementNames = "";
        foreach (Element element in power.Elements)
        {
            elementNames = string.Concat(elementNames, element.Name, ", ");
        }

        elementNames = elementNames.Substring(0, elementNames.Length - 2);
        // ^ To remove the extra comma at the end
        elementText.text = elementNames;
    }

    void DisplayDamage()
    {
        damageText.text = power.Damage + "";
    }

    void DisplayHealing()
    {
        healingText.text = power.Healing + "";
    }
    
}