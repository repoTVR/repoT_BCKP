using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickAttack : MonoBehaviour
{

    private GameObject lvlChanger;
    // Start is called before the first frame update
    void Start()
    {
        lvlChanger = GameObject.FindGameObjectWithTag("LvlChanger"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Call()
    {
        lvlChanger.GetComponent<SceneSetup>().LoadNextScene();
    }
}
