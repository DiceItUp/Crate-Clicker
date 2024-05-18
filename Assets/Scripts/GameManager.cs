using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1;
    public TextMeshProUGUI scoreText, gameOverText, timerText;
    private int score;
    public bool gameIsActive;
    public Button restartButton;
    public GameObject titleScreen, pauseScreen;
    public int timer = 60;
    public AudioSource playerAudio;
    public AudioClip goodSound, badSound;
    public Slider volumeSlider, volSlider2;
    public GameObject volumeSliderGO, VolsliderGO2;
    public static float sliderValue = 0.25f;
    public GameObject playerCamera;
    public static bool paused = false;
   

    // Start is called before the first frame update
    private void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void Start()
    {

        
        playerCamera = GameObject.Find("Main Camera");
        playerAudio = playerCamera.GetComponent<AudioSource>();
        volSlider2.value = sliderValue;
        volumeSlider.value = sliderValue;
        playerAudio.volume = sliderValue;


    }

    // Update is called once per frame
    void Update()
    {
        if(titleScreen.activeInHierarchy)
        {
            sliderValue = volumeSlider.value;
            volSlider2.value = sliderValue;
            playerAudio.volume = sliderValue;
        }
        else if(pauseScreen.activeInHierarchy)
        {

            sliderValue = volSlider2.value;
            volumeSlider.value = sliderValue;
            playerAudio.volume = sliderValue;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gameIsActive)
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (scoreToAdd < 0)
         playerAudio.PlayOneShot(badSound);
        else if (scoreToAdd > 0)
         playerAudio.PlayOneShot(goodSound);

        score += scoreToAdd;
        scoreText.text = "Score: " + score;

    }
    public void GameOver() 
    {
        gameIsActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    IEnumerator Timer()
    {
        while(timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
            timerText.text = "Time: " + timer;
        }
        GameOver();
    }
    IEnumerator SpawnTarget()
    {
        while (gameIsActive) { 
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0, 4);
        Instantiate(targets[index]);
    }
    }
    public void RestartGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        gameIsActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(score);
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        StartCoroutine(Timer());
        timerText.text = "Time: " + timer;
    }
    public void Resume()
    {
        
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    void Pause()
    {
        
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
