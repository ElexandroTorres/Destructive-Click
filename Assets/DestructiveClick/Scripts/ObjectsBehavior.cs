using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBehavior : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    private Rigidbody objectRb;
    private GameManager gameManager;
    [SerializeField] private int destructionPoints;
    [SerializeField] private float yPositionSpawn = -2;
    private float minForce = 12;
    private float maxForce = 16;
    private float torqueRange = 10;
    private float xRangeSpawn = 4;
    private float yLimit = -7.0f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objectRb = GetComponent<Rigidbody>();

        objectRb.AddForce(RandomForce(), ForceMode.Impulse);

        objectRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPosition();
    }

    void Update()
    {
        OutOfScreen();
    }

    private void OutOfScreen()
    {
        if(transform.position.y < yLimit)
        {
            if(!this.gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLives();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        if(!gameManager.isGameOver)
        {
            Destroy(this.gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            gameManager.UpdateScore(destructionPoints);
        }
    }

    public void DestroyObject()
    {
        if(!gameManager.isGameOver)
        {
            Destroy(this.gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            gameManager.UpdateScore(destructionPoints);
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    private float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRangeSpawn, xRangeSpawn), yPositionSpawn);
    }
}
