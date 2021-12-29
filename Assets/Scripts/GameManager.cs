using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public enum GameState
{
    waiting,
    ready,
    shaking,
    pouring,
    rolling,
    selecting
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Vector3[] posArray = new Vector3[5];
    public static Quaternion[] rotArray = new Quaternion[6];
    public static int turnCount = 1;
    public static bool rollTrigger = false;

    public GameState currentGameState = GameState.waiting;
    public UnityEvent onWaitStart;
    public UnityEvent onReadyStart;
    public UnityEvent onRollingWait;
    public UnityEvent onShakingStart;
    public UnityEvent onPouringStart;
    public UnityEvent onRollingStart;
    public UnityEvent onRollingFinish;

    private float posX = 1.4f;
    private float posY = 7.0f;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        posArray[0] = new Vector3(-2 * posX, posY, 0f);
        posArray[1] = new Vector3(-posX, posY, 0f);
        posArray[2] = new Vector3(0f, posY, 0f);
        posArray[3] = new Vector3(posX, posY, 0f);
        posArray[4] = new Vector3(2 * posX, posY, 0f);

        rotArray[0] = Quaternion.Euler(90f, 0f, 0f);
        rotArray[1] = Quaternion.Euler(0f, 0f, 0f);
        rotArray[2] = Quaternion.Euler(0f, 90f, 90f);
        rotArray[3] = Quaternion.Euler(0f, 0f, -90f);
        rotArray[4] = Quaternion.Euler(180f, 0f, 0f);
        rotArray[5] = Quaternion.Euler(-90f, 90f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Wait();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X) && currentGameState == GameState.waiting && CupManager.playingAnim == false)
        {
            onReadyStart.Invoke();
            SetGameState(GameState.ready);
        }

        if (Input.GetKey(KeyCode.X) && currentGameState == GameState.waiting && CupManager.playingAnim == false)
        {
            onWaitStart.Invoke();
            SetGameState(GameState.ready);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (currentGameState == GameState.ready || currentGameState == GameState.selecting) && turnCount <= 3)
        {
            IntializeDice();
            SetGameState(GameState.shaking);
            onShakingStart.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Space) && currentGameState == GameState.shaking)
        {
            SetGameState(GameState.pouring);
            onPouringStart.Invoke();
        }

        if (currentGameState == GameState.rolling && rollTrigger == true)
        {
            rollTrigger = false;
            onRollingStart.Invoke();
        }


        //if (Input.GetKeyDown(KeyCode.Space) && (currentGameState == GameState.waiting || currentGameState == GameState.selecting) && turnCount <= 3)
        //{
        //    IntializeDice();
        //    SetGameState(GameState.rolling);
        //    onRollingStart.Invoke();
        //}

        bool rollingFinished = !DiceScript.diceInfoList.Any(x => x.diceNumber == 0);

        if (currentGameState == GameState.rolling && rollingFinished)
        {
            SetGameState(GameState.selecting);
            onRollingFinish.Invoke();
            turnCount += 1;
        }
    }

    private void IntializeDice()
    {
        foreach (DiceInfo diceInfo in DiceScript.diceInfoList)
        {
            if (turnCount == 1)
            {
                diceInfo.keeping = false;
            }

            if (diceInfo.keeping == false)
            {
                diceInfo.diceNumber = 0;
            }
        }
    }


    public void SetGameState(GameState newGameState)
    {
        if (Enum.IsDefined(typeof(GameState), newGameState))
        {
            currentGameState = newGameState;
        }
    }

    public void Wait()
    {
        SetGameState(GameState.waiting);
        turnCount = 1;
        onRollingWait.Invoke();
    }
}
