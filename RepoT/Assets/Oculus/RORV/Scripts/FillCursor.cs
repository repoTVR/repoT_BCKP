using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillCursor : MonoBehaviour {

    public float sec = 3f;
    public float tiempo = 0f;
    Image radial;
    bool go = false;
    float tiempoinicio = 0f;
    //public Transform radial;

	// Use this for initialization
	void Start () {

        NotificationCenter.DefaultCenter().AddObserver(this, "EnBoton");
        NotificationCenter.DefaultCenter().AddObserver(this, "EnNada");

        radial = GetComponent<Image>();
        radial.fillAmount = 1;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (go)
        {
            
            //tiempo = tiempo + Time.deltaTime;
            //Debug.Log("Tiempo = " + tiempo);

            radial.fillAmount += (Time.deltaTime/sec);
            //Debug.Log("Fill = " + radial.fillAmount);
            //print(radial.fillAmount);

        }
        else {
            //Debug.Log("Uscito");
            radial.fillAmount = 0;
      
        }

        
    }

    void EnBoton(Notification notificacion)
    {
        go = true;
        tiempo = 0;
    }

    void EnNada(Notification notificacion)
    {
        go = false;
        //tiempo = 1;
    }
}
