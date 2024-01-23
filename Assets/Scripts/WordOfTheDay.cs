using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WordOfTheDay : MonoBehaviour
{
    [SerializeField] TMP_Text wordOfTheDay;
    [SerializeField] string[] textOptions;

    private void Awake()
    {
        string wordToDisplay = RandomWord();

        wordOfTheDay.text = wordToDisplay;
    }

    private string RandomWord()
    {
        string randomWord = textOptions[UnityEngine.Random.Range(0, textOptions.Length)];
        return randomWord;
    }
}
