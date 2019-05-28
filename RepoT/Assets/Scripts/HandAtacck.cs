using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAtacck : MonoBehaviour
{

    public bool isAttacking;
    private Vector3 finalSize;
    private Vector3 startSize;
    public float tempoPassato;
    public float tempoTotale;
    // Start is called before the first frame update
    void Start()
    {
        startSize = transform.localScale;
        finalSize = new Vector3(startSize.x + 3f, startSize.y + 3f, startSize.z + 3f);
        tempoTotale = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            transform.localScale = Vector3.Lerp(startSize, finalSize, tempoPassato / tempoTotale);
            tempoPassato += Time.deltaTime;
        }
        else
        {
            tempoPassato = 0f;
            transform.localScale = startSize;
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
