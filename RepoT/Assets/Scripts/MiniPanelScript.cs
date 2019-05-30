﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniPanelScript : MonoBehaviour
{
    public GameObject player;
    public static int cont;
    private int contatoreBottoni;
    public Button buttonPrefab;
    private Button actualButton;
    private SortedDictionary<int, Button> buttons;
    [SerializeField] private Sprite[] arrImg;
    [SerializeField] private Sprite[] arrNumeri;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new SortedDictionary<int, Button>();
        contatoreBottoni = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addButton(int id)
    {
        actualButton = Instantiate(buttonPrefab, transform, false);
        buttons.Add(cont, actualButton);
        cont++;
        actualButton.GetComponentInChildren<Image>().sprite = arrImg[id];
    }

    public void AddButtonSpecial(int id)
    {
        actualButton = Instantiate(buttonPrefab, transform, false);
        buttons.Add(cont, actualButton);
        cont++;
        actualButton.GetComponentInChildren<Image>().sprite = arrNumeri[id];
    }

    public Button getButtonById(int id)
    {
        if (buttons.ContainsKey(id))
        {
            return buttons[id];
        }
        else
        {
            return null;
        }

    }
    public void selectButton()
    {
        if(contatoreBottoni > 0 && getButtonById(contatoreBottoni-1) != null)
        {
            getButtonById(contatoreBottoni-1).GetComponentInChildren<RawImage>().enabled = false;
        }

        if(getButtonById(contatoreBottoni) != null && contatoreBottoni <= cont)
        {
            getButtonById(contatoreBottoni).GetComponentInChildren<RawImage>().enabled = true;
        }

        if (getButtonById(contatoreBottoni+1) != null && contatoreBottoni+1 <= cont)
        {
            getButtonById(contatoreBottoni+1).GetComponentInChildren<RawImage>().enabled = false;
        }

        contatoreBottoni++;
        

    }

    public void RefreshIndexButtonAfterFor()
    {
        contatoreBottoni += 3;
    }

    public void ClearAfterFor()
    {
        getButtonById(contatoreBottoni - 2).GetComponentInChildren<RawImage>().enabled = false;
        getButtonById(contatoreBottoni - 3).GetComponentInChildren<RawImage>().enabled = false;
    }

    public void SelectFor()
    {
        if (contatoreBottoni > 0 && getButtonById(contatoreBottoni - 1) != null)
        {
            getButtonById(contatoreBottoni - 1).GetComponentInChildren<RawImage>().enabled = false;
        }

        if (getButtonById(contatoreBottoni) != null && contatoreBottoni <= cont)
        {
            getButtonById(contatoreBottoni).GetComponentInChildren<RawImage>().enabled = true;
            getButtonById(contatoreBottoni+1).GetComponentInChildren<RawImage>().enabled = true;
            getButtonById(contatoreBottoni+2).GetComponentInChildren<RawImage>().enabled = true;
        }
    }

    public void EliminaUltimoBottone()
    {
        if (buttons.ContainsKey(cont-1))
        {
            buttons.Remove(cont-1);
            Destroy(transform.GetChild(cont-1).gameObject);
            cont--;
            contatoreBottoni--;
        }
    }

    public void EliminaTutteLeAzioni()
    {
        buttons.Clear();
        foreach(Transform tr in transform)
        {
            Destroy(tr.gameObject);
        }
        cont = 0;
        contatoreBottoni = 0;
    }
}
