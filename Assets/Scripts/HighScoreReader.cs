using System.Collections;
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

        if (PlayerPrefs.GetInt("LastScore") > 0)
        {
            placeholder += "Last Score: " + PlayerPrefs.GetInt("LastScore") + "\n";
        }

        if (PlayerPrefs.GetInt("HighScore") > 0)
        {
            placeholder += "High Score: " + PlayerPrefs.GetInt("HighScore");
        }

        highScoreText.text = placeholder;
    }
}
