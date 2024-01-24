using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] int allSadPeople;
    [SerializeField] GameObject winUI;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameTimer gameTimer;
    [SerializeField] GameObject TimerUI;

    private void Awake()
    {
        allSadPeople = GameObject.FindGameObjectsWithTag("SadNPC").Length;
        winUI.SetActive(false);
    }

    public void SubtractAmount(int amount)
    {
        allSadPeople -= amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if(allSadPeople == 0)
        {
            player.SetCutscene(true);
            gameTimer.SetCountDown(false);
            TimerUI.SetActive(false);
            //Win Animation
            winUI.SetActive(true);
        }
    }
}
