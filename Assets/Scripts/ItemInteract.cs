using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemInteract : MonoBehaviour
{
    PlayerControlsScript playerControl;
    [SerializeField] GameObject interactionUI;
    [SerializeField] GameObject parentObject;
    [SerializeField] GameObject itemIcon;
    [SerializeField] PlayerMovement player;


    private void Awake()
    {
        interactionUI.SetActive(false);
        playerControl = new PlayerControlsScript();
        playerControl.Player.Enable();
        itemIcon.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) { return; }

        player.CanGrab(true);
        interactionUI.SetActive(true);
        playerControl.Player.Interact.performed += CollectItem;
    }

    public void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) { return; }

        player.CanGrab(false);
        interactionUI.SetActive(false);
        playerControl.Player.Interact.performed -= CollectItem;
    }

    private void CollectItem(InputAction.CallbackContext context)
    {
        interactionUI.SetActive(false);
        itemIcon.SetActive(true);
        //insventorySystem.Add(pickedItem);
        Destroy(parentObject);
    }
}
