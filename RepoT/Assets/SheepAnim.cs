﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepAnim : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("walk", true);
        }
    }
}
