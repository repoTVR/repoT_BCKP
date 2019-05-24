using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerLvl : MonoBehaviour
{
    //Parametro per decidere da che cubo iniziare a colorare, eventualmente da prendere in base al livello
    int cuboPartenza = 1;
    ArrayList cubi;

// Start is called before the first frame update
    void Start()
    {
        cubi = new ArrayList();
        cubi = GetComponent<Percorso>().getCuboMaggId(cuboPartenza);
        foreach(GameObject c in cubi)
        {
            c.GetComponent<ColorChangerCubo>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
