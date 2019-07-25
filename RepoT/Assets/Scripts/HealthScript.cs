using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    ParticleSystem piume;
    [HideInInspector] public Animator anim;
    public Image img;
    RectTransform imgTransform;
    float size;
    public int vite;
    float dim;
    public bool tutorial;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        img = GetComponentInChildren<Image>();
        imgTransform = img.rectTransform;
        size = imgTransform.sizeDelta.x;
        dim = (size);
        imgTransform.sizeDelta = new Vector2(size * vite, imgTransform.sizeDelta.y);
        piume = GetComponentInChildren<ParticleSystem>();
    }

    
    public IEnumerator loseHealth()
    {
        float length;

        if (!piume.isPlaying)
        {
            piume.Play();
        }

        //La gallina del tutorial non perde vite
        if (!tutorial)
        {
            imgTransform.sizeDelta = new Vector2(imgTransform.sizeDelta.x - dim, imgTransform.sizeDelta.y);
            if (vite > 1)
            {
                anim.SetBool("hit", true);
            }
            else
            {
                anim.SetTrigger("dead");
                Destroy(gameObject, 0.5f);
            }

            vite--;
        }

        //Altrimenti fai partire comunque l'animazione
        else
        {
            anim.SetBool("hit", true);
        }


        length = anim.GetCurrentAnimatorClipInfo(0).Length;

        yield return new WaitForSeconds(length);
        anim.SetBool("hit", false);
    }
}
