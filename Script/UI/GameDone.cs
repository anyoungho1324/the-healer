using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDone : MonoBehaviour
{
    boss_script boss;
    Text myText;
    public GameObject Done;
    Image myImage;
    public enum STATE
    {
       NORMAL, BOSSDEAD,MAEULMUJINDEAD
    }
    public bool TimeStop = false;
    public STATE myState = STATE.NORMAL;

    private void Awake()
    {
        boss = GameObject.Find("boss-parts").GetComponent<boss_script>();

        myText = GetComponentInChildren<Text>();
        myImage = Done.GetComponent<Image>();
        Done.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (boss.BossOnDead)
        {
            TimeStop = true;
            ChangeState(STATE.BOSSDEAD);
        }
       // Debug.Log(boss.maeulJuinDead_Count);
        if(boss.maeulJuinDead_Count == 3)
        {

            ChangeState(STATE.MAEULMUJINDEAD);
        }
        StateProcess();
        // 보스가 죽을 시
        
    }
    public void ChangeState(STATE s)
    {
        if (s == myState) return;

        myState = s;

        switch(myState)
        {
            case STATE.NORMAL:
               // Done.gameObject.SetActive(false);

                break;
            case STATE.BOSSDEAD:
                Debug.Log("BOSSDEAD");

                Done.gameObject.SetActive(true);

                break;
            case STATE.MAEULMUJINDEAD:
                Debug.Log("MAEULMUJINDEAD");
                StartCoroutine(ChangeAlpha());


                Done.gameObject.SetActive(true);

                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.BOSSDEAD:
                Time.timeScale = 0;

                myText.text = "WIN!";
                break;
            case STATE.MAEULMUJINDEAD:
                myText.text = "";
                break;

        }
    }

    IEnumerator ChangeAlpha()
    {
        Color col = myImage.color;
        col = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0f);

        while(myImage.color.a < 1.0f)
        {
            col = new Color(myImage.color.r,myImage.color.g, myImage.color.b, myImage.color.a + Time.deltaTime * 0.15f);
            myImage.color = col;
            yield return null;
        }
        
    }
    public void HomeButton()
    {
        SceneManager.LoadScene("title");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
