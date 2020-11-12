using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays info for the last battle card clicked in the scene.
/// </summary>
public class BattlePowerInfoDisplay : MonoBehaviour
{
    [SerializeField] Image art = null;
    [SerializeField] TextMeshProUGUI elementText = null, damageText = null, healingText = null;
    [SerializeField] string separator = ", ";

    protected virtual void Awake()
    {
        DisplayHub.AnyClicked.AddListener(OnAnyDisplayHubClicked);
        ClearDisplays();
    }

    void OnAnyDisplayHubClicked(IDisplayHub hub)
    {
        BattlePowerDisplayHub powerHub = hub as BattlePowerDisplayHub;

        bool powerNotClicked = powerHub == null;
        if (powerNotClicked)
            return;

        Power = powerHub.DisplayBase;
    }

    BattlePower Power
    {
        set
        {
            power = value;
            UpdateDisplays();
        }
    }

    BattlePower power = null;

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
            elementNames = string.Concat(elementNames, element.Name, separator);
        }

        elementNames = elementNames.Substring(0, elementNames.Length - separator.Length);
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

}
