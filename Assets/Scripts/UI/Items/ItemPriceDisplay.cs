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
            UpdatePriceToDisplay();
            priceText.text = priceToDisplay.ToString();
        }
        else
            priceText.text = "N/A";
    }

    protected virtual void UpdatePriceToDisplay()
    {
        priceToDisplay = (int)(DisplayBase.Price * priceMultiplier);
    }

    public virtual int PriceDisplayed
    {
        get { return priceToDisplay; }
    }

    int priceToDisplay;
}
