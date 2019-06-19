using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour
{
    private SkinnedMeshRenderer rend;
    private Animator anim;
    private bool morto = false;
    public bool debug;
    // Start is called before the first frame update
    void Start()
    {
        debug = true;
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
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
        if (collision.gameObject.CompareTag("Cubo"))
        {
            if(collision.gameObject.GetComponent<ColorChangerCubo>().getColore() == 1)
            {
                //Debug.Log("Ho toccato il cubo");
                Color colorCubo = collision.gameObject.GetComponent<Renderer>().material.color;
                if (!colorCubo.Equals(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color))
                {
                    if (!morto)
                    {
                        Debug.Log("Son morto");
                        Debug.Log("Colore : " + colorCubo);
                        Debug.Log("Nero " + Color.black);
                        gameObject.GetComponent<Movimento>().Morte();
                        morto = true;
                    }
                }
            }
            
        }
    }
}
