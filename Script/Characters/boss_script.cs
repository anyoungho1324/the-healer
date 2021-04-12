using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class boss_script : MonoBehaviour
{

    public float maxHealth = 1000f;
    public float currentHealth;
    public bool BossOnDead;

    public int maeulJuinDead_Count;

    public Animator animator;
    public ParticleSystem firebreath;
    public Animator earthquakeAnim;

    public HealthBar healthBar;
    public cat_script catScript;
    public weasel_script weaselScript;
    public quokka_script quokkaScript;

    //SlashAttack
    private bool OnSlahAttack;
    public float slashdeal = 20.0f;
    public GameObject[] DamageEffets = new GameObject[3];
    public List<bool> CharactersOnDead;
   // float EffectDelay;
    float SlashAttackDelay = 0;
    float SlashEffect_DelTime = 0f;

    //BressAttack
    public GameObject[] FrameEffects = new GameObject[3];
    List<bool> CharactersOnFrame;
    private float Burn_DotDeal = 0.15f;
    public bool Burn_TakeBurnDotDeal = false;
    float elapsed = 0f;
    private float burndeal = 0.5f;
    bool burn = false;
    float burn_accum_damage = 0.0f;
    float burnDot_accum_damage = 0.0f;

    //SternAttack
    public MoveBackground myCameraMove;
    public GameObject[] SternEffects = new GameObject[3];
    List<bool> CharactersOnStern;
    public bool OnSternAttack;
    float SternAttackDelay = 0.0f;
    public float sternTime = 0.0f;
    public float SternDelayTime = 2.0f;
    float SternEffectRotSpeed = 50.0f;
    public float SternDeal = 10.0f;
    bool NoneTakeDamage; //주민들이 스턴이면 true
    public Animator myAnim;

    public enum STATE
    {
        NORMAL,SLASHATTACK,BRESSATTACK,STERNATTACK
    }
    public STATE myState = STATE.NORMAL;
  
    // Start is called before the first frame update
    private void Awake()
    {
        myAnim.GetComponent<Animator>();
        myCameraMove.GetComponent<MoveBackground>();
        NoneTakeDamage = false;
        OnSlahAttack = false;
        OnSternAttack = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        firebreath.Stop();
        BossOnDead = false;
        maeulJuinDead_Count = 0;



        foreach (GameObject go in DamageEffets)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in SternEffects)
        {
            go.SetActive(false);
        }
        foreach(GameObject go in FrameEffects)
        {
            go.SetActive(false);
        }


    }
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        Burn();
        BossAttack_BurnDotDeal();

        if (OnSlahAttack)
            SlashAttack();

        if (OnSternAttack)
            SternAttack();

        StateProcess();
    }
    void StateChange(STATE s)
    {
        if(myState == s) return;

        myState = s;

        switch(myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.SLASHATTACK:
                break;
            case STATE.BRESSATTACK:
                break;
            case STATE.STERNATTACK:
                break;

        }
    }
    void StateProcess()
    {
        switch(myState)
        {
            case STATE.NORMAL:
                myAnim.SetTrigger("Basic");
                
                break;
            case STATE.SLASHATTACK:
                myAnim.SetTrigger("slashattack");
                StateChange(STATE.NORMAL);
                break;
            case STATE.BRESSATTACK:
                myAnim.SetTrigger("breath");
                StateChange(STATE.NORMAL);

                break;
            case STATE.STERNATTACK:
                myAnim.SetTrigger("stern");
                StateChange(STATE.NORMAL);

                break;
        }
    }
    public void TakeDamage(float damage)
    {
        if(!NoneTakeDamage)
        {
            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);
            if(currentHealth < Mathf.Epsilon)
            {
                BossOnDead = true;
            }
        }
    }

    void Slashattack_Check()
    {
        OnSlahAttack = true;
        OnJuminDamage(slashdeal);
        Debug.Log("Slash : " + slashdeal);
        FindObjectOfType<AudioManager>().Play("scratch");
    }
    void SlashAttack()
    {
        Mauljumin_StateCheck();
        
        int Ef_count = 0;

        SlashAttackDelay += Time.deltaTime;
        if(SlashAttackDelay >= 0.1)
        {
            foreach (bool character in CharactersOnDead)
            {
                if (!character)
                {
                    DamageEffets[Ef_count].SetActive(true);                   
                    Ef_count++;
                }
                else
                    Ef_count++;

                Ef_count = Ef_count >= 3 ? 0 : Ef_count;
            }
            
        }


        SlashEffect_DelTime += Time.deltaTime;

        if (SlashEffect_DelTime >= 0.5f)
        {
            foreach (GameObject go in DamageEffets)
            {
                go.SetActive(false);
            }
            SlashAttackDelay = 0.0f;
            SlashEffect_DelTime = 0f;
            OnSlahAttack = false;
            //EffectDelay = 0f;
        }
    }

    void Breath()
    {
        burn = true;
        firebreath.Play();
        FindObjectOfType<AudioManager>().Play("breath");
    }
    void Breathstop()
    {
        burn = false;
        Burn_TakeBurnDotDeal = true;

        firebreath.Stop();

        //check
        {
            Debug.Log("burn_accum_damage : " + burn_accum_damage);
            burn_accum_damage = 0;
        }

    }
    void Burn()
    {
        if (burn)
        {
            OnJuminDamage(burndeal);
            // check
            {
                burn_accum_damage += burndeal;
            }
        }
    }
    void BossAttack_BurnDotDeal()
    {
        BurnDotDealFrame_Effect();
        if (Burn_TakeBurnDotDeal)
        {
            
            OnJuminDamage(Burn_DotDeal);

            elapsed += Time.deltaTime;
            burnDot_accum_damage += Burn_DotDeal;

            if (3f <= elapsed)
            {
                Burn_TakeBurnDotDeal = false;

                elapsed = 0f;
                Debug.Log("BrunDotDeal : " + burnDot_accum_damage);
            }
        }
    }
    void BurnDotDealFrame_Effect()
    {
        Mauljumin_StateCheck();

        int Ef_count = 0;

        
        if (Burn_TakeBurnDotDeal)
        {
            foreach (bool character in CharactersOnDead)
            {
                if (!character)
                {
                    FrameEffects[Ef_count].SetActive(true);
                    Ef_count++;
                }
                else
                    Ef_count++;

                Ef_count = Ef_count >= 3 ? 0 : Ef_count;
            }

        }

        else 
        {
            foreach (GameObject go in FrameEffects)
            {
                go.SetActive(false);
            }

        }

    }

    void SternAttack_Check()
    {
        earthquakeAnim.SetTrigger("PlayStart");


    }
    void SternAttack_Check_2()
    {
        OnSternAttack = true;
        SetJuminStern(true);
        myCameraMove.isShake = true;
        OnJuminDamage(SternDeal);
        Debug.Log("SternDeal : " + SternDeal);
        FindObjectOfType<AudioManager>().Play("stomp");

    }
    void SternAttack()
    {


        SternAttackDelay += Time.deltaTime;

        if(SternAttackDelay >= 0.1f)
        {
            for (int i = 0; i < SternEffects.Length; i++)
            {
                Mauljumin_StateCheck();

                if (CharactersOnDead[i])
                {
                    SternEffects[i].SetActive(false);

                }
                else
                    SternEffects[i].SetActive(true);

                if (CharactersOnStern[i])
                {
                    SternEffects[i].transform.Rotate(Vector3.forward * SternEffectRotSpeed);
                }
                else if(!CharactersOnStern[i])
                {
                    SternEffects[i].SetActive(false);
                    CharactersOnStern[i] = false;
                }    
                    

            }
            
        }

        sternTime += Time.deltaTime;

        if (sternTime > SternDelayTime)
        {
            foreach (GameObject go in SternEffects)
            {
                go.SetActive(false);
            }
            SternAttackDelay = 0.0f;
            sternTime = 0.0f;

            OnSternAttack = false;
            SetJuminStern(false);
        }

    }

    public void Mauljumin_StateCheck()
    {
        CharactersOnDead = new List<bool> { quokkaScript.OnDead, weaselScript.OnDead, catScript.OnDead };
        CharactersOnStern = new List<bool> { quokkaScript.OnStern, weaselScript.OnStern, catScript.OnStern };
        CharactersOnFrame = new List<bool> { quokkaScript.OnFrame, weaselScript.OnFrame, catScript.OnFrame };
    }

    void OnJuminDamage(float Damage)
    {
        quokkaScript.TakeDamage(Damage);
        weaselScript.TakeDamage(Damage);
        catScript.TakeDamage(Damage);
    }
    void SetJuminStern(bool result)
    {
        quokkaScript.OnStern = result;
        weaselScript.OnStern = result;
        catScript.OnStern = result;
        NoneTakeDamage = result;
    }
    


}

