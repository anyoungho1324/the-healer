using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class heal1pscript : MonoBehaviour
{
    public float heal = 10.0f;
    public Button yourButton;

    public Button catheal;
    public Button quokkaheal;
    public Button weaselheal;

    public int healcount;

    public Image myStartImage;
    public Image DoneHealImage;
    public Text mytext;
    bool isCountZero;

    public cat_script catScript;
    public weasel_script weaselScript;
    public quokka_script quokkaScript;

    private void Awake()
    {
        mytext.GetComponent<Text>();
        myStartImage.GetComponent<Image>();

        isCountZero = false;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        catheal.enabled = false;
        quokkaheal.enabled = false;
        weaselheal.enabled = false;
        Debug.Log("비활성화");
    }
    void Start()
    {

    }
    private void Update()
    {
        if (healcount == 0)
        {
            myStartImage.sprite = DoneHealImage.sprite;

            isCountZero = true;
            Destroy(mytext);
            yourButton.enabled = false;
        }
    }

    void TaskOnClick()
    {
        Debug.Log("활성화");
        if (healcount > 0)
        {
            healcount -= 1;
            if (!catScript.OnDead)
            {
                catheal.enabled = true;
            }
            if (!weaselScript.OnDead)
            {
                weaselheal.enabled = true;
            }
            if (!quokkaScript.OnDead)
            {
                quokkaheal.enabled = true;
            }

        }


        if (!isCountZero)
        {
            Text txt = transform.Find("Text").GetComponent<Text>();
            txt.text = healcount.ToString();

        }
    }

}