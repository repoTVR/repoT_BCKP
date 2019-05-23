using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimento : MonoBehaviour
{

    #region Pubblici

    [SerializeField] private float speed; //velocità camminata
    [SerializeField] private float jumpForce; //forza di salto

    public GameObject miniPanel; //Pannello esecuzione comandi
    
    public GameObject partVittoria;

    public ArrayList azioniList;

    public bool play;

    #endregion

    #region Privati

    private CharacterController characterController;

    private Vector3 movimento;

    private Animator anim;

    private GameObject lvlController;
    private GameObject primoCubo;
    private GameObject ultimoCubo;
    private GameObject arma;
    private GameObject posDestinazione; //transform posizione cubo successivo (aggiornamento)

    private Transform posAttuale; //transform posizione attuale (aggiornamento)

    private Quaternion rotation;

    private Vector3 distanzaCubi; //distanza spostamento

    private bool morto;
    private bool uno;
    private bool inPosizione;

    private float oldRotation;
    private float targetRot;
    private float timeCount;
    private float gravity = 9.8f;

    private int idCuboAttuale; //id del cubo su cui il player è sopra
    private int indexAzione; //index dell'array azioni[]

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        azioniList = new ArrayList();
        inPosizione = true;
        uno = true;
        timeCount = 0f;


        #region ListaAzioniManuali

        //azioniList.Add(3);
        //azioniList.Add(0);
        //azioniList.Add(3);
        //azioniList.Add(0);
        //azioniList.Add(2);

        //azioniList.Add(4);
        //azioniList.Add(4);
        //azioniList.Add(4);
        //azioniList.Add(4);
        //azioniList.Add(4);
        //azioniList.Add(0);
        //azioniList.Add(3);

        #endregion


        arma = GameObject.FindGameObjectWithTag("Weapon");

        rotation = Quaternion.Euler(0f, 0f, 0f);

        rotation = transform.rotation;
        lvlController = GameObject.FindGameObjectWithTag("GameController");
        primoCubo = lvlController.GetComponent<Percorso>().GetCuboById(0);
        ultimoCubo = lvlController.GetComponent<Percorso>().GetCuboFinale();

        characterController = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        movimento = Vector3.zero;
        distanzaCubi = Vector3.zero;

        posAttuale = transform;
        idCuboAttuale = lvlController.GetComponent<Percorso>().GetIndexPercorso();
        posDestinazione = lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale + 1);


    }

    // Update is called once per frame
    void Update()
    {
        //Inizio movimento 
        if (play)
        {
            Debug.Log("Play ");
            CambiaAzione();
            play = false;
        }

        //Controllo atterraggio
        if(characterController.isGrounded)
        {
            anim.SetBool("is_in_air", false);
        }

        //Check fine movimento avanti/salto
        if(IsDestinazioneRaggiunta())
        {
            anim.SetBool("run", false);
            if (lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale).name.Equals(ultimoCubo.name))
            {
                if (uno)
                {
                    Vittoria();
                    uno = false;
                }
            }
            lvlController.GetComponent<Percorso>().IncrementIndexPercorso();
            ResetMovimento();
        }

        //Applicazione gravità
        movimento.y -= gravity * Time.deltaTime;

        //Applicazione movimento
        characterController.Move(movimento * Time.deltaTime);

        //Applicazione rotazione
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, timeCount * 0.5f);
        timeCount += Time.deltaTime;

        //Check fine rotazione
        if (Mathf.Abs((transform.localEulerAngles.y - targetRot)) <= 0.01f && !inPosizione)
        {
            inPosizione = true;
            ResetMovimento();
            
        }

    }


    //Fine azione, switch nuova azione
    public void ResetMovimento()
    { 
        movimento = Vector3.zero;
        indexAzione++;
        CambiaAzione();

    }

    public void ClearIndex()
    {
        indexAzione = 0;
    }

    //Identificazione nuova azione da svolgere
    void CambiaAzione()
    {
        Debug.Log("Dentro Cambia Azione");
        miniPanel.GetComponent<MiniPanelScript>().selectButton(indexAzione);
        posAttuale = transform;
        idCuboAttuale = lvlController.GetComponent<Percorso>().GetIndexPercorso();
        posDestinazione = lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale + 1);
        

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
                case 4:
                    {
                        StartCoroutine("Attacca");
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
        timeCount = 0f;
        inPosizione = false;
        oldRotation = transform.localEulerAngles.y;
        targetRot = oldRotation + 90f;
        if (targetRot < 0)
        {
            targetRot += 360f;
        }

        rotation = Quaternion.Euler(0, targetRot, 0);
    }

    void GiraSinistra()
    {
        timeCount = 0f;
        inPosizione = false;
        oldRotation = transform.localEulerAngles.y;
        targetRot = oldRotation - 90f;
        if(targetRot < 0)
        {
            targetRot += 360f;
        }

        rotation = Quaternion.Euler(0, targetRot, 0);
    }

    private void MoveToTarget(Vector3 target)
    {
        distanzaCubi = target - transform.position;

        if(!IsDestinazioneRaggiunta())
        {
            anim.SetBool("run", true);
            movimento = transform.forward * speed;
        }
    }

    private void JumpToTarget(Vector3 target)
    {
        distanzaCubi = target - transform.position;

        if (!IsDestinazioneRaggiunta())
        {
            anim.SetBool("is_in_air", true);
            movimento = transform.forward * speed;
            movimento.y = jumpForce;
        }
    }
    

    IEnumerator Attacca()
    {
        float length;
        arma.GetComponent<AxeScript>().isAttacking = true;
        Debug.Log("Attacco");
        anim.SetBool("attack", true);
        length = anim.GetCurrentAnimatorClipInfo(0).Length;
        //Debug.Log("length = " + length);
        yield return new WaitForSeconds(length);
        arma.GetComponent<AxeScript>().isAttacking = false;
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(0.5f);
        ResetMovimento();
    }

    private bool IsDestinazioneRaggiunta()
    {
        if(posDestinazione != null)
        {
            float x = posDestinazione.transform.position.x - transform.position.x;
            float z = posDestinazione.transform.position.z - transform.position.z;
            float y = posDestinazione.transform.position.y - transform.position.y;

            return Mathf.Abs(x + z) < .05f && Mathf.Abs(y) < 1.2f;
        }else
        {
            return true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Terrain"))
        {
            Morte();
        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Ho toccato rudy");
            Morte();
        }
    }

    void Morte()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");
        movimento = Vector3.zero;
        anim.CrossFade("Morte", 0.1f);
        morto = true;

        foreach(Transform tr in menu.transform)
        {
            tr.gameObject.SetActive(tr.gameObject.tag.Equals("PanelMorte") || tr.gameObject.tag.Equals("PanelMenu"));
        }


    }

    public bool getMorto()
    {
        return morto;
    }

    public void EliminaUltimaAzione()
    {
        azioniList.RemoveAt(azioniList.Count - 1);
    }

    public void EliminaTutteLeAzioni()
    {
        azioniList.Clear();
    }

    private void Vittoria()
    {
        Transform light = GameObject.FindGameObjectWithTag("LightFinale").transform;
        Instantiate(partVittoria, light.position, Quaternion.Euler(-90f, 0f, 0f));
        anim.CrossFade("Vittoria", .1f);

        GameObject menu = GameObject.FindGameObjectWithTag("Menu");
        foreach (Transform tr in menu.transform)
        {
            tr.gameObject.SetActive(tr.CompareTag("PanelVittoria"));
        }
        GameObject panelVittoria = GameObject.FindGameObjectWithTag("PanelVittoria");
        //Dimensioni dell'immagine della vittoria
        RectTransform imgTransform = panelVittoria.GetComponentInChildren<Image>().rectTransform;
        Debug.Log("Img -> " + panelVittoria.GetComponentInChildren<Image>());

        //Larghezza dell'immagine
        float size = imgTransform.sizeDelta.x;

        //Numero delle stelle ottenute (da prendere eventualmente dal lvlmanager);
        float numStelle = 5f;
        imgTransform.sizeDelta = new Vector2(imgTransform.sizeDelta.x * numStelle, imgTransform.sizeDelta.y);

    }
}
