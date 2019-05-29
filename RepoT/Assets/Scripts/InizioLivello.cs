using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InizioLivello : MonoBehaviour
{

    Vector3 positionIniziale;
    Quaternion rotIniziale;
    GameObject player;
    GameObject miniPanel;
    //SceneSetup sceneSetupScript;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        positionIniziale = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        rotIniziale = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z);
        miniPanel = GameObject.FindGameObjectWithTag("PanelEsecuzione");
        //sceneSetupScript = GameObject.FindGameObjectWithTag("LvlController").GetComponent<SceneSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ClearListaAzioniETabellone()
    {
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().EliminaTutteLeAzioni();
            //miniPanel.GetComponent<MiniPanelScript>().EliminaTutteLeAzioni();
        }
    }
}
