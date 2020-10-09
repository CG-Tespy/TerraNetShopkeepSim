public class BattlePowerImageDisplay : ImageDisplayComponent<BattlePower>
{
    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            displaySpriteOn.sprite = DisplayBase.FullArt;
    }
}
