using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //these were part of the old code
    //public float moveSpeed = 1f;
    //public Vector3 desiredPosition;
    //Animator anim;

    Destructible destructible;
  
    public int speed = 300;
    public GameObject deathVFX;
    public GameObject deathCam;
    public GameObject enemyPickerCam;
    public GameObject throwingKnife;

    [Header("booleans")]

    bool isMoving = false;
    public bool canTakeMovementInput;
    public bool canTakeAimInput;
    public bool canThrowKnife;
    public bool throwKnifeInput;
    private bool cannotThrowKnifeInput;
    bool initialMovementEnabled;

    public bool isMovingForward;
    public bool isMovingBack;
    public bool isMovingLeft;
    public bool isMovingRight;

    public bool canMoveFront;
    public bool canMoveBack;
    public bool canMoveLeft;
    public bool canMoveRight;

    [Header("Delay between Knife Throws")]

    public float initialDelayTimer = 1f;
    public float delayTimer = 0f;

    private void Start()
    {
        deathCam.SetActive(false);
        enemyPickerCam.SetActive(false);
        destructible = GetComponent<Destructible>();
        delayTimer = 0;
        
    }

    void Update()
    {
        HandleGameStarted();
        LimitMovement();
        if (canTakeAimInput) HandleEnemyPickerCam();
        if (canTakeMovementInput) HandleMovement();
    }

    private void HandleGameStarted()
    {
        if (!initialMovementEnabled && !canTakeMovementInput && GameController.instance.hasStarted)
        {
            canTakeMovementInput = true;
            canTakeAimInput = true;
            initialMovementEnabled = true;
        }
    }

    private void HandleEnemyPickerCam()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            canTakeMovementInput = false;
            enemyPickerCam.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            enemyPickerCam.SetActive(false);
            canTakeMovementInput = true;
        }

        

        throwKnifeInput = (
                (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                (Input.GetKeyDown(KeyCode.Space))
                );

        
        if (GameController.instance.numberOfKnives > 0)
        {
            delayTimer -= Time.deltaTime;
            if(delayTimer <= 0)
            {
                canThrowKnife = true;
            }
            if(delayTimer > 0)
            {
                print("Under Knife Cooldown");
                canThrowKnife = false;
            }
        }

        else if(GameController.instance.numberOfKnives == 0) canThrowKnife = false;

        if (canThrowKnife && throwKnifeInput)
        {
            ThrowKnife();
        }
        if (!canThrowKnife && throwKnifeInput)
            
        {
            //uimanager.instance.shownoknives() or somewthing once the UI has been completed
            print("can't Throw cuz zero knives lmao");
        }

    }

    private void ThrowKnife()
    {
        //Debug.Log("Throw Knife was called, No of knives are " + GameController.instance.numberOfKnives);
        GameObject newKnife = Instantiate(throwingKnife,transform.position,Quaternion.identity) as GameObject;
        newKnife.GetComponent<Knife>().SetTarget(enemyPickerCam.GetComponent<CameraFollow>().target);
        GameController.instance.numberOfKnives--;
        throwKnifeInput = false;
        enemyPickerCam.GetComponent<CameraFollow>().ChangeTarget(newKnife.transform);
        delayTimer = initialDelayTimer;
    }

    private void LimitMovement()
    {
        canMoveFront = Mathf.Round(transform.position.z) <= 15f;
        canMoveBack = Mathf.Round(transform.position.z) > 0f;
        canMoveLeft = Mathf.Round(transform.position.x) > 1f;
        canMoveRight = Mathf.Round(transform.position.x) <= 12f;
    }

    private void HandleMovement()
    {
        if (isMoving)
        {
            return;
        }

        if (canMoveRight && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(Roll(Vector3.right));

        }
        else if (canMoveLeft && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (canMoveFront && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {

            StartCoroutine(Roll(Vector3.forward));
        }
        else if (canMoveBack && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {

            StartCoroutine(Roll(Vector3.back));
        }
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);

            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;
    }

    #region oldCode
    //// Start is called before the first frame update
    //void Start()
    //{
    //    canTakeInput = true;  
    //    anim = GetComponent<Animator>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (canTakeInput) HandleInput();
    //    HandleTweening();
    //}

    //private void HandleInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        ResetAnimTriggers();
    //        anim.SetTrigger("Forward");
    //        isMovingForward = true;
    //        canTakeInput=false;
    //        desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
    //    }
    //    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        ResetAnimTriggers();
    //        anim.SetTrigger("Back");
    //        isMovingBack = true;
    //        canTakeInput = false;
    //        desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);

    //    }
    //    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        ResetAnimTriggers();
    //        anim.SetTrigger("Left");
    //        isMovingLeft = true;
    //        canTakeInput = false;
    //        desiredPosition = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);

    //    }
    //    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        ResetAnimTriggers();
    //        anim.SetTrigger("Right");
    //        isMovingRight = true;
    //        canTakeInput = false;
    //        desiredPosition = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);

    //    }

    //}

    //private void ResetAnimTriggers()
    //{
    //    anim.ResetTrigger("Forward");
    //    anim.ResetTrigger("Back");
    //    anim.ResetTrigger("Left");
    //    anim.ResetTrigger("Right");
    //}

    //private void HandleTweening()
    //{
    //    if (isMovingForward && transform.position != desiredPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

    //        if (transform.position == desiredPosition)
    //        {
    //            isMovingForward = false;
    //            canTakeInput = true;
    //        }
    //    }

    //    if (isMovingBack && transform.position != desiredPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

    //        if (transform.position == desiredPosition)
    //        {
    //            isMovingBack = false;
    //            canTakeInput = true;
    //        }
    //    }
    //    if (isMovingLeft && transform.position != desiredPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

    //        if (transform.position == desiredPosition)
    //        {
    //            isMovingLeft = false;
    //            canTakeInput = true;
    //        }
    //    }
    //    if (isMovingRight && transform.position != desiredPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

    //        if (transform.position == desiredPosition)
    //        {
    //            isMovingRight = false;
    //            canTakeInput = true;
    //        }
    //    }
    //}

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")|| other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        UIManager.Instance.ChangeControlsView(UIManager.gameState.deadState);
        deathCam.SetActive(true);
        deathCam.GetComponent<CameraFollow>().ChangeTarget(transform);        
        GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(newDeathVFX,10f);
        destructible.BreakDeath();
        canTakeMovementInput = false;
        GameController.instance.hasDied = true;
    }
}
