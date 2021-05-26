using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private TrailRenderer trail;
    private BoxCollider trailCollider;
    private GameManager gameManager;
    private Camera camera;
    private Vector3 mousePos;
    private bool isSwiping = false;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        trail = GetComponent<TrailRenderer>();
        trailCollider = GetComponent<BoxCollider>();
        camera = Camera.main;

        trail.enabled = false;
        trailCollider.enabled = false;
    }

    void UpdateMousePosition()
    {
        mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                                                Mathf.Abs(camera.transform.position.z)));
        transform.position = mousePos;
    }

    void UpdateTrailComponents()
    {
        trail.enabled = isSwiping;
        trailCollider.enabled = isSwiping;
    }

    void Update()
    {
        if(!gameManager.isGameOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                UpdateTrailComponents();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                isSwiping = false;
                UpdateTrailComponents();
            }

            if(isSwiping)
            {
                UpdateMousePosition();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<ObjectsBehavior>())
        {
            collision.gameObject.GetComponent<ObjectsBehavior>().DestroyObject();
        }
    }

    private void ShowClickPoints()
    {

    }
}
