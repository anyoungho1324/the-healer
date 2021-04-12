using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
// Start is called before the first frame update
{
    public enum STATE
    {
        NORMAL, SHAKE
    }
    public STATE myState = STATE.NORMAL;
    Vector3 StartPos;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public bool isShake;
    private void Awake()
    {
        isShake = false;
        StartPos = this.transform.position;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isShake)
            ChangeState(STATE.SHAKE);


        StateProcess();

    }

    void ChangeState(STATE s)
    {
        if (s == myState) return;

        myState = s;
        switch(myState)
        {
            case STATE.NORMAL:
                this.transform.position = StartPos;

                break;
            case STATE.SHAKE:
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.SHAKE:
                StartCoroutine(ShakeCamera());
                break;
        }
    }
    public IEnumerator ShakeCamera()
    {
        float timer = 0f;

        while(timer < 0.8f)
        {
            timer += Time.deltaTime;
            Vector3 randPos = (Vector3)Random.insideUnitSphere * shakeAmount;
            randPos.z = StartPos.z;
            Vector3 Pos = Vector3.Lerp(StartPos, randPos, timer);
            //this.transform.position = (Vector2)Random.insideUnitSphere * shakeAmount;

            this.transform.position = Pos;
            yield return null;


        }
        isShake = false;
        myState = STATE.NORMAL;
    }
}
