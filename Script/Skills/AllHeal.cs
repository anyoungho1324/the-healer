using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AllHeal : MonoBehaviour
{
    public float heal = 11.0f;

    public Button yourButton;

    public cat_script catScript;
    public weasel_script weaselScript;
    public quokka_script quokkaScript;
    
    public int healcount;

    public Image myStartImage;
    public Image DoneHealImage;
    public Text mytext;
    bool isCountZero;

    /*public ParticleSystem healeffectcat;
    public ParticleSystem healeffectquokka;
    public ParticleSystem healeffectweasel;*/

    //public Sprite healdepleted;

    /*void SetText(string text)
    {
        Text txt = transform.Find("Text").getComponent<text>();
        txt.text = text;
    }*/

    private void Awake()
    {
        mytext.GetComponent<Text>();
        myStartImage.GetComponent<Image>();
        
        isCountZero = false;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
    }
    void Start()
    {
        /*healeffectquokka.Stop();
        healeffectcat.Stop();
        healeffectweasel.Stop();*/
        /*Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = healcount.ToString();*/
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
        /*GameObject thecat = GameObject.Find("cat");
        cat_script catScript = thecat.GetComponent<cat_script>();*/
        
        if (healcount > 0)
        {
            FindObjectOfType<AudioManager>().Play("heal");
            healcount -= 1;
            catScript.Heal(heal);
            weaselScript.Heal(heal);
            quokkaScript.Heal(heal);
            /*healeffectquokka.Play();
            healeffectcat.Play();
            healeffectweasel.Play();*/


        }/*else
        {
            GetComponent<Button>().GetComponent<Image>().sprite = healdepleted;
        }*/
        if(!isCountZero)
        {
            Text txt = transform.Find("Text").GetComponent<Text>();
            txt.text = healcount.ToString();

        }
    }

}
