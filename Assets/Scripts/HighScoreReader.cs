﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreReader : MonoBehaviour
{
    public GameObject highScoreUI;

    private Text highScoreText;
    private string placeholder;

    void Start()
    {
        highScoreText = highScoreUI.GetComponent<Text>();

        placeholder = "";

        if (PlayerPrefs.GetInt("Rush_LastScore") > 0)
        {
            placeholder += "Last Score: " + PlayerPrefs.GetInt("Rush_LastScore") + "\n";
        }

        if (PlayerPrefs.GetInt("HighScore") > 0)
        {
            placeholder += "High Score: " + PlayerPrefs.GetInt("Rush_HighScore");
        }

        highScoreText.text = placeholder;
    }
}
