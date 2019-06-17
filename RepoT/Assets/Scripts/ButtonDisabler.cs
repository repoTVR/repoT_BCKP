using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    GameObject panelTutorial;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        panelTutorial = GameObject.FindGameObjectWithTag("PanelTutorial");
        button = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if(panelTutorial.GetComponent<TextSkipper>().cont <= 0)
        {
            button.interactable = false;
        }
    }
}
