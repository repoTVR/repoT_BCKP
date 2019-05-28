using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaTag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("PanelSpeciali"))
        {
            Debug.Log("....");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
