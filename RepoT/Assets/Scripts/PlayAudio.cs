using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public bool play;
    AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            PlayAudioClip();
            play = false;
        }
    }

    public void PlayAudioClip()
    {
        clip.Play();
    }
}
