using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChangerTutorial : MonoBehaviour
{
    private int idCuboColorato;
    public GameObject cuboColorato;
    public bool morto;
    private SkinnedMeshRenderer rend;
    public bool changeColor;

    // Start is called before the first frame update
    void Start()
    {
        idCuboColorato = cuboColorato.GetComponent<IdCubo>().GetId();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Awake()
    {
        changeColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color color)
    {
        rend.material.SetColor("_Color", color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Se sto collidendo con un cubo e questo cubo ha il flag colore impostato
        if(collision.gameObject.CompareTag("Cubo") && collision.gameObject.GetComponent<ColorChangerCubo>().getColore() == 1)
        {
            //Prendo il colore del cubo
            Color colorCubo = collision.gameObject.GetComponent<Renderer>().material.color;
            //Se il cubo non è dello stesso colore del player
            if (!colorCubo.Equals(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color))
            {
                if (!gameObject.GetComponent<CamminataSingola>().morto)
                {
                    gameObject.GetComponent<CamminataSingola>().Morte();
                }
            }
        }

        if(collision.gameObject.CompareTag("Cubo") && (collision.gameObject.GetComponent<IdCubo>().GetId() == (idCuboColorato-1)))
        {
            GetComponent<ChangeColorLvlTutorial>().play = false;
            if (changeColor)
            {
                GetComponent<CamminataSingola>().playerColorato = true;
                //Debug.Log("Cambio colore");
                Color color = cuboColorato.gameObject.GetComponent<Renderer>().material.color;
                rend.material.SetColor("_Color", color);

            }
        }
    }


}
