using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAtacck : MonoBehaviour
{

    //Bool per far partire o stoppare l'animazione di ingradimento mano
    public bool isAttacking;

    //Size iniziale della mano
    private Vector3 finalSize;

    //Size finale della mano
    private Vector3 startSize;

    //Gestione del tempo
    public float tempoPassato = 0f;
    public float tempoTotale;


    void Start()
    {
        //Startsize è il size della mano all'inizio
        startSize = transform.localScale;

        //Size finale sarà 3 volte quello iniziale
        finalSize = new Vector3(startSize.x + 3f, startSize.y + 3f, startSize.z + 3f);

        //Durata dell'animazione
        tempoTotale = 0.5f;
        tempoPassato = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Is attacking + " + isAttacking);
        if (isAttacking)
        {

            //Lerpo dalla grandezza iniziale alla grandezza finale con tempo 0.5f
            transform.localScale = Vector3.Lerp(startSize, finalSize, tempoPassato / tempoTotale);
            tempoPassato += Time.deltaTime;

            //Faccio in modo che tempo passato non superi mai il valore 0.5f
            if(tempoPassato > 0.5f)
            {
                tempoPassato = 0.5f;
            }
        }
        else
        {

            //Lerpo al contrario
            transform.localScale = Vector3.Lerp(startSize, finalSize, tempoPassato / tempoTotale);
            tempoPassato -= Time.deltaTime;

            //Faccio in modo che tempo passato non scenda mai sotto 0
            if (tempoPassato < 0f)
            {
                tempoPassato = 0f;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Se mano entra in collisione con nemico
        if (other.CompareTag("Enemy"))
        {
            //Controllo per evitare collisioni accidentali
            if (isAttacking)
            {
                other.GetComponent<HealthScript>().StartCoroutine("loseHealth");
            }
        }
    }

    private void OnDisable()
    {
        //Quando lo script viene disattivato riporto la mano alla dimensione iniziale e azzero contatore del tempo
        transform.localScale = startSize;
        tempoPassato = 0f;
    }
}
