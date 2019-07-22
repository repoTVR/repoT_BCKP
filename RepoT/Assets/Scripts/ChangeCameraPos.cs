using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPos : MonoBehaviour
{

    private GameObject[] cameraPos;
    private GameObject cameraGaze;
    private int cont;

    // Start is called before the first frame update
    void Start()
    {
        cameraGaze = GameObject.FindGameObjectWithTag("CameraGaze");
        cameraPos = GameObject.FindGameObjectsWithTag("CameraPos");
        cont = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
            if(cont >= cameraPos.Length)
            {
                cont = 0;
            }
            cameraGaze.transform.position = cameraPos[cont].transform.position;
            cameraGaze.transform.rotation = cameraPos[cont].transform.rotation;

            cont++;
        }    
    }
}
