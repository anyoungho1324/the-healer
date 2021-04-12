using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cleanse : MonoBehaviour
{
    public boss_script boss;

    public quokka_script quokka;
    public weasel_script weasel;
    public cat_script cat;
    Image myStartImage;
    public Image DoneImage;
    public Text mytext;


    public Button myButton;
    public Button catButton;
    public Button quokkaButton;
    public Button weaselButton;

    public GameObject Particle;
    List<Button> Buttons;
    public List<Transform> positions;
    int Cleanse_Count = 5;
    bool IsCountZero = false;
    private void Awake()
    {
        myStartImage = GetComponent<Image>();
        mytext.GetComponent<Text>();
        myButton = GetComponent<Button>();
        mytext.text = Cleanse_Count.ToString();
        Buttons = new List<Button> { quokkaButton,weaselButton, catButton };

        foreach (Button bu in Buttons)
        {
            bu.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Cleanse_Count <= 0)
        {
            IsCountZero = true;
            mytext.text = "";
            myButton.enabled = false;
            myStartImage.sprite = DoneImage.sprite;
        }
        else
            mytext.text = Cleanse_Count.ToString();


    }
    public void CountDown()
    {
        if (!IsCountZero)
        {
            Cleanse_Count--;

            //마을 주민 버튼생성 
            for(int i=0;i<Buttons.Count;i++)
            {
                if (boss.CharactersOnDead[i] == true)
                {
                    Buttons[i].interactable = false;
                }
                else
                    Buttons[i].interactable = true;

            }
  
        }
    }
    public void ButtonClickQuokka()
    {
        FindObjectOfType<AudioManager>().Play("clean");
        foreach (Button bu in Buttons)
        {
            bu.interactable = false;
        }

        Particle.SetActive(true);
        Instantiate(Particle, positions[0].transform.position, Quaternion.identity);
        StopAllCoroutines();
        StartCoroutine(ParticleDestory());
        quokka.OnStern = false;
        quokka.Heal(10);


    }
    public void ButtonClickWeasel()
    {
        FindObjectOfType<AudioManager>().Play("clean");

        foreach (Button bu in Buttons)
        {
            bu.interactable = false;
        }

        Particle.SetActive(true);
        Instantiate(Particle, positions[1].transform.position, Quaternion.identity);
        StopAllCoroutines();

        StartCoroutine(ParticleDestory());

        weasel.OnStern = false;
        weasel.Heal(10);
        
    }
    public void ButtonClickCat()
    {
        FindObjectOfType<AudioManager>().Play("clean");
        foreach (Button bu in Buttons)
        {
            bu.interactable = false;
        }
        Particle.SetActive(true);
        Instantiate(Particle, positions[2].transform.position,Quaternion.identity);
        StopAllCoroutines();

        StartCoroutine(ParticleDestory());
        cat.OnStern = false;
        cat.Heal(10);
    }
    IEnumerator ParticleDestory()
    {
        float DelayTime = 0f;

        while (DelayTime < 1.5f)
        {
            DelayTime += Time.deltaTime;


            yield return null;
        }

        Particle.SetActive(false);
    }
}
