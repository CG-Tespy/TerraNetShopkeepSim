public class ItemNameDisplay : NameDisplayComponent<Item>
{
    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            nameText.text = ((dynamic)DisplayBase).DisplayName; // Trust that the component has a name property.
        else
            nameText.text = string.Empty;
    }
}
