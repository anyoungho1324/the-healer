using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Invincibility : MonoBehaviour
{
    public boss_script boss;
    public quokka_script quokka;
    public weasel_script weasel;
    public cat_script cat;

    public Image myStartImage;
    public Image DoneImage;
    public Text mytext;

    public Button myButton;
    bool IsCountZero = false;
    int Invincibility_Count = 5;

    public GameObject Particles;
    public List<Transform> positions;
    private void Awake()
    {

        myStartImage.GetComponent<Image>();
        mytext.GetComponent<Text>();
        myButton.GetComponent<Button>();
        mytext.text = Invincibility_Count.ToString();
        //for (int i = 0; i < positions.Count; i++)
        //{
        //    Instantiate(Particles, positions[i].transform.position, Quaternion.identity);
        //}
    }
    // Update is called once per frame
    void Update()
    {
         
        if (Invincibility_Count <= 0)
        {
            IsCountZero = true;
            mytext.text = "";
            myButton.enabled = false;
            myStartImage.sprite = DoneImage.sprite;
        }
        else
            mytext.text = Invincibility_Count.ToString();


    }
    public void CountUP()
    {
        if(!IsCountZero)
        {
            boss.Mauljumin_StateCheck();
            for(int i = 0;i<positions.Count;i++)
            {
                if(!boss.CharactersOnDead[i] == true)
                {
                    Instantiate(Particles, positions[i].transform.position, Quaternion.identity);

                }
            }
            Invincibility_Count--;
            cat.OnInvinviblity = true;
            quokka.OnInvinviblity = true;
            weasel.OnInvinviblity = true;

        }
    }
}
