using UnityEngine;
using TMPro;

public abstract class NameDisplayComponent<TDisplayFor> : DisplayComponent<TDisplayFor>
{
    protected virtual void Awake()
    {
        if (nameText == null)
            nameText = GetComponent<TextMeshProUGUI>();

        UpdateDisplay();
    }

    [SerializeField] TextMeshProUGUI nameText = null;

    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            nameText.text = ((dynamic)DisplayBase).Name; // Trust that the component has a name property.
        else
            nameText.text = string.Empty;
    }
    
}
