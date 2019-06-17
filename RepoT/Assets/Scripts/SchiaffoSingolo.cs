using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchiaffoSingolo : MonoBehaviour
{
    public bool play;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        play = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            StartCoroutine("Attacca");
            play = false;
        }
    }

    IEnumerator Attacca()
    {
        for(int i =0; i < 200; i++)
        {
            float length;
            yield return new WaitForSeconds(0.25f);
            gameObject.GetComponentInChildren<HandAtacck>().isAttacking = true;
            anim.SetBool("attack", true);
            length = anim.GetCurrentAnimatorClipInfo(0).Length;
            //Debug.Log("length = " + length);
            yield return new WaitForSeconds(length);
            //arma.GetComponent<AxeScript>().isAttacking = false;
            gameObject.GetComponentInChildren<HandAtacck>().isAttacking = false;
            anim.SetBool("attack", false);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
