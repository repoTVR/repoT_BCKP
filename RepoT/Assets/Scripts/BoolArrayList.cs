﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolArrayList : MonoBehaviour
{
    private static List<bool> boolArrayList;
    public ArrayList list;



    // Start is called before the first frame update
    void Start()
    {
        boolArrayList = new List<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<bool> GetBoolArrayList()
    {
        return boolArrayList;
    }

    public void SetBoolArrayList(List<bool> list)
    {
        boolArrayList = list;
    }
}
