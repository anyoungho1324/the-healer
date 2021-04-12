using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnePlayerHeal : MonoBehaviour
{
    boss_script boss;
    quokka_script quokka;
    weasel_script weasel;
    cat_script cat;

    public Image DoneImage;
    Image myStartImage;
    Text mytext;


    public GameObject Particle;
    public List<Button> CharacterButtons;
    public List<Transform> positions;

    Button myButton;

    int Heal_Count = 5;
    bool IsCountZero = false;

    public enum STATE
    {
        QUOKKA,WEASEL,CAT
    }
    public STATE myState;

    private void Awake()
    {
        boss = GameObject.Find("boss-parts").GetComponent<boss_script>();
        quokka = GameObject.Find("quokka_ingame").GetComponent<quokka_script>();
        weasel = GameObject.Find("weasel_ingame").GetComponent<weasel_script>();
        cat = GameObject.Find("cat_ingame").GetComponent<cat_script>();

        myButton = GetComponent<Button>();
        myStartImage = GetComponent<Image>();
        mytext = GetComponentInChildren<Text>();
        mytext.text = Heal_Count.ToString();

        //CharacterButtons[0].interactable = false;
        foreach (Button bt in CharacterButtons)
        {
            bt.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Heal_Count <= 0)
        {
            IsCountZero = true;
            mytext.text = "";
            myButton.enabled = false;
            myStartImage.sprite = DoneImage.sprite;

        }
        else
            mytext.text = Heal_Count.ToString();

    }
    public void CountDown()
    {
        if (!IsCountZero)
        {
            Heal_Count--;

            //마을 주민 버튼생성 
            for (int i = 0; i < CharacterButtons.Count; i++)
            {
                if (boss.CharactersOnDead[i] == true)
                {
                    CharacterButtons[i].interactable = false;
                }
                else
                {
                    CharacterButtons[i].interactable = true;
                    
                }

            }

        }
    }
    public void ButtonClickQuokka()
    {
        //FindObjectOfType<AudioManager>().Play("clean");
        foreach (Button bu in CharacterButtons)
        {
            bu.interactable = false;
        }

        Particle.SetActive(true);
        Instantiate(Particle, positions[0].transform.position, Quaternion.identity);
        StartCoroutine(ParticleDestory());
        quokka.OnStern = false;
        quokka.Heal(10);


    }
    public void ButtonClickWeasel()
    {
        //FindObjectOfType<AudioManager>().Play("clean");

        foreach (Button bu in CharacterButtons)
        {
            bu.interactable = false;
        }

        Particle.SetActive(true);
        Instantiate(Particle, positions[1].transform.position, Quaternion.identity);

        StartCoroutine(ParticleDestory());

        weasel.OnStern = false;
        weasel.Heal(10);

    }
    public void ButtonClickCat()
    {
        //FindObjectOfType<AudioManager>().Play("clean");
        foreach (Button bu in CharacterButtons)
        {
            bu.interactable = false;
        }
        Particle.SetActive(true);
        Instantiate(Particle, positions[2].transform.position, Quaternion.identity);

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
