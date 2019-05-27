using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdPanelCentrale : MonoBehaviour
{

    [SerializeField] private int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPanelCentrale(int id)
    {
        return id == this.id;
    }

    
}
