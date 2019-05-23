﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    public bool isAttacking;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                other.GetComponent<HealthScript>().StartCoroutine("loseHealth");
            }
        }
    }
}
