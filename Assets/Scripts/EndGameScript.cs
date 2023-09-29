using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private TMP_Text winLoseTxt;
    private void Start()
    {
        winLoseTxt.text = PlayerPrefs.GetString("WinLose");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
