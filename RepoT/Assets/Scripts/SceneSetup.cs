using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    private static int totScene = 2;
    private int[] numMosseLvl;
    private static SceneSetup instance = null;



    private void Awake()
    {
        Initialization();
        numMosseLvl = new int[6] { 4, 2, 7, 9, 12, 16 };
    }

    public void LoadNextScene()
    {
        Debug.Log("Load next scene");
        //Prendo l'indice della scena
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = sceneIndex+1;
        Debug.Log("Siamo nella scena " + sceneIndex);
        //Se esiste una scena successiva la carico
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            //GameObject.FindGameObjectWithTag("PanelEsecuzione").GetComponent<MiniPanelScript>().ClearCont();
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Ultima scena");
        }

    }

    public float getPercLvl(float numMosse)
    {
        return (numMosseLvl[SceneManager.GetActiveScene().buildIndex] / numMosse);
    }


    private void Initialization()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void Riavvia()
    {
        //GameObject.FindGameObjectWithTag("PanelEsecuzione").GetComponent<MiniPanelScript>().ClearCont();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
