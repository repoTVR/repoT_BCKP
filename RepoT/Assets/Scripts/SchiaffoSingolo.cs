using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SchiaffoSingolo : MonoBehaviour
{
    public TextMeshProUGUI textNumAttacco;
    public int forTimes;
    public bool play = true;
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
        if(textNumAttacco != null)
        {
            forTimes = 5;
        }
        else
        {
            forTimes = 200;
        }

        for(int i = 0; i < forTimes; i++)
        {
            if(textNumAttacco != null)
            {
                textNumAttacco.SetText((i + 1) + "");
                textNumAttacco.fontSize = (i+1)*10;
            }
            //Durata dell'animazione
            float length;

            //Attesa 0.25f per problema atterraggio character
            yield return new WaitForSeconds(0.25f);
            gameObject.GetComponentInChildren<HandAtacck>().isAttacking = true;
            anim.SetBool("attack", true);

            //Durata clip in play
            length = anim.GetCurrentAnimatorClipInfo(0).Length;

            //Aspetta per la durata dell'attacco
            yield return new WaitForSeconds(length);

            gameObject.GetComponentInChildren<HandAtacck>().isAttacking = false;
            anim.SetBool("attack", false);

            yield return new WaitForSeconds(0.25f);
        }

    }


    //Quando l'oggetto viene disabilitato stoppo la coroutine
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //Quando l'oggetto viene riabilitato do il play
    private void OnEnable()
    {
        play = true;
    }
}
