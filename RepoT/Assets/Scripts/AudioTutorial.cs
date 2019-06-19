using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTutorial : MonoBehaviour
{

    public AudioClip[] audioClipArray;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioPlay(int id)
    {
        audioSource.clip = audioClipArray[id];
        audioSource.Play();
    }
}
