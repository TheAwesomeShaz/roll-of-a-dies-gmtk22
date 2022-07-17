using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header ("Knife related UI")]
    public GameObject knifeImage;
    public TextMeshProUGUI knivesCountText;

    [Header ("Alert Texts")]
    public GameObject dimensionClearedText;
    public GameObject dimensionShiftText;
    public GameObject diceText;
    public GameObject gameLoseText;
    public GameObject gameWinText;

    [Header("Controls UI")]
    public GameObject WkeyImage;
    public GameObject AkeyImage;
    public GameObject SkeyImage;
    public GameObject DkeyImage;

    public GameObject WkeyPressedImage;
    public GameObject AkeyPressedImage;
    public GameObject SkeyPressedImage;
    public GameObject DkeyPressedImage;

    public GameObject shiftImage;
    public GameObject spaceImage;

    public GameObject shiftPressedImage;
    public GameObject spacePressedImage;

    [Header("Controls Text")]

    public GameObject moveText;
    public GameObject restartText;
    public GameObject knifeShootText;
    public GameObject aimText;
    public GameObject selectEnemyText;



    public enum gameState
    {
        initialState, //0
        movingState, //1
        aimingState, //2
        deadState, //3
    }

    public gameState currentState;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeControlsView(gameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case gameState.initialState:
                HandleInitialStateUI(); //to disable all
                break;
            case gameState.movingState:
                HandleInitialStateUI(); //to disable all
                HandleMovingStateUI();
                break;

            case gameState.aimingState:
                HandleInitialStateUI(); //to disable all
                HandleAimingStateUI();
                break;
            case gameState.deadState:
                HandleInitialStateUI(); //to disable all
                HandleDeadStateUI();
                break;
        }

    }

    private void HandleInitialStateUI()
    {
        knivesCountText.gameObject.SetActive(false);
        knifeImage.SetActive(false);

        WkeyImage.SetActive(false);
        AkeyImage.SetActive(false);
        SkeyImage.SetActive(false);
        DkeyImage.SetActive(false);
        shiftImage.SetActive(false);
        spaceImage.SetActive(false);

        moveText.SetActive(false);
        aimText.SetActive(false);
        knifeShootText.SetActive(false);
        selectEnemyText.SetActive(false);
        restartText.SetActive(false);

    }

    private void HandleDeadStateUI()
    {
        spaceImage.SetActive(true);    
        restartText.SetActive(true);
    }

    private void HandleAimingStateUI()
    {
        shiftImage.SetActive(true);
        spaceImage.SetActive(true);
        AkeyImage.SetActive(true);
        DkeyImage.SetActive(true);

        selectEnemyText.SetActive(true);
        aimText.SetActive(true);
        knifeShootText.SetActive(true);
        knivesCountText.gameObject.SetActive(true);
        knifeImage.SetActive(true);
    }
    
    private void HandleMovingStateUI()
    {
        WkeyImage.SetActive(true);
        AkeyImage.SetActive(true);
        SkeyImage.SetActive(true);
        DkeyImage.SetActive(true);
        shiftImage.SetActive(true);

        moveText.SetActive(true);
        aimText.SetActive(true);

        knivesCountText.gameObject.SetActive(true);
        knifeImage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        knivesCountText.text = "x" + GameController.instance.numberOfKnives.ToString();
        HandleVisualKeyFeedback();

        //only for debug purposes
        DebugChangeState();

    }

    private void DebugChangeState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeControlsView(gameState.initialState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeControlsView(gameState.movingState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeControlsView(gameState.aimingState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeControlsView(gameState.deadState);
        }
    }

    private void HandleVisualKeyFeedback()
    {
        // W key visual feedback
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            WkeyPressedImage.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            WkeyPressedImage.SetActive(false);
        }

        // A key visual feedback
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AkeyPressedImage.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            AkeyPressedImage.SetActive(false);
        }

        // S key visual feedback
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SkeyPressedImage.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            SkeyPressedImage.SetActive(false);
        }

        // D key visual feedback
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            DkeyPressedImage.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            DkeyPressedImage.SetActive(false);
        }

        //Shift key visual feedback
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            shiftPressedImage.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            shiftPressedImage.SetActive(false);
        }

        // Space key visual feedback
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressedImage.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spacePressedImage.SetActive(false);
        }
    }
}
