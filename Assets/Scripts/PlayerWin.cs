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
    [SerializeField] GameObject CanWinUI;
    [SerializeField] GameObject collectionUI;
    [SerializeField] GameObject frogMayor;

    private void Awake()
    {
        allSadPeople = GameObject.FindGameObjectsWithTag("SadNPC").Length;
        winUI.SetActive(false);
        CanWinUI.SetActive(false);
        frogMayor.SetActive(false);
    }

    public void SubtractAmount(int amount)
    {
        allSadPeople -= amount;

        if(allSadPeople == 0)
        {
            CanWinUI.SetActive(true);
            collectionUI.SetActive(false);
            frogMayor.SetActive(true);
        }
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
            winUI.SetActive(true);
            CanWinUI.SetActive(false);
            gameTimer.gameObject.SetActive(false);
            //player.GameWin();
        }
    }
}
