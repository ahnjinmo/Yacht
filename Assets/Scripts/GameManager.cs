using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public enum GameState
{
    waiting,
    rolling,
    sorting,
    selecting
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Vector3[] posArray = new Vector3[5];
    public static Quaternion[] rotArray = new Quaternion[6];

    public GameState currentGameState = GameState.waiting;
    public UnityEvent onRollingWait;
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
        SetGameState(GameState.waiting);
        onRollingWait.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (currentGameState == GameState.waiting || currentGameState == GameState.selecting))
        {
            IntializeDice();
            SetGameState(GameState.rolling);
            onRollingStart.Invoke();
        }

        bool rollingFinished = !DiceScript.diceInfoList.Any(x => x.diceNumber == 0);

        if (currentGameState == GameState.rolling && rollingFinished)
        {
            SetGameState(GameState.selecting);
            onRollingFinish.Invoke();
        }
    }

    private void IntializeDice()
    {
        foreach (DiceInfo diceInfo in DiceScript.diceInfoList)
        {
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

    public void StartGame()
    {

    }
}
