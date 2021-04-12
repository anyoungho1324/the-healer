using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_script : MonoBehaviour
{ 

    public ParticleSystem healeffect_quokka;
    public ParticleSystem healeffect_cat;
    public ParticleSystem healeffect_weasel;

    public ParticleSystem healeffect_staff;

    public cat_script catScript;
    public weasel_script weaselScript;
    public quokka_script quokkaScript;
    // Start is called before the first frame update


    void Start()
    {

        healeffect_quokka.Stop();
        healeffect_cat.Stop();
        healeffect_weasel.Stop();
        healeffect_staff.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void heal_start()
    {
        if (!quokkaScript.OnDead)
        {
            healeffect_quokka.Play();
        }
        if (!catScript.OnDead)
        {
            healeffect_cat.Play();
        }
        if (!weaselScript.OnDead)
        {
            healeffect_weasel.Play();
        }

    }
    void heal_stop()
    {

        healeffect_quokka.Stop();
        healeffect_cat.Stop();
        healeffect_weasel.Stop();
    }
    void heal_effect_on()
    {
        healeffect_staff.Play();
        
    }
    void heal_effect_off()
    {
        healeffect_staff.Stop();
    }
}
