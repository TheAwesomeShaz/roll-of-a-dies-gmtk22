using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool hasStarted;
    public bool hasDied;
    UIManager uiManager;
    public static GameController instance;

    public int numberOfKnives;
    public int currentDimension;


    //game should be won in dimensions 0123 to be won
    public bool[] gamesWon;
    bool allGamesWon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
        UIManager.Instance.ChangeControlsView(UIManager.gameState.initialState);
    }

    private void Update()
    {
        // Debug stuff ofc ofc
        if (Input.GetKeyDown(KeyCode.Space) && hasDied)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            hasDied = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !hasStarted)
        {
            UIManager.Instance.ChangeControlsView(UIManager.gameState.movingState);
            hasStarted = true;
        }
       
    }

    public void DimensionWin()
    {
        print("GameWon in Dimension "+currentDimension);
        gamesWon[currentDimension] = true;

        if (AllDimensionsWon())
        {
            //display win screen
        }
    }

    private bool AllDimensionsWon()
    {
        foreach (bool gameWin in gamesWon)
        {
            if (!gameWin)
            {
                allGamesWon = false;
            }
        }
        return allGamesWon;
    }

}
