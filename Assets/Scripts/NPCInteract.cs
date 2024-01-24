using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class NPCInteract : MonoBehaviour
{
    bool inCutscene;
    bool finalActive;
    [SerializeField] GameObject interactionUI;
    [SerializeField] GameObject DialoguePanel;

    [Header("OutsideScripts")]
    PlayerControlsScript playerControls;
    [SerializeField] PauseUI pause;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameTimer timer;
    [SerializeField] PlayerWin pointOff;

    [Header("Dialogue Text")]
    [SerializeField] TMP_Text dialogueTemplate;
    [SerializeField] string customDialogue;
    [SerializeField] string wrongItemDialogue;
    [SerializeField] string rightItemDialogue;

    [Header("Item Checks")]
    [SerializeField] bool wrongItem;
    [SerializeField] bool rightItem;
    [SerializeField] int timeLoss;

    [Header("Animations")]
    [SerializeField] Animation npcAnimation;
    [SerializeField] bool isHappy;

    private void Awake()
    {
        interactionUI.SetActive(false);
        DialoguePanel.SetActive(false);

        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();

        npcAnimation.Play("Idle");

    }

    #region Get/Set Cutscene
    public bool GetCutscene() { return inCutscene; }
    public bool SetCutscene(bool setActive) {  return inCutscene = setActive; }
    #endregion

    public bool SetWrongItem(bool checker)
    {
        return wrongItem = checker;
    }

    public bool SetRightItem(bool checker)
    {
        return wrongItem = checker;
    }
    public bool SetCutsceneAndInteraction(bool setActive)
    {
        SetCutscene(!setActive);

        interactionUI.SetActive(!setActive);
        DialoguePanel.SetActive(setActive);

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

        SetCutsceneAndInteraction(true);

        if(DialoguePanel == true)
        {
            DialoguePanel.SetActive(false);
        }

        if(isHappy)
        {
            npcAnimation.Play("Idle_Happy");
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (pause.GetPaused())
        {
            return;
        }

        if (interactionUI.gameObject == true && inCutscene == false)
        {
            inCutscene = true;
            interactionUI.SetActive(false);

            if(wrongItem == true)
            {
                dialogueTemplate.text = wrongItemDialogue;

                timer.LossOfTime(timeLoss);
            }
            else if (rightItem == true)
            {
                dialogueTemplate.text = rightItemDialogue;

                if (isHappy == false)
                {
                    npcAnimation.Play("Laugh");
                    npcAnimation.PlayQueued("Idle_Happy");
                    isHappy = true;
                    pointOff.SubtractAmount(1);
                }
            }
            else
            {
                dialogueTemplate.text = customDialogue;
            }

            DialoguePanel.SetActive(true);
        }
    }
}
