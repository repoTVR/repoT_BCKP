using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickAttack : MonoBehaviour
{
    //Bool debugging senza oculus
    public bool nextScene;
    private GameObject lvlChanger;
    // Start is called before the first frame update
    void Start()
    {
        lvlChanger = GameObject.FindGameObjectWithTag("LvlChanger"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (nextScene)
        {
            Call();
            nextScene = false;
        }
    }

    public void Call()
    {
        lvlChanger.GetComponent<SceneSetup>().LoadNextScene();
    }
}
