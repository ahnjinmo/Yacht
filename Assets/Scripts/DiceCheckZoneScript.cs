using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    Vector3 diceVelocity;
    DiceScript diceScript;

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Side")
        {
            diceScript = col.transform.parent.gameObject.GetComponent<DiceScript>();
            diceVelocity = diceScript.diceVelocity;

            if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
            {
                switch (col.gameObject.name)
                {
                    case "Side1":
                        diceScript.diceNumber = 6;
                        break;
                    case "Side2":
                        diceScript.diceNumber = 5;
                        break;
                    case "Side3":
                        diceScript.diceNumber = 4;
                        break;
                    case "Side4":
                        diceScript.diceNumber = 3;
                        break;
                    case "Side5":
                        diceScript.diceNumber = 2;
                        break;
                    case "Side6":
                        diceScript.diceNumber = 1;
                        break;
                }

                diceScript.currentDiceState = DiceState.stopped;
                diceScript.rb.isKinematic = true;
            }
        }

    }
}
