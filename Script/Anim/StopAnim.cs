using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnim : MonoBehaviour
{

    public Animator CharacterAnim;

    // Start is called before the first frame update
    private void Awake()
    {
        CharacterAnim = this.GetComponent<Animator>();
    }

    public void AnimStop()
    {
        CharacterAnim.SetBool("attack", false);
    }
    public void AnimStart()
    {
        CharacterAnim.SetBool("attack", true);
    }
}
