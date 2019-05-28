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
            //Debug.Log("Ho toccato il cubo");
            if (!collision.gameObject.GetComponent<Renderer>().material.color.Equals(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color) &&
                collision.gameObject.GetComponent<Renderer>().material.color != Color.white)
            {
                if (!morto)
                {
                    Debug.Log("Son morto");
                    gameObject.GetComponent<Movimento>().Morte();
                    morto = true;
                }
            }
        }
    }
}
