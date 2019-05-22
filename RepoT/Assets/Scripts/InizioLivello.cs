using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InizioLivello : MonoBehaviour
{

    Transform transformIniziale;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transformIniziale = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
