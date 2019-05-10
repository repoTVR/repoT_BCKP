using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStopPlayerMovimento : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop()
    {
        try
        {
            player.GetComponent<Movimento>().enabled = false;
        }catch(Exception e)
        {
            e.ToString();
        }
        
    }

    public void Play()
    {
        try
        {
            player.GetComponent<Movimento>().enabled = true;
        }
        catch (Exception e)
        {
            e.ToString();
        }
    }
}
