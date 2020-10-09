using UnityEngine;

public class BattlePowerLoadoutDisplay : MonoBehaviour
{
    [Tooltip("What will hold the displays.")]
    [SerializeField] RectTransform displayHolder = null;
    [SerializeField] BattlePowerDisplayHub hubPrefab = null;
    [SerializeField] BattlePowerLoadout toDisplay = null;

    void Start()
    {
        DisplayLoadout();
    }

    void DisplayLoadout()
    {
        var powers = toDisplay.Powers;

        for (int i = 0; i < powers.Count; i++)
        {
            var currentPower = powers[i];
            AddDisplayHubWith(currentPower);
        }
    }


    void AddDisplayHubWith(BattlePower power)
    {
        var newHub = Instantiate(hubPrefab, displayHolder, false);
        newHub.BattlePower = power;
    }
}
