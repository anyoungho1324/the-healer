using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
   public enum STATE
    {
        PAUSE,HOME, PLAY
    }

    public STATE myState = STATE.PLAY;

    public GameObject PauseUI;

    private void Awake()
    {
        PauseUI.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        StateProcess();

    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;

        switch(myState)
        {
            case STATE.PLAY:
                PauseUI.gameObject.SetActive(false);
                break;
            case STATE.PAUSE:
                PauseUI.gameObject.SetActive(true);

                break;
            case STATE.HOME:
                SceneManager.LoadScene("title");
                break;

        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.PLAY:
                GameDone obj;
                obj = GameObject.Find("GameDoneMain").GetComponent<GameDone>();

                if (!obj.TimeStop)
                    Time.timeScale = 1;
                else
                    Time.timeScale = 0;
                
                break;
            case STATE.PAUSE:
                Time.timeScale = 0;
                break;
            case STATE.HOME:
                break;

        }
    }
    public void AddPuaseMenu()
    {
        ChangeState(STATE.PAUSE);
    }
    public void HomeButton()
    {
        ChangeState(STATE.HOME);

    }
    public void PlayButton()
    {
        ChangeState(STATE.PLAY);

    }
}
