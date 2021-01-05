public class StageNameDisplay : NameDisplayComponent<Stage>
{
    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            nameText.text = DisplayBase.DisplayName; // Trust that the component has a name property.
        else
            nameText.text = string.Empty;
    }
}
