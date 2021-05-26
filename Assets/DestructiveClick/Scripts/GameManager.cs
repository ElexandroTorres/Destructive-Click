using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;

    [SerializeField] private Slider volumeControl;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameInfos;
    [SerializeField] private TextMeshProUGUI finalScore;

    private AudioSource mainMusic;
    private GameState state;
    private float spawnRate = 1.0f;
    private int score;
    private int lives;
    private bool isPaused = false;
    
    void Start()
    {
        state = GameState.MAINMENU;
        titleScreen.SetActive(true);
        mainMusic = GetComponent<AudioSource>();
        mainMusic.volume = volumeControl.value; 
        mainMusic.Play();
        volumeControl.onValueChanged.AddListener(delegate {VolumeChangeCheck(); });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(state == GameState.RUNING) 
            {
                Time.timeScale = 0;
                isPaused = true;
                state = GameState.PAUSED;
            }
            else if(state == GameState.PAUSED)
            {
                Time.timeScale = 1;
                isPaused = false;
                state = GameState.RUNING;
            }
            
            DisplayMenus(state);
        }
    }

    public void VolumeChangeCheck()
    {
        mainMusic.volume = volumeControl.value; 
    }
    
    IEnumerator SpawnObject()
    {
        while(state == GameState.RUNING)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, objects.Count);
            Instantiate(objects[index]);
        }
    }

    public void UpdateScore(int scorePoints)
    {
        score += scorePoints;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        state = GameState.GAMEOVER;
        finalScore.text = "Score: " + score;
        gameOverScreen.SetActive(true);
        gameInfos.SetActive(false);
        DisplayMenus(state);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficult)
    {
        state = GameState.RUNING;
        lives = 3;
        spawnRate /= difficult;
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = lives + " Lives";
        StartCoroutine(SpawnObject());
        
        gameInfos.SetActive(true);
        gameOverScreen.SetActive(false);
        titleScreen.SetActive(false);
    }

    public void UpdateLives()
    {
        if(lives > 0)
        {
            lives--;
            livesText.text = lives + " Lives";
        }
        else 
        {
            GameOver();
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void DisplayMenus(GameState currentState)
    {
        Color screenColor = new Color32(0, 0, 0, 0);

        if(currentState == GameState.PAUSED)
        {
            pauseScreen.SetActive(true);
            screenColor.a = 0.5f;
        }
        else if(currentState == GameState.GAMEOVER)
        {
            screenColor.a = 0.8f;
        }
        else 
        {
            pauseScreen.SetActive(false);
        }

        pauseScreen.transform.parent.gameObject.GetComponent<Image>().color = screenColor;
    }

}
