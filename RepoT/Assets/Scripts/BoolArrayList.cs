using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolArrayList : MonoBehaviour
{
    private static ArrayList boolArrayList;



    // Start is called before the first frame update
    void Start()
    {
        boolArrayList = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ArrayList GetBoolArrayList()
    {
        return boolArrayList;
    }

    public void SetBoolArrayList(ArrayList list)
    {
        boolArrayList = list;
    }
}
