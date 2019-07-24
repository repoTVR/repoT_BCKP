using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHitHand : MonoBehaviour
{

    public AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip RandomizeAudio()
    {
        return audios[(int)Random.Range(0, 2)];
    }
}
