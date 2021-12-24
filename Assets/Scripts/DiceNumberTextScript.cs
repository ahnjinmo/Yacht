using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnWaiting()
    {
        text.text = "roll the dices";
    }

    public void OnRolling()
    {
        text.text = "rolling dice...";
    }

    public void OnSelecting()
    {
        var a = DiceScript.diceInfoList.OrderBy(x => x.diceNumber).Select(x => x.diceNumber).ToArray();
        string result = "[" + string.Join(",", a) + "]";

        text.text = result;
    }
}
