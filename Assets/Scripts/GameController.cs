using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timeLimit;
    public int countInSeconds;
    public float hitTreshold;
    public float checkDelay;

    public GameObject timerUI;
    public GameObject actionUI;
    public GameObject scoreUI;
    public GameObject pauseButton;
    public GameObject pauseDialog;
    public GameObject hammerSprite;

    public AudioClip[] hitEffects;
    public AudioClip missEffect;

    private Text timerText;
    private Text actionText;
    private Text scoreText;
    private RectTransform hammerRect;

    private int score;
    private float timer;
    private bool acceptInput;
    private bool isPaused;

    void Start()
    {
        mapElements();

        showGameUI(false);
        actionUI.SetActive(true);

        acceptInput = false;
        timer = timeLimit + countInSeconds;
        StartCoroutine(countIn(countInSeconds));
    }

    void Update()
    {
        updateUIElements();
        if (!isPaused && acceptInput)
        {
            readInput();
        }
        

        if (timer <= 0.0f)
        {
            gotoGameOver();
        }
        else if (!isPaused)
        {
            // update timer
        }
    }

    void mapElements()
    {
        actionText = actionUI.GetComponent<Text>();
        scoreText = scoreUI.GetComponent<Text>();
        timerText = timerUI.GetComponent<Text>();
        hammerRect = hammerSprite.GetComponent<RectTransform>();
    }

    void updateUIElements()
    {
        scoreText.text = "Score: " + score.ToString();
        timerText.text = "Time: " + Mathf.Floor(timer / 1.0f).ToString();
    }

    void readInput()
    {
        animateHammer(75.0f);
    }

    void animateHammer(float degree)
    {
        hammerRect.rotation.Set(0, 0, degree, 0);
    }

    void gotoGameOver()
    {

    }

    void addScore(int points)
    {
        score += points;
    }

    IEnumerator countIn(int seconds)
    {
        showGameUI(false);
        actionUI.SetActive(true);
        pauseDialog.SetActive(false);

        yield return new WaitForSeconds(seconds);

        acceptInput = true;
        showGameUI(true);
        actionUI.SetActive(false);
    }

    IEnumerator makePenalty(int point, int penaltyTime)
    {
        if (score - point > 0)
        {
            score -= point;
        }
        else
        {
            score = 0;
        }

        acceptInput = false;
        yield return new WaitForSeconds(penaltyTime);
        acceptInput = true;
    }

    public void pauseGame()
    {
        isPaused = true;
        showGameUI(false);
    }

    public void resumeGame()
    {
        isPaused = false;
        showGameUI(true);
    }

    public void showGameUI(bool value)
    {
        pauseDialog.SetActive(!value);

        scoreUI.SetActive(value);
        pauseButton.SetActive(value);
        timerUI.SetActive(value);
        hammerSprite.SetActive(value);
    }
}
