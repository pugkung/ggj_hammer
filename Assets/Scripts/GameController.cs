using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timeLimit;
    public int countInSeconds;
    public float hitTresholdVelocity;
    public float resetTresholdVectorX;
    public float checkDelay;

    public GameObject timerUI;
    public GameObject actionUI;
    public GameObject scoreUI;
    public GameObject pauseButton;
    public GameObject pauseDialog;
    public GameObject tutorialUI;
    public GameObject hammerSprite;
    public GameObject particleEffect;

    public AudioClip[] hitEffects;
    public AudioClip missEffect;
    public ParticleSystem partEffect;

    private Text timerText;
    private Text actionText;
    private Text scoreText;
    private RectTransform hammerRect;

    private int score;
    private float timer;
    private bool acceptInput;
    private bool isPaused;

    private Vector3 input;
    private Vector3 prevInput; // previous frame
    private Vector3 smoothedAccel;
    private float inputX;
    private float prevX;
    private float velocity; // d(x)
    private bool isHitting;
    private AudioSource audio;

    void Start()
    {
        initGame();
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
            timer -= Time.deltaTime;
        }
    }

    void initGame()
    {
        isHitting = false;
        score = 0;
        isPaused = false;
    }

    void mapElements()
    {
        actionText = actionUI.GetComponent<Text>();
        scoreText = scoreUI.GetComponent<Text>();
        timerText = timerUI.GetComponent<Text>();
        hammerRect = hammerSprite.GetComponent<RectTransform>();
        partEffect = particleEffect.GetComponent<ParticleSystem>();

        audio = GetComponent<AudioSource>();
    }

    void updateUIElements()
    {
        scoreText.text = "Score: " + score.ToString();
        timerText.text = Mathf.Floor((timer + 1.0f) / 1.0f).ToString();
    }

    void readInput()
    {
        prevInput = input;
        input = Input.acceleration;

        prevX = prevInput.x;
        inputX = Mathf.Abs(input.x);
        velocity = (inputX - prevX) / Time.deltaTime;

        if (velocity > hitTresholdVelocity && !isHitting)
        {
            score++;
            isHitting = true;
            makeHitFeedback();
        }
        else if (isHitting && inputX < resetTresholdVectorX)
        {
            isHitting = false;
        }

        Vector3 smoothedAccel = Vector3.Lerp(input, prevInput, 0.1f);
        animateHammer(Mathf.Abs(smoothedAccel.x));
    }

    void animateHammer(float degree)
    {
        hammerRect.rotation = Quaternion.EulerAngles(0f, 0f, degree);
    }

    void makeHitFeedback()
    {
        int rand = Mathf.FloorToInt(Random.Range(0, hitEffects.Length));
        audio.clip = hitEffects[rand];
        audio.Play();

        partEffect.Play();
        Vibration.Vibrate(50);
    }

    void gotoGameOver()
    {
        PlayerPrefs.SetInt("Rush_LastScore", score);
        if (score > PlayerPrefs.GetInt("Rush_HighScore"))
        {
            PlayerPrefs.SetInt("Rush_HighScore", score);
        }

        actionText.text = "Time Over";
        acceptInput = false;
        showGameUI(false);
        pauseDialog.SetActive(false);
        actionUI.SetActive(true);
        StartCoroutine(delayAndReturntoMainMenu());
    }

    void addScore(int points)
    {
        score += points;
    }

    IEnumerator countIn(int seconds)
    {
        showGameUI(false);
        tutorialUI.SetActive(true);
        actionUI.SetActive(true);
        pauseDialog.SetActive(false);

        yield return new WaitForSeconds(seconds);

        tutorialUI.SetActive(false);
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

    IEnumerator delayAndReturntoMainMenu()
    {
        yield return new WaitForSeconds(countInSeconds);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
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
