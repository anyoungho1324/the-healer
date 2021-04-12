using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Vector3 dir = Vector3.up;
    float ArrowMoveSpeed = 4f;
   // float dist;
    public Vector3 Target;

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * ArrowMoveSpeed;

        
        this.transform.Translate(dir * delta);


    }

}
