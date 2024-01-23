using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PauseUI : MonoBehaviour
{
    bool isPaused;

    [SerializeField] GameObject pauseUI;

    [SerializeField] PlayerControlsScript playerControls;
    [SerializeField] PlayerMovement player;

    public void Awake()
    {
        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();
        playerControls.Player.Pause.performed += PausingGame;
    }

    public bool GetPaused() { return isPaused; }

    private void PausingGame(InputAction.CallbackContext context)
    {
        if (player.GetCutscene())
        {
            return;
        }

        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
        }
    }
}
