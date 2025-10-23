using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>();

    public void Add(string itemID)
    {
        items.Add(itemID);
    }

    public bool Has(string itemID)
    {
        return items.Contains(itemID);
    }
}
