using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary <InventoryItem, ItemsForInventory> m_inventoryItems;
    public List<ItemsForInventory> inventory { get; private set; }

    private void Awake()
    {
        inventory = new List<ItemsForInventory>();
        m_inventoryItems = new Dictionary<InventoryItem, ItemsForInventory>();
    }

    public ItemsForInventory Get(InventoryItem item)
    {
        if(m_inventoryItems.TryGetValue(item, out ItemsForInventory value))
        {
            return value;
        }

        return null;
    }

    public void Add(InventoryItem referenceItem)
    {
        if(m_inventoryItems.TryGetValue(referenceItem, out ItemsForInventory value))
        {
            value.AddToStack();
        }
        else
        {
            ItemsForInventory newItem = new ItemsForInventory(referenceItem);
            inventory.Add(newItem);
            m_inventoryItems.Add(referenceItem, newItem);
        }
    }

    public void Remove(InventoryItem referenceItem)
    {
        if(m_inventoryItems.TryGetValue(referenceItem, out ItemsForInventory value))
        {
            value.RemoveFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                m_inventoryItems.Remove(referenceItem);
            }
        }
    }
}
