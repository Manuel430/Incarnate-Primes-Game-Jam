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
    [SerializeField] GameObject theirItem;

    [Header("OutsideScripts")]
    PlayerControlsScript playerControls;
    [SerializeField] PauseUI pause;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameTimer timer;
    [SerializeField] PlayerWin pointOff;
    [SerializeField] NPCItemSelect decision; 

    [Header("Dialogue Text")]
    [SerializeField] TMP_Text dialogueTemplate;
    [SerializeField] string customDialogue;
    [SerializeField] string wrongItemDialogue_01;
    [SerializeField] string wrongItemDialogue_02;
    [SerializeField] string rightItemDialogue;

    [Header("Item Checks")]
    [SerializeField] bool wrongItem_01;
    [SerializeField] bool wrongItem_02;
    [SerializeField] bool rightItem;
    [SerializeField] int timeLoss;

    [Header("Animations")]
    [SerializeField] Animation npcAnimation;
    [SerializeField] bool isHappy;

    [Header("Audio Sources")]
    [SerializeField] AudioSource sadInteract;
    [SerializeField] AudioSource happyInteract;
    [SerializeField] AudioSource wrongTalk;
    [SerializeField] AudioSource rightTalk;

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

    public bool SetWrongItem_01(bool checker)
    {
        return wrongItem_01 = checker;
    }

    public bool SetWrongItem_02(bool checker)
    {
        return wrongItem_02 = checker;
    }

    public bool SetRightItem(bool checker)
    {
        return rightItem = checker;
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

            if (!isHappy)
            {
                decision.ItemDecider();
            }

            if (wrongItem_01 == true && rightItem == false)
            {
                dialogueTemplate.text = wrongItemDialogue_01;
                wrongTalk.Play();
                sadInteract.Play();
                timer.LossOfTime(timeLoss);
            }
            else if(wrongItem_02 == true && rightItem == false)
            {
                dialogueTemplate.text = wrongItemDialogue_02;
                wrongTalk.Play();
                sadInteract.Play();
                timer.LossOfTime(timeLoss);
            }
            else if (rightItem == true)
            {

                dialogueTemplate.text = rightItemDialogue;
                happyInteract.Play();

                if (isHappy == false)
                {
                    rightTalk.Play();
                    npcAnimation.Play("Laugh");
                    npcAnimation.PlayQueued("Idle_Happy");
                    isHappy = true;
                    pointOff.SubtractAmount(1);
                }
            }
            else
            {
                dialogueTemplate.text = customDialogue;
                sadInteract.Play();
            }

            DialoguePanel.SetActive(true);
        }
    }
}
