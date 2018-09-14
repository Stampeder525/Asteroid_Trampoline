using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EZCameraShake;


public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameObject gameOverText;
    public GameObject highScoreText;
    public GameObject comboText;
    public GameObject bombText;
    public GameObject shockwave;
    public bool gameOver = false;
    public Text scoreText;
    public int maxCombo = 8;
    public int bombs = 3;


    private int score = 0;
    private int highScore = 0;
    private int combo = 1;
    private AudioSource bombSound;
    private Animator anim;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        highScore = PlayerPrefs.GetInt("highscore");
        var audioSources = GetComponents<AudioSource>();
        bombSound = audioSources[1];
        anim = shockwave.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (gameOver)
        {
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highscore", highScore);
                highScoreText.GetComponent<Text>().text = "New Highscore: " + highScore.ToString();
                highScoreText.SetActive(true);
            }
            gameOverText.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && bombs > 0 && !gameOver)
        {
            bombSound.Play();
            shockwave.SetActive(true);
            anim.SetTrigger("BombActivated");
            CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 2f);
            bombs--;
            bombText.GetComponent<Text>().text = "Bombs: " + bombs.ToString();
            ResetCombo();
        }
    }

    public void IncrementScore()
    {
        score+= combo;
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void IncrementCombo()
    {
        if(combo == 1)
        {
            comboText.SetActive(true);
        }
        if(combo < maxCombo)
        {
            combo++; //increments twice
            comboText.GetComponent<Text>().text = "x" + combo.ToString() + " COMBO";
        }
    }

    public void ResetCombo()
    {
        combo = 1;
        comboText.SetActive(false);
    }
}
