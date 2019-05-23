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
    int vite;
    float dim;
    // Start is called before the first frame update
    void Start()
    {
        vite = 5;
        anim = GetComponent<Animator>();
        img = GetComponentInChildren<Image>();
        imgTransform = img.rectTransform;
        size = imgTransform.sizeDelta.x;
        dim = (size / 5);
        piume = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public IEnumerator loseHealth()
    {

        if (!piume.isPlaying)
        {
            piume.Play();
        }
        float length;
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
        length = anim.GetCurrentAnimatorClipInfo(0).Length;
        Debug.Log("length = " + length);

        yield return new WaitForSeconds(length);
        anim.SetBool("hit", false);
    }
}
