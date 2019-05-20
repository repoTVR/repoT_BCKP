using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percorso : MonoBehaviour
{

    private SortedDictionary<int,GameObject> percorso;
    private GameObject[] arrayCubi;
    private int keyPercorso;

    // Start is called before the first frame update
    void Awake()
    {
        percorso = new SortedDictionary<int, GameObject>();
        arrayCubi = GameObject.FindGameObjectsWithTag("Cubo");
        InizializzaMap();
        keyPercorso = 0;
    }

    void Stampa()
    {
        foreach(KeyValuePair<int,GameObject> vk in percorso)
        {
            Debug.Log("id: " + vk.Key + "nome" + vk.Value.name);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetIndexPercorso()
    {
        return keyPercorso;
    }

    public void IncrementIndexPercorso()
    {
        keyPercorso++;
    }

    public void DecrementIndexPercorso()
    {
        keyPercorso--;
    }

    public GameObject GetCuboFinale()
    {
        return GetCuboById(percorso.Count - 1);
    }

    public GameObject GetCuboById(int id)
    {
        if(percorso.ContainsKey(id))
        {
            return percorso[id];
        }
        else
        {
            Debug.Log("Chiave Non Trovata " + this.name);
            return null;
        }
    }

    private void InizializzaMap()
    {
        foreach(GameObject g in arrayCubi)
        {
            try
            {
                percorso.Add(g.GetComponent<IdCubo>().GetId(), g);
            }catch(ArgumentException e)
            {
                Console.WriteLine("Errore inserimento mappa " + e.ToString());
            }
        }
    }
}
