using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround_Slider : MonoBehaviour
{
    public Slider mySlider;
    AudioSource myAudio;

    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        mySlider.value = myAudio.volume;

    }

    // Update is called once per frame
    void Update()
    {
        myAudio.volume = mySlider.value;
    }
}
