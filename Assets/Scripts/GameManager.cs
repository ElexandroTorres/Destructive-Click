using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    private float spawnRate = 1.0f;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    void Update()
    {
        
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
}
