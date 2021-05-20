using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultButton : MonoBehaviour
{
    private Button difficultButton;
    private GameManager gameManager;
    [SerializeField] private int difficult;
    
    void Start()
    {
        difficultButton = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        difficultButton.onClick.AddListener(SetDifficult);
    }

    void SetDifficult()
    {
        gameManager.StartGame(difficult);
    }
}
