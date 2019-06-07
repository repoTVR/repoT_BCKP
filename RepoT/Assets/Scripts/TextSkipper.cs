using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSkipper : MonoBehaviour
{

    public TextMeshProUGUI[] textArray;
    private int cont;
    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        textArray[cont].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextText()
    {
        //Disattivo il testo corrente
        textArray[cont].gameObject.SetActive(false);
        //Aumento il contatore
        cont++;

        //Controllo se sono arrivato alla fine dell'array e in tal caso azzero
        cont %=  textArray.Length;
        //Attivo il testo successivo
        textArray[cont].gameObject.SetActive(true);

    }

    public void RepeatAudio()
    {
        foreach (Transform tr in gameObject.transform)
        {
            if (tr.gameObject.activeSelf)
            {
                tr.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
