using UnityEngine;
using UnityEngine.UI;
using TMPText = TMPro.TextMeshProUGUI;

public class CustomerDisplay : MonoBehaviour
{
    [SerializeField] Image mugshot = null;
    [SerializeField] TMPText descriptionField = null;
    [SerializeField] TMPText nameField = null;
    [SerializeField] ItemDisplayHub itemDisplayer = null;
    Customer customer = null;
    public virtual Customer Customer
    {
        get { return customer; }
        set
        {
            customer = value;
            Refresh();
        }
    }

    protected virtual void Refresh()
    {
        mugshot.sprite = Customer.Mugshot;
        descriptionField.text = customer.Description;
        nameField.text = customer.name;
        itemDisplayer.Item = customer.WantedItem;
    }


}
