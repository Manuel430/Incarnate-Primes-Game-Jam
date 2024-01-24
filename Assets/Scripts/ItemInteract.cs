using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteract : MonoBehaviour
{
    PlayerControlsScript playerControl;
    [SerializeField] GameObject interactionUI;
    [SerializeField] GameObject parentObject;

    //[SerializeField] InventoryItem pickedItem;
    //[SerializeField] InventorySystem inventorySystem;

    private void Awake()
    {
        interactionUI.SetActive(false);
        playerControl = new PlayerControlsScript();
        playerControl.Player.Enable();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) { return; }

        interactionUI.SetActive(true);
        playerControl.Player.Interact.performed += CollectItem;
    }

    public void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) { return; }

        interactionUI.SetActive(false);
        playerControl.Player.Interact.performed -= CollectItem;
    }

    private void CollectItem(InputAction.CallbackContext context)
    {
        interactionUI.SetActive(false);
        //inventorySystem.Add(pickedItem);
        Destroy(parentObject);
    }
}
