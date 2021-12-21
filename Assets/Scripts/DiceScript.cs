using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DiceState
{
    rolling,
    stopped
}
public class DiceScript : MonoBehaviour
{
    private bool keep = false;
    private bool trigger = false;
    private Vector3 prevPosition;
    private Vector3 targetPosition;
    private Quaternion prevRotation;
    private Quaternion targetRotation;

    private float timeStartedLerping;
    private float lerpTime = 0.35f;

    public Rigidbody rb;
    public Vector3 diceVelocity;
    public int diceIndex;
    public int diceNumber = 0;
    public DiceState currentDiceState = DiceState.stopped;

    public static int[] diceNumberArray = new int[5] { 0, 0, 0, 0, 0 };



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
        diceNumberArray[diceIndex] = diceNumber;

        if (currentDiceState == DiceState.stopped && trigger == true)
        {
            float currentLerpTime = Time.time - timeStartedLerping;
            float t = currentLerpTime / lerpTime;
            if (t >= 1.0f)
            {
                trigger = false;
                return;
            }
            Debug.Log(t);
            t = Mathf.Clamp(Mathf.Sin(t * Mathf.PI * 0.5f), 0f, 1f);
            transform.rotation = Quaternion.Lerp(prevRotation, targetRotation, t);
            transform.position = Vector3.Lerp(prevPosition, targetPosition, t);


        }
    }

    public void Wait()
    {
        transform.position = CupManager.instance.inCupSpawnTransforms[diceIndex].position;
        transform.rotation = Random.rotation;
    }

    public void Roll()
    {
        if (currentDiceState != DiceState.rolling)
        {
            rb.isKinematic = false;
            currentDiceState = DiceState.rolling;
            diceNumber = 0;
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = GenerateRandomPos();
            transform.rotation = Random.rotation;
            rb.AddForce(transform.up * 200);
            rb.AddTorque(dirX, dirY, dirZ);
        }
    }

    public void OnRollingFinish()
    {
        prevPosition = transform.position;
        prevRotation = transform.rotation;
        targetPosition = GameManager.posArray[diceIndex];
        targetRotation = GameManager.rotArray[diceNumber - 1];
        trigger = true;
        timeStartedLerping = Time.time;
    }


    public Vector3 GenerateRandomPos()
    {
        return new Vector3(Random.Range(-3, 3), 2, Random.Range(-3, 3));
    }


}
