using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealthBar : MonoBehaviour
{
    public Image myHeatlthBarImage;
    public Image RedBarImage;
    public Image GreenBarImage;

    public boss_script boss;

    // Start is called before the first frame update
    void Start()
    {
        myHeatlthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.Burn_TakeBurnDotDeal)
        {

            ChangeRedHealthBar();

        }
        else
        {
           // Debug.Log(boss.Burn_TakeBurnDotDeal);

            ReturnHealthBar();

        }
    }

    void ChangeRedHealthBar()
    {
        myHeatlthBarImage.sprite = RedBarImage.sprite;

    }
    void ReturnHealthBar()
    {
        myHeatlthBarImage.sprite = GreenBarImage.sprite;
    }
}
