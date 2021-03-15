using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomerGenerator))]
public class CustomerManager : MonoBehaviour
{
    [Tooltip("Transform holding the Customers. Becomes this GameObject's if null.")]
    [SerializeField] Transform customerHolder = null;

    protected virtual void Awake()
    {
        generator = GetComponent<CustomerGenerator>();
        EnsureWeHaveCustomerHolder();
    }

    CustomerGenerator generator = null;

    protected virtual void EnsureWeHaveCustomerHolder()
    {
        if (customerHolder == null)
            customerHolder = this.transform;
    }

    public virtual void BringInCustomers(int amount)
    {
        customersInShop = generator.Generate(amount);
        CustomersEnteredShop.Invoke(customersInShop);
    }

    IList<Customer> customersInShop = null;
    public IList<Customer> CustomersInShop { get { return customersInShop; } }
    public Action<IList<Customer>> CustomersEnteredShop = delegate { };

    public virtual void MakeCustomersLeave()
    {
        while (customersInShop.Count > 0)
        {
            Customer firstInLine = customersInShop[0];
            Destroy(firstInLine.gameObject);
        }
    }

    protected virtual void OnEnable()
    {
        ListenForEvents();
    }

    protected virtual void ListenForEvents()
    {
        CustomersEnteredShop += OrganizeCustomersUnderHolder;
    }

    protected virtual void OrganizeCustomersUnderHolder(IList<Customer> customers)
    {
        for (int i = 0; i < customers.Count; i++)
        {
            Customer currentCust = customers[i];
            currentCust.transform.parent = customerHolder;
        }
    }

    protected virtual void OnDisable()
    {
        UnlistenForEvents();
    }

    protected virtual void UnlistenForEvents()
    {
        CustomersEnteredShop -= OrganizeCustomersUnderHolder;
    }

    /// <summary>
    /// Makes the customers who found nothing to buy, leave.
    /// </summary>
    public virtual void ClearCustomersWhoWantNothing()
    {
        for (int i = 0; i < customersInShop.Count; i++)
        {
            Customer customer = customersInShop[i];
            if (!customer.WantsAnything)
                MakeCustomerLeave(customer);
        }
    }

    protected virtual void MakeCustomerLeave(Customer customer)
    {
        customersInShop.Remove(customer);
        Destroy(customer.gameObject);
    }

}
