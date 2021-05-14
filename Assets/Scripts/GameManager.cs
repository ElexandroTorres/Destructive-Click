using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float spawnRate = 1.0f;
    private int score;

    void Start()
    {
        StartCoroutine(SpawnObject());
        score = 0;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnObject()
    {
        while(true)
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
}
