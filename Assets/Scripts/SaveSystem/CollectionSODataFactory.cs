using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public abstract class CollectionSODataFactory<TSaveData, TItem, TItemDatabase, TItemHolder, THolderDatabase>
    where TSaveData : ICollectionSOData, new()
    where TItem: Item
    where TItemDatabase: CollectionSO<TItem>
    where TItemHolder : CollectionSO<TItem>
    where THolderDatabase: CollectionSO<TItemHolder>
{

    public static TSaveData CreateFrom(TItemHolder itemHolder, TItemDatabase itemDatabase,
        THolderDatabase holderDatabase)
    {
        TSaveData result = new TSaveData();
        result.ContentIndexes = GetContentIndexes(itemHolder, itemDatabase, holderDatabase);
        result.CollectionName = itemHolder.name;
        result.CollectionIndex = holderDatabase.IndexOf(itemHolder);

        return result;
    }

    public static IList<int> GetContentIndexes(TItemHolder itemHolder, TItemDatabase itemDatabase,
        THolderDatabase holderDatabase)
    {
        IList<int> indexes = new List<int>();

        foreach (TItem item in itemHolder.Contents)
        {
            if (!itemDatabase.Contains(item))
                AlertDatabaseNotHaving(item, itemDatabase);

            indexes.Add(itemDatabase.IndexOf(item));
        }

        return indexes;
    }

    protected static void AlertDatabaseNotHaving(TItem item, TItemDatabase database)
    {
        string messageFormat =  "{0} {1} does not have the {2} {3}";
        string message = string.Format(messageFormat, itemDatabaseTypeName, database.Name, itemTypeName, item.name);
        throw new System.ArgumentException(message);
    }

    
    protected static string itemDatabaseTypeName = typeof(TItemDatabase).Name;
    protected static string itemTypeName = typeof(TItem).Name;
    protected static string itemHolderTypename = typeof(TItemHolder).Name;
    protected static string holderDatabaseTypeName = typeof(THolderDatabase).Name;
}
