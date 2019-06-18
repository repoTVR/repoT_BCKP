using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAtacck : MonoBehaviour
{

    public bool isAttacking;
    private Vector3 finalSize;
    private Vector3 startSize;
    public float tempoPassato = 0f;
    public float tempoTotale;
    public float tempoPassato2;
    // Start is called before the first frame update
    void Start()
    {
        startSize = transform.localScale;
        finalSize = new Vector3(startSize.x + 3f, startSize.y + 3f, startSize.z + 3f);
        tempoTotale = 0.5f;
        tempoPassato = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tempo passato = " + tempoPassato);
        if (isAttacking)
        {

            //Lerpo dalla grandezza iniziale alla grandezza finale
            transform.localScale = Vector3.Lerp(startSize, finalSize, tempoPassato / tempoTotale);
            tempoPassato += Time.deltaTime;
            if(tempoPassato > 0.5f)
            {
                tempoPassato = 0.5f;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(startSize, finalSize, tempoPassato / tempoTotale);
            tempoPassato -= Time.deltaTime;
            if (tempoPassato < 0f)
            {
                tempoPassato = 0f;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                other.GetComponent<HealthScript>().StartCoroutine("loseHealth");
            }
        }
    }
}
