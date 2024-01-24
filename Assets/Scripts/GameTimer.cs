using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] float maxTime;
    float remainingTime;
    bool countDown;

    [Header("UI")]
    [SerializeField] GameObject gameOverUI;

    [Header("Scripts")]
    [SerializeField] PlayerMovement player;

    private void Awake()
    {
        gameOverUI.SetActive(false);
        countDown = true;
        remainingTime = maxTime;
    }

    private void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);

        if (countDown)
        {
            if (remainingTime > 1)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 1)
            {
                remainingTime = 0;
                player.SetCutscene(true);
                //Play death animation before stopping the whole game
                gameOverUI.SetActive(true);
            }
        }
    }

    public bool SetCountDown(bool setActive)
    {
        return countDown = setActive;
    }

    public void LossOfTime(int timeLoss)
    {
        remainingTime -= timeLoss;
    }
}
