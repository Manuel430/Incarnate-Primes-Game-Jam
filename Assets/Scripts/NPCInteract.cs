using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    bool inCutscene;
    bool finalActive;
    [SerializeField] GameObject interactionUI;

    [Header("OutsideScripts")]
    PlayerControlsScript playerControls;
    [SerializeField] PauseUI pause;
    [SerializeField] PlayerMovement player;

    private void Awake()
    {
        interactionUI.SetActive(false);

        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();
    }

    #region Get/Set Cutscene
    public bool GetCutscene() { return inCutscene; }
    public bool SetCutscene(bool setActive) {  return inCutscene = setActive; }
    #endregion

    public bool SetCutsceneAndInteraction(bool setActive)
    {
        player.SetCutscene(!setActive);
        SetCutscene(!setActive);

        interactionUI.SetActive(setActive);

        finalActive = setActive;
        return finalActive;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        interactionUI.SetActive(true);
        playerControls.Player.Interact.performed += Interact;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        interactionUI.SetActive(false);
        playerControls.Player.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (pause.GetPaused())
        {
            return;
        }
    }
}
