using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanse_Prefab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyEffect());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyEffect()
    {
        float delayTime = 0f;

        while(delayTime < 3f)
        {
            delayTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
