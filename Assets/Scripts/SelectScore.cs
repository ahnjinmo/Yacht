using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SelectScore : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        Text categoryText = go.transform.Find("CategoryText").GetComponent<Text>();


        int done = StrategyScript.strategies[categoryText.text]["done"];

        if (done != 1)
        {
            StrategyScript.strategies[categoryText.text]["done"] = 1;
            GameManager.instance.Wait();
        }
        // Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

    }
}
