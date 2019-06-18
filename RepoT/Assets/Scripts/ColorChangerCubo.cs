using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerCubo : MonoBehaviour
{
    //private Color[] colorArray;
    //Renderer rend;

    public int flagColore;

    // Start is called before the first frame update
    void Start()
    {
        //Inizializzazione array di 4 colori
        //colorArray = new Color[4] { Color.green, Color.red, Color.yellow, Color.cyan };
        //rend = GetComponent<Renderer>();
        //StartCoroutine("ColorChange");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getColore()
    {
        return flagColore;
    }


    //private IEnumerator ColorChange()
    //{
    //    int iter = 6;
    //    int index;
    //    for(int i = 0; i < iter; i++)
    //    {
    //        index = i;
    //        if (index >= colorArray.Length)
    //        {
    //            index = index % colorArray.Length;
    //        }
    //        rend.material.SetColor("_Color", colorArray[index]);

    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}
}
