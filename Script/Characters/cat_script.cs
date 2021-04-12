using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class cat_script : MonoBehaviour
{
    private int maxHealth = 100;
    private float currentHealth;
    public bool OnDead;
    public bool OnStern;
    public bool OnFrame;
    public bool OnInvinviblity;
    bool OnInvinviblityrutin;
    // StopAnim stopanim;
    public HealthBar healthBar;
    public boss_script bossScript;

    public Transform[] flypoint = new Transform[3];
    public Vector3 firstPosition = Vector3.zero;
    public float FlyRotSpeed = 50.0f;
    public float rate = 0.0f;

    public Animator CatAnim;
    float CatAttackDelay = 0.0f;

    //cat button
    public Button weaselButton;
    public Button catButton;
    public Button quokkaButton;
    Button btn;
    public ParticleSystem healeffect;

    // Start is called before the first frame update
    private void Awake()
    {
        OnDead = false;
        OnStern = false;
        OnFrame = false;
        OnInvinviblity = false;
        OnInvinviblityrutin = true;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        firstPosition = this.transform.position;
        CatAnim = GetComponent<Animator>();

        btn = catButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        weaselButton.enabled = false;
        catButton.enabled = false;
        quokkaButton.enabled = false;
    }
    void Start()
    {
        Debug.Log("cat MaxHealth = " + maxHealth);

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
                Debug.Log("cat Destroy");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(OnInvinviblity && OnInvinviblityrutin)
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
            catOnDead();
        }


        CatAttackDelay += Time.deltaTime;
        if (CatAttackDelay > 1.2f)
        {
            if (!OnStern)
                CatAnim.SetTrigger("Attack_2");
            CatAttackDelay = 0;
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
        if (!OnDead && !OnInvinviblity)
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
                bossScript.TakeDamage(10);
            }
        }

        
    }

    void catOnDead()
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
        this.transform.parent.position = FlyPoint_list[0];
        this.transform.parent.Rotate(-Vector3.forward * FlyRotSpeed);

        catButton.enabled = false;
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
