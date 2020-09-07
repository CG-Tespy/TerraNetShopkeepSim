using UnityEngine;
using TMPro;

public class ItemPriceDisplay : DisplayComponent<Item>
{
    [SerializeField] TextMeshProUGUI priceText = null;

    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            priceText.text = DisplayBase.Price.ToString();
        else
            priceText.text = "N/A";
    }
}
