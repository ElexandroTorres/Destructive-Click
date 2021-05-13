using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBehavior : MonoBehaviour
{
    private Rigidbody _objectRb;
    private int _minForce = 12;
    private int _maxForce = 16;
    private int _minTorque = -10;
    private int _maxTorque = 10;
    private int _minXPosition = -4;
    private int _maxXPosition = 4;
    private int _yPosition = -6;

    void Start()
    {
        _objectRb = GetComponent<Rigidbody>();

        _objectRb.AddForce(Vector3.up * Random.Range(_minForce, _maxForce), ForceMode.Impulse);

        int xTorque = Random.Range(_minTorque, _maxTorque);
        int yTorque = Random.Range(_minTorque, _maxTorque);
        int zTorque = Random.Range(_minTorque, _maxTorque);
        _objectRb.AddTorque(xTorque, yTorque, zTorque, ForceMode.Impulse);

        transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), _yPosition);
    }

    void Update()
    {
        if(transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
