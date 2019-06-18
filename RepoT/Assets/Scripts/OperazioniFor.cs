using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperazioniFor : MonoBehaviour
{

    private int idAzione;
    private int numVolte;
    private GameObject miniPanel;
    private int counterMaxAzioni = 12;

    // Start is called before the first frame update
    void Start()
    {
        miniPanel = GameObject.FindGameObjectWithTag("PanelEsecuzione");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdAzione(int id)
    {
        idAzione = id;
        Debug.Log("ID SetIdAzione" + id);
    }

    public int GetIdAzione()
    {
        return idAzione;
    }

    public void SetNumVolte(int n)
    {
        numVolte = n;
        AddForAction();
    }

    public int GetNumVolte()
    {
        return numVolte;
    }

    public void AddForAction()
    {
        GameObject panel = GameObject.FindGameObjectWithTag("PanelEsecuzione");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (miniPanel.GetComponent<MiniPanelScript>().GetCont() < counterMaxAzioni-2)
        {
            panel.GetComponent<MiniPanelScript>().addButton(GetIdAzione());
            panel.GetComponent<MiniPanelScript>().AddButtonSpecial(0);
            panel.GetComponent<MiniPanelScript>().AddButtonSpecial(GetNumVolte());

            for (int i = 0; i < GetNumVolte(); i++)
            {
                player.GetComponent<Movimento>().azioniList.Add(60 + GetIdAzione());

            }
        }else
        {
            Debug.Log("N Max");
        }
            
    }
}
