using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private float spawnRate = 1.0f;
    private int score;

    void Start()
    {
        isGameOver = false;
        StartCoroutine(SpawnObject());
        score = 0;
        scoreText.text = "Score: " + score;
        gameOverText.gameObject.SetActive(false);
    }

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
        gameOverText.gameObject.SetActive(true);
    }
}
