using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    private AudioSource audioS;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool autoSwitch;
    [SerializeField] private float secSkip;
    private int c;

    // Start is called before the first frame update
    void Start()
    {
        c = 0;
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.clip = clips[c];
        audioS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(autoSwitch)
        {
            StartCoroutine("WaitForSkip");
        }
    }

    IEnumerator WaitForSkip()
    {
        autoSwitch = false;
        yield return new WaitForSeconds(secSkip);
        c++;

        if (c>=clips.Length)
        {
            c = 0;
        }

        audioS.clip = clips[c];
        audioS.Play();
        autoSwitch = true;
    }
}
