using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSkipper : MonoBehaviour
{
    //Per debugging senza oculus
    public bool prev, next, repeat;

    //Array di testi
    public TextMeshProUGUI[] textArray;

    //Contatore
    public int cont;


    void Start()
    {
        cont = 0;

        //Si fa partire il primo testo
        textArray[cont].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (next)
        {
            NextText();
            next = false;
        }
        if (prev)
        {
            PrevText();
            prev = false;
        }
        if (repeat)
        {
            RepeatPanel();
            repeat = false;
        }
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
        //Attivo tutti i figli
        foreach (Transform tr in textArray[cont].transform)
        {
            tr.gameObject.SetActive(true);
        }

    }

    public void PrevText()
    {
        //Disattivo il testo corrente
        textArray[cont].gameObject.SetActive(false);

        if(cont > 0)
        {
            cont--;
        }
        else
        {
            cont = textArray.Length - 1;
        }

        //Attivo il padre
        textArray[cont].gameObject.SetActive(true);

        //Attivo tutti i figli
        foreach (Transform tr in textArray[cont].transform)
        {
            tr.gameObject.SetActive(true);
        }

    }

    public void RepeatPanel()
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
