using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject titleScreen;
    private float spawnRate = 1.0f;
    private int score;
    private int lives;
    
    IEnumerator SpawnObject()
    {
        while(!isGameOver)
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
        isGameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficult)
    {
        lives = 3;
        spawnRate /= difficult;
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        isGameOver = false;
        StartCoroutine(SpawnObject());
        
        gameOverScreen.SetActive(false);
        titleScreen.SetActive(false);
    }

    public void UpdateLives()
    {
        if(lives > 0)
        {
            lives--;
            livesText.text = "Score: " + lives;
        }
        else 
        {
            GameOver();
        }
    }
}
