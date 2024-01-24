using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemsForInventory
{
    public InventoryItem item {  get; private set; }
    public int stackSize { get; private set; }

    public ItemsForInventory(InventoryItem source)
    {
        item = source;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
