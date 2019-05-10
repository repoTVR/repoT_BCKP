using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private int[] azioni; //array di azioni
    [SerializeField] private float speed; //velocità camminata
    [SerializeField] private float jumpForce; //forza di salto
    [SerializeField] private GameObject lvlController;

    private CharacterController characterController;
    private Vector3 movimento;
    private Animator anim;

    private GameObject primoCubo;
    private GameObject ultimoCubo;

    private Transform posAttuale; //transform posizione attuale (aggiornamento)
    private GameObject posDestinazione; //transform posizione cubo successivo (aggiornamento)
    private int idCuboAttuale; //id del cubo su cui il player è sopra

    private float gravity = 9.8f;
    private bool flagHit;
    private int indexAzione; //index dell'array azioni[]
    private string nomeOldCollision; //nome del gameobject cubo con cui ho colliso precedentemente
    private Vector3 distanzaCubi; //distanza spostamento



    // Start is called before the first frame update
    void Start()
    {
        primoCubo = lvlController.GetComponent<Percorso>().GetCuboById(0);
        ultimoCubo = lvlController.GetComponent<Percorso>().GetCuboFinale();
        characterController = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        movimento = Vector3.zero;
        distanzaCubi = Vector3.zero;
        nomeOldCollision = primoCubo.name;
        CambiaAzione();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(characterController.isGrounded)
        {
            anim.SetBool("is_in_air", false);
        }

        if(IsDestinazioneRaggiunta())
        {
            anim.SetBool("run", false);
            if(posDestinazione.name.Equals(ultimoCubo.name))
            {
                anim.CrossFade("Vittoria", .1f);
                lvlController.GetComponent<PlayStopPlayerMovimento>().Stop();
            }
            lvlController.GetComponent<Percorso>().IncrementIndexPercorso();
            ResetMovimento();
        }

        movimento.y -= gravity * Time.deltaTime;

        characterController.Move(movimento * Time.deltaTime);
    }

    void ResetMovimento()
    {
        movimento = Vector3.zero;
        indexAzione++;
        CambiaAzione();

    }

    void CambiaAzione()
    {
        posAttuale = transform;
        idCuboAttuale = lvlController.GetComponent<Percorso>().GetIndexPercorso();
        posDestinazione = lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale + 1);
        
        if(posDestinazione == null)
        {
            Debug.Log("Errore");
        }

        if (indexAzione < azioni.Length)
        {
            switch (azioni[indexAzione])
            {
                case 0:
                    {
                        Cammina();
                        break;
                    }
                case 1:
                    {
                        GiraDestra();
                        break;
                    }
                case 2:
                    {
                        GiraSinistra();
                        break;
                    }
                case 3:
                    {
                        Salta();
                        break;
                    }
            }
        }else
        {
            movimento = Vector3.zero;
        }
        
    }

    void Cammina()
    {
        MoveToTarget(posDestinazione.transform.position);
    }

    void Salta()
    {
        JumpToTarget(posDestinazione.transform.position);
    }

    void GiraDestra()
    {
        transform.Rotate(0f, 90f, 0f, Space.Self);
        ResetMovimento();
    }

    void GiraSinistra()
    {
        transform.Rotate(0f, -90f, 0f, Space.Self);
        ResetMovimento();
    }

    private void MoveToTarget(Vector3 target)
    {
        distanzaCubi = target - transform.position;

        if(!IsDestinazioneRaggiunta())
        {
            anim.SetBool("run", true);
            movimento = transform.forward * speed;
            //distanzaCubi.normalized
        }
    }

    private void JumpToTarget(Vector3 target)
    {
        distanzaCubi = target - transform.position;

        if (!IsDestinazioneRaggiunta())
        {
            anim.SetBool("is_in_air", true);
            movimento = transform.forward * speed;
            //distanzaCubi.normalized
            movimento.y = jumpForce;
        }
    }

    private bool IsDestinazioneRaggiunta()
    {
        float x = posDestinazione.transform.position.x - transform.position.x;
        float z = posDestinazione.transform.position.z - transform.position.z;
        float y = posDestinazione.transform.position.y - transform.position.y;

        return Mathf.Abs(x + z) < .2f && Mathf.Abs(y) < 1.2f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Terrain"))
        {
            Morte();
        }
    }

    void Morte()
    {
        movimento = Vector3.zero;
        anim.CrossFade("Morte", 0.1f);
    }

}
