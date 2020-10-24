public class SellTacticImageDisplay : ImageDisplayComponent<SellTactic>
{
    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            displaySpriteOn.sprite = DisplayBase.Icon;
    }
}
