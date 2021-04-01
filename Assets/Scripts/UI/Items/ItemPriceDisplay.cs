using UnityEngine;
using TMPro;

public class ItemPriceDisplay : DisplayComponent<Item>
{
    [SerializeField] protected TextMeshProUGUI priceText = null;
    [Tooltip("Applied to the price shown")]
    [SerializeField] protected float priceMultiplier = 1;

    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
        {
            int priceToDisplay = (int)(DisplayBase.Price * priceMultiplier);
            priceText.text = priceToDisplay.ToString();
        }
        else
            priceText.text = "N/A";
    }
}
