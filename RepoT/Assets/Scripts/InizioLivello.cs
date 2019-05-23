using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InizioLivello : MonoBehaviour
{

    Transform transformIniziale;
    GameObject player;
    GameObject miniPanel;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transformIniziale = player.transform;
        miniPanel = GameObject.FindGameObjectWithTag("PanelEsecuzione");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Riavvia()
    {
        ClearListaAzioniETabellone();
        player.transform.position = transformIniziale.position;
        player.transform.rotation = transformIniziale.rotation;

    }

    public void ClearListaAzioniETabellone()
    {
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().EliminaTutteLeAzioni();
            miniPanel.GetComponent<MiniPanelScript>().EliminaTutteLeAzioni();
        }
    }
}
