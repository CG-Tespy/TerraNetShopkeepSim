using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField] Customer[] customerPool = null;

    /// <summary>
    /// Generates customers randomly and returns a list of the ones created.
    /// </summary>
    public virtual IList<Customer> Generate(int amount)
    {
        IList<Customer> generated = new List<Customer>();

        for (int i = 0; i < amount; i++)
        {
            Customer toGenerate = GetRandomCustomerType();
            Customer broughtIntoWorld = BringCustomerIntoWorld(toGenerate);
            RemoveCloneFromName(broughtIntoWorld);
            generated.Add(broughtIntoWorld);
        }

        return generated;
    }

    protected virtual Customer GetRandomCustomerType()
    {
        int randIndex = Random.Range(0, customerPool.Length - 1);
        return customerPool[randIndex];
    }

    protected virtual Customer BringCustomerIntoWorld(Customer customer)
    {
        Customer broughtIntoWorld = Instantiate<Customer>(customer);
        return broughtIntoWorld;
    }

    protected virtual void RemoveCloneFromName(Customer customer)
    {
        customer.name = customer.name.Replace(clone, "").Trim();
    }

    protected static string clone = "(Clone)";
}
