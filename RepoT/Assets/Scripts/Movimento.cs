﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class Movimento : MonoBehaviour
{
    [SerializeField] private int[] azioni; //array di azioni
    [SerializeField] private float speed; //velocità camminata
    [SerializeField] private float jumpForce; //forza di salto
    [SerializeField] private float rotationSpeed = 5f; //velocità di rotazione

    private CharacterController characterController;
    private Vector3 movimento;
    private Animator anim;
    private GameObject lvlController;
    private AnimationEvent eventPostRotazione;


    private GameObject primoCubo;
    private GameObject ultimoCubo;

    #region Davide
    ////Prova per highlight della selezione in esecuzione al momento
    public GameObject miniPanel;
    private Quaternion rotation;

    private Quaternion oldRotation;

    public GameObject lightBeamIniziale;
    public GameObject lightBeamFinale;
    Vector3 posLightBeamIniziale;
    Vector3 posLightBeamFinale;
    public bool inPosizione;
    public GameObject partVittoria;
    public ArrayList azioniList;
    public bool play;

    #endregion 

    private Transform posAttuale; //transform posizione attuale (aggiornamento)
    private GameObject posDestinazione; //transform posizione cubo successivo (aggiornamento)
    private int idCuboAttuale; //id del cubo su cui il player è sopra

    private bool giraSx;
    private bool giraDx;

    private float gravity = 9.8f;
    private bool flagHit;
    private int indexAzione; //index dell'array azioni[]
    private string nomeOldCollision; //nome del gameobject cubo con cui ho colliso precedentemente
    private Vector3 distanzaCubi; //distanza spostamento



    // Start is called before the first frame update
    void Start()
    {
        #region Davide
        azioniList = new ArrayList();
        //azioniList.Add(3);
        //azioniList.Add(0);
        //azioniList.Add(3);
        //azioniList.Add(0);
        //azioniList.Add(2);
        //azioniList.Add(0);
        //azioniList.Add(3);

        rotation = Quaternion.Euler(0f, 0f, 0f);
        #endregion
        lvlController = GameObject.FindGameObjectWithTag("GameController");
        primoCubo = lvlController.GetComponent<Percorso>().GetCuboById(0);
        ultimoCubo = lvlController.GetComponent<Percorso>().GetCuboFinale();

        #region Davide

        
        posLightBeamIniziale = new Vector3(primoCubo.transform.position.x, primoCubo.transform.position.y + 0.579f, primoCubo.transform.position.z);
        Instantiate(lightBeamIniziale, posLightBeamIniziale, Quaternion.identity);

        posLightBeamFinale = new Vector3(ultimoCubo.transform.position.x, ultimoCubo.transform.position.y + 0.579f, ultimoCubo.transform.position.z);
        Instantiate(lightBeamFinale, posLightBeamFinale, Quaternion.identity);
        #endregion

        characterController = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        movimento = Vector3.zero;
        distanzaCubi = Vector3.zero;
        nomeOldCollision = primoCubo.name;

        posAttuale = transform;
        idCuboAttuale = lvlController.GetComponent<Percorso>().GetIndexPercorso();
        posDestinazione = lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale + 1);

        //Inizio movimento
        //CambiaAzione();

    }

    // Update is called once per frame
    void Update()
    {
        //Inizio movimento (in teoria)
        if (play)
        {
            CambiaAzione();
            play = false;
        }

        if(characterController.isGrounded)
        {
            anim.SetBool("is_in_air", false);
        }

        if(IsDestinazioneRaggiunta())
        {
            anim.SetBool("run", false);
            if (posDestinazione.name.Equals(ultimoCubo.name))
            {
                Instantiate(partVittoria, posLightBeamFinale, Quaternion.Euler(-90f, 0f, 0f));
                anim.CrossFade("Vittoria", .1f);
                lvlController.GetComponent<PlayStopPlayerMovimento>().Stop();
            }
            lvlController.GetComponent<Percorso>().IncrementIndexPercorso();
            ResetMovimento();
        }

        movimento.y -= gravity * Time.deltaTime;

        characterController.Move(movimento * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //Debug.Log(Mathf.Abs(transform.localEulerAngles.y));
        //Debug.Log("Rot finale = " + rotFinale);

        if ( (transform.localEulerAngles.y) == 270.0001f && !inPosizione)
        {
            ResetMovimento();
            Debug.Log("entrato if");
            inPosizione = true;
        }

    }


    public void ResetMovimento()
    {
        Debug.Log("Reset movimento");   
        movimento = Vector3.zero;
        indexAzione++;
        CambiaAzione();

    }

    void CambiaAzione()
    {
        miniPanel.GetComponent<MiniPanelScript>().selectButton(indexAzione);
        posAttuale = transform;
        idCuboAttuale = lvlController.GetComponent<Percorso>().GetIndexPercorso();
        posDestinazione = lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale + 1);
        
        if(posDestinazione == null)
        {
            Debug.Log("Errore");
        }

        if (indexAzione < azioniList.Count)
        {
            switch (azioniList[indexAzione])
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
        inPosizione = false;
        oldRotation = transform.rotation;
        //anim.SetBool("giraDx", true);
        //transform.Rotate(0f, 90f, 0f, Space.Self);
        //rotation = new Vector3(0, 90f * rotationSpeed, 0);
        rotation = Quaternion.Euler(0, this.rotation.y+90f, 0);
        //ResetMovimento();
    }

    void GiraSinistra()
    {
        inPosizione = false;
        oldRotation = transform.rotation;
        //anim.SetBool("giraSx", true);
        //transform.Rotate(0f, -90f, 0f, Space.Self);
        rotation = Quaternion.Euler(0, this.rotation.y-90f, 0);

        //ResetMovimento();
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
