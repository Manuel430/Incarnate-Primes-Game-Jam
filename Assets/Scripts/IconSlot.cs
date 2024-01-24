using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class IconSlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject itemStack;
    [SerializeField] TMP_Text stacktext;

    [SerializeField] InventorySystem inventorySystem;

    public void Set(ItemsForInventory item)
    {
        itemIcon.sprite = item.item.icon;
        
        if(item.stackSize <= 1)
        {
            itemStack.SetActive(false);
            return;
        }

        stacktext.text = item.stackSize.ToString();
    }

    private void Awake()
    {
        //inventorySystem
    }
}
