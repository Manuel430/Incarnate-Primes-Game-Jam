using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PauseUI : MonoBehaviour
{
    [SerializeField ]bool isPaused;

    [SerializeField] GameObject pauseUI;

    [SerializeField] PlayerControlsScript playerControls;
    [SerializeField] PlayerMovement player;

    public void Awake()
    {
        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();
        playerControls.Player.Pause.performed += PausingGame;
        isPaused = false;

        Time.timeScale = 1.0f;
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isPaused = false;
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
        }
    }

    public void ContinuePlay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
}
