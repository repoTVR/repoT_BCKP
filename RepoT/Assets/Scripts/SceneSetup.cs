using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    private static int totScene = 2;
    private int[] numMosseLvl;
    


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        numMosseLvl = new int[2] { 6, 8 };
    }

    public void LoadNextScene()
    {
        //Prendo l'indice della scena
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = sceneIndex+1;
        //Se esiste una scena successiva la carico
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
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


}
