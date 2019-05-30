using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movimento : MonoBehaviour
{

    #region Pubblici

    [SerializeField] private float speed; //velocità camminata
    [SerializeField] private float jumpForce; //forza di salto

    public GameObject miniPanel; //Pannello esecuzione comandi
    
    public GameObject partVittoria;

    public ArrayList azioniList;

    public bool play;

    public int lvl;

    public bool riavviaLvl;

    //[HideInInspector] public ArrayList ifArrayList;

    #endregion

    #region Privati


    //Id del cubo su cui si trova il player
    int idCubo = -1;

    //Array di azioni per i vari livelli
    ArrayList azioniLvl1;
    ArrayList azioniLvl2;

    //Oggetto per la gestione dei livelli
    private GameObject lvlChanger;

    //Bool che controlla se il player è arrivato al cubo finale
    private bool arrivato;
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

    private bool caduto;
    private bool uno;
    private bool inPosizione;
    private bool isFor;
    private bool wasFor;

    private float oldRotation;
    private float targetRot;
    private float timeCount;
    private float gravity = 9.8f;

    private int idCuboAttuale; //id del cubo su cui il player è sopra
    private int indexAzione; //index dell'array azioni[]

    private ArrayList indexFor;
    float distY;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        lvlChanger = GameObject.FindGameObjectWithTag("LvlChanger");
        azioniList = new ArrayList();
        inPosizione = true;
        indexFor = new ArrayList();
        //ifArrayList = new ArrayList();
        uno = true;
        isFor = false;
        wasFor = false;
        timeCount = 0f;

        uno = true;

        #region ListaAzioniManuali

        //azioniLvl1 = new ArrayList();
        //azioniLvl2 = new ArrayList();
        //azioniLvl2.Add(3);
        //azioniLvl2.Add(0);
        //azioniLvl2.Add(3);
        //azioniLvl2.Add(4);
        //azioniLvl2.Add(4);
        //azioniLvl2.Add(4);
        //azioniLvl2.Add(4);
        //azioniLvl2.Add(4);
        //azioniLvl2.Add(0);
        //azioniLvl2.Add(2);
        //azioniLvl2.Add(0);
        //azioniLvl2.Add(3);

        //azioniLvl1.Add(3);
        //azioniLvl1.Add(0);
        //azioniLvl1.Add(3);
        //azioniLvl1.Add(0);
        //azioniLvl1.Add(2);
        //azioniLvl1.Add(4);
        //azioniLvl1.Add(4);
        //azioniLvl1.Add(4);
        //azioniLvl1.Add(4);
        //azioniLvl1.Add(4);
        //azioniLvl1.Add(4);
        //azioniLvl1.Add(0);
        //azioniLvl1.Add(3);
        ////azioniLvl1.Add(2);
        ////azioniLvl1.Add(0);
        ////azioniLvl1.Add(1);
        ////azioniLvl1.Add(0);
        ////azioniLvl1.Add(3);

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
            //if (lvl == 1)
            //{
            //    azioniList = (ArrayList)azioniLvl1.Clone();
            //}
            //if (lvl == 2)
            //{
            //    azioniList = (ArrayList)azioniLvl2.Clone();
            //}
            //Debug.Log("Play ");

            CambiaAzione();
            play = false;
        }

        //Controllo atterraggio
        if(characterController.isGrounded)
        {
            anim.SetBool("is_in_air", false);
        }

        distY = transform.position.y - lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale).transform.position.y;
        Debug.Log("Dist = " + distY);

        //Check fine movimento avanti/salto
        if (IsDestinazioneRaggiunta())
        {

            
            anim.SetBool("run", false);
            //if (lvlController.GetComponent<Percorso>().GetCuboById(idCuboAttuale).name.Equals(ultimoCubo.name))
            if(arrivato)
            {
                if (uno)
                {
                    movimento = Vector3.zero;
                    Vittoria();
                    uno = false;
               
                }
            }
            else
            {
                //Debug.Log("Sono arrivato");
                lvlController.GetComponent<Percorso>().IncrementIndexPercorso();
                ResetMovimento();
            }
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

    public void setIsFor(bool flag)
    {
        isFor = flag;
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

    public void ForActionCalled(int n)
    {
        int c = azioniList.Count-1;

        for(int i = 0; i < n; i++)
        {
            indexFor.Add(c + i);
        }
    }

    //Identificazione nuova azione da svolgere
    void CambiaAzione()
    {
        //Debug.Log("Dentro Cambia Azione");

        wasFor = isFor;
        setIsFor(false);

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
                        Debug.Log("Attacco");
                        StartCoroutine("Attacca");
                        break;
                    }

                case 64:
                    {
                        setIsFor(true);
                        StartCoroutine("Attacca");
                        break;
                    }

                case 5:
                    {
                        //Caso cambia colore, il player diventa dello stesso colore del cubo successivo
                        CambiaColore();

                        break;
                    }
            }
        }else
        {
            movimento = Vector3.zero;
        }

        if(isFor)
        {
            miniPanel.GetComponent<MiniPanelScript>().SelectFor();
        }else
        {
            if(wasFor)
            {
                miniPanel.GetComponent<MiniPanelScript>().RefreshIndexButtonAfterFor();
                miniPanel.GetComponent<MiniPanelScript>().ClearAfterFor();
            }
            miniPanel.GetComponent<MiniPanelScript>().selectButton();
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
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponentInChildren<HandAtacck>().isAttacking = true;
        anim.SetBool("attack", true);
        length = anim.GetCurrentAnimatorClipInfo(0).Length;
        //Debug.Log("length = " + length);
        yield return new WaitForSeconds(length);
        //arma.GetComponent<AxeScript>().isAttacking = false;
        gameObject.GetComponentInChildren<HandAtacck>().isAttacking = false;
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(0.25f);
        ResetMovimento();
    }

    private bool IsDestinazioneRaggiunta()
    {
        if(posDestinazione != null)
        {
            float x = posDestinazione.transform.position.x - transform.position.x;
            float z = posDestinazione.transform.position.z - transform.position.z;
            float y = posDestinazione.transform.position.y - transform.position.y;

            return Mathf.Abs(x + z) < .05f && Mathf.Abs(y) < .25f;
        }else
        {
            return true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag.Equals("Terrain"))
        {
            caduto = true;
            Morte();
        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<HealthScript>().anim.SetBool("playerDead", true);
            Morte();
        }

        if (collision.gameObject.CompareTag("Cubo"))
        {
            //Id del cubo con cui sto collidendo
            idCubo = collision.gameObject.GetComponent<IdCubo>().GetId();
            //Debug.Log("Sono arrivato al cubo n " + idCubo);

            //Debug.Log("Il colore è " + gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color);

            //Se sono arrivato al cubo finale
            if (idCubo == lvlController.GetComponent<Percorso>().GetCuboFinale().gameObject.GetComponent<IdCubo>().GetId())
            {
                arrivato = true;
            }

            //Se sono arrivato al cubo immediatamente prima di quello da cui parte il cambio colore cambio bool per bloccare funzione

            ColorChangerLvl  colorChangerLvl = lvlController.GetComponent<ColorChangerLvl>();
            if ((idCubo+1) == colorChangerLvl.cuboPartenza[SceneManager.GetActiveScene().buildIndex])
            {
                Debug.Log("Bloccato il cambio colore");
                colorChangerLvl.play = false;
            }

        }

    }

    public void Morte()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");
        movimento = Vector3.zero;
        anim.CrossFade("Morte", 0.1f);


        foreach (Transform tr in menu.transform)
        {
            tr.gameObject.SetActive(tr.gameObject.tag.Equals("PanelMorte") || tr.gameObject.tag.Equals("PanelMenu"));
        }

        if (riavviaLvl)
        {
            lvlChanger.GetComponent<SceneSetup>().Riavvia();
            riavviaLvl = false;
        }
    }

    public bool getCaduto()
    {
        return caduto;
    }

    public void setCaduto(bool caduto)
    {
        this.caduto = caduto;
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
            tr.gameObject.SetActive(tr.CompareTag("PanelVittoria") || tr.gameObject.tag.Equals("PanelMenu"));
        }
        GameObject panelVittoria = GameObject.FindGameObjectWithTag("PanelVittoria");
        //Dimensioni dell'immagine della vittoria
        RectTransform imgTransform = panelVittoria.GetComponentInChildren<Image>().rectTransform;
        //Debug.Log("Img -> " + panelVittoria.GetComponentInChildren<Image>());

        //Larghezza dell'immagine
        float size = imgTransform.sizeDelta.x;

        //Numero delle stelle ottenute
        float numStelle = lvlChanger.GetComponent<SceneSetup>().getPercLvl((float)azioniList.Count);
        //Debug.Log("Num stelle ottenute = " + numStelle);
        imgTransform.sizeDelta = new Vector2(imgTransform.sizeDelta.x * numStelle, imgTransform.sizeDelta.y);

        //lvlChanger.GetComponent<SceneSetup>().LoadNextScene();
    }

    private void CambiaColore()
    {
        //Debug.Log("Case cambia colore ");
        //Debug.Log("Il colore del cubo successivo è " + lvlController.GetComponent<Percorso>().GetCuboById(idCubo + 1).GetComponent<Renderer>().material.color);
        Color colorCuboSucc = lvlController.GetComponent<Percorso>().GetCuboById(idCubo + 1).GetComponent<Renderer>().material.color;
        gameObject.GetComponent<PlayerColorChanger>().ChangeColor(colorCuboSucc);
        ResetMovimento();
    }
}