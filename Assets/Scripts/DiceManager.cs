using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class DiceManager : MonoBehaviour
{
    public GameObject[] dices;
    public UnityEvent onRollingFinish;

    // Start is called before the first frame update
    void Awake()
    {
        dices = GameObject.FindGameObjectsWithTag("Dice");
        int diceIndex = 0;
        foreach (var dice in dices)
        {
            dice.GetComponent<DiceScript>().diceIndex = diceIndex;
            diceIndex += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRollingWait()
    {
        foreach (var dice in dices)
        {
            dice.GetComponent<DiceScript>().Wait();
        }
    }


    public void OnRollingStart()
    {
        foreach (var dice in dices)
        {
            dice.GetComponent<DiceScript>().Roll();
        }
    }
    

    public void OnRollingFinish()
    {
        foreach (var dice in dices)
        {
            dice.GetComponent<DiceScript>().OnRollingFinish();
        }
    }
}
