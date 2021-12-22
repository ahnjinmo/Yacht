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
        StrategyScript.strategies[categoryText.text]["done"] = 1;

        Text scoreText = go.transform.parent.Find("ScoreText").GetComponent<Text>();
        scoreText.color = Color.black;
    }
}
