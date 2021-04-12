using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class weasel_script : MonoBehaviour
{
    //상태
    private float maxHealth = 100f;
    public float currentHealth;
    public bool OnDead;
    public bool OnStern;
    public bool OnFrame;
    public bool OnInvinviblity;
    bool OnInvinviblityrutin;

    public HealthBar healthBar;
    public boss_script bossScript;


    public Transform[] flypoint = new Transform[3];
    public Vector3 firstPosition = Vector3.zero;
    public float FlyRotSpeed = 50.0f;
    public float rate = 0.0f;
    public float speed = 40.0f;

    public Animator WeaselAnim;
    float weaselAttackDelay = 0f;

    //weasel button
    public Button weaselButton;
    public Button catButton;
    public Button quokkaButton;

    public ParticleSystem healeffect;

    public GameObject Arrow;
    public Transform Arrow_StartPos;
   // public Transform Arrow_EndPos;
    // Start is called before the first frame update
    private void Awake()
    {
        OnStern = false;
        OnDead = false;
        OnFrame = false;
        OnInvinviblity = false;
        OnInvinviblityrutin = true;


        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        WeaselAnim = this.GetComponent<Animator>();

        Button btn = weaselButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        weaselButton.enabled = false;
        catButton.enabled = false;
        quokkaButton.enabled = false;
    }
    void Start()
    {
        Debug.Log("weasel MaxHealth = " + maxHealth);

    }
    private void FixedUpdate()
    {

        if (OnDead)
        {
            rate += Time.fixedDeltaTime * 0.5f;
            if (rate >= 1.2)
            {
                bossScript.maeulJuinDead_Count++;

                Destroy(gameObject);
                Debug.Log("weasel Destroy");

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (OnInvinviblity && OnInvinviblityrutin)
        {
            StartCoroutine(OnInvincibilityCor());
        }


        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= Mathf.Epsilon)
        {
            OnDead = true;
            WeaselOnDead();
        }



        weaselAttackDelay += Time.deltaTime;
        if(weaselAttackDelay > 1.2f)
        {
            if (!OnStern)
                WeaselAnim.SetTrigger("Attack_2");
            weaselAttackDelay = 0;
        }


    }
    IEnumerator OnInvincibilityCor()
    {
        OnInvinviblityrutin = false;

        float delaytime = 0;

        while (delaytime < 3f)
        {
            delaytime += Time.deltaTime;
            //Debug.Log(delaytime);
            yield return null;
        }
        Debug.Log("OnInvincibilityCor = off");
        OnInvinviblityrutin = true;
        OnInvinviblity = false;
    }
    public void TakeDamage(float damage)
    {
        if(!OnDead && !OnInvinviblity)
        {
            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);

        }
      
    }

    public void Heal(float heal)
    {
        if (!OnDead)
        {
            StartCoroutine(HealDelayTime(heal));

        }
    }

    IEnumerator HealDelayTime(float heal)
    {
        float StackHeal = 0;

        while (StackHeal < heal)
        {

            currentHealth += 1f;
            StackHeal += 1f;
            healthBar.SetHealth(currentHealth);

            yield return null;

        }
       // Debug.Log("StackHeal : " + StackHeal);
    }

    void attack()
    { 
        if (!OnDead && !OnStern)
        {
            if (!bossScript.BossOnDead)
            {
                bossScript.TakeDamage(10f);
            }
            //Debug.Log("Instantiate");
           Instantiate(Arrow, Arrow_StartPos.position, Arrow_StartPos.rotation);
            FindObjectOfType<AudioManager>().Play("archer");
        }
    }

    void WeaselOnDead()
    {
        List<Vector3> FlyPoint_list = new List<Vector3>();

        FlyPoint_list.Add(firstPosition);
        foreach (var i in flypoint)
        {
            FlyPoint_list.Add(i.position);
        }

        while (FlyPoint_list.Count > 1)
        {
            List<Vector3> PointResult = new List<Vector3>();

            for (int i = 0; i < FlyPoint_list.Count - 1; i++)
            {
                Vector3 result = Vector3.Lerp(FlyPoint_list[i], FlyPoint_list[i + 1], rate);
                PointResult.Add(result);
            }
            FlyPoint_list = PointResult;

        }
        //this.transform.position = FlyPoint_list[0];
        //this.transform.Rotate(-Vector3.forward * FlyRotSpeed);
        this.transform.parent.position = FlyPoint_list[0];
        this.transform.parent.Rotate(-Vector3.forward * FlyRotSpeed);

        weaselButton.enabled = false;

    }
    void TaskOnClick()
    {
        if (!OnDead)
        {
            Heal(10);
            healeffect.Play();
            FindObjectOfType<AudioManager>().Play("heal");
        }
        weaselButton.enabled = false;
        catButton.enabled = false;
        quokkaButton.enabled = false;

    }
}
