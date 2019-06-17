using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageRudy : MonoBehaviour
{
    ParticleSystem piume;
    [HideInInspector] public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        piume = GetComponentInChildren<ParticleSystem>();

    }

    public IEnumerator TakeDamage()
    {
        anim.SetBool("hit", true);
        if (!piume.isPlaying)
        {
            piume.Play();
        }
        float length = anim.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(length);
        anim.SetBool("hit", false);
    }
}
