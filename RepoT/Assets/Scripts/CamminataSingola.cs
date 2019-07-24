using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamminataSingola : MonoBehaviour
{
    //private SkinnedMeshRenderer rend;
    private bool once;
    Color colorPartenza;
    public bool playerColorato;
    public bool morto = false;
    public GameObject cuboFinale;
    public bool isDestinazioneRaggiunta;
    private Animator anim;
    private Vector3 movimento = Vector3.zero;
    [SerializeField] private float speed = 2f; //velocità camminata
    [SerializeField] private float jumpForce = 5f; //forza di salto
    private Vector3 posPartenza;
    private CharacterController characterController;
    private float gravity = 9.8f;
    public bool play;
    // Start is called before the first frame update
    void Start()
    {
        once = true;
        colorPartenza = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        playerColorato = false;
        characterController = gameObject.GetComponent<CharacterController>();
        posPartenza = gameObject.transform.position;
        anim = GetComponent<Animator>();
        movimento = Vector3.zero;
        isDestinazioneRaggiunta = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestinazioneRaggiunta)
        {
            anim.SetBool("run", true);
            movimento = transform.forward * speed;
        }
        else
        {
            if (once)
            {
                StartCoroutine("WaitBeforeNextAction");
                once = false;
            }
        }
        characterController.Move(movimento * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CuboFinale"))
        {
            Debug.Log("Ho toccato il cubo finale");
            isDestinazioneRaggiunta = true;
        }
    }

    private IEnumerator WaitBeforeNextAction()
    {
        anim.SetBool("run", false);
        movimento = Vector3.zero;
        yield return new WaitForSeconds(1f);
        ResetPosizione();
    }

    private void ResetPosizione()
    {
        gameObject.transform.position = posPartenza;
        if (morto)
        {
            anim.CrossFade("Idle", 0.1f);
            morto = false;
        }
        if(GetComponent<PlayerColorChangerTutorial>() != null)
        {
            if (playerColorato)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material.color = colorPartenza;
                playerColorato = false;
            }
            Debug.Log("Change color = " + GetComponent<PlayerColorChangerTutorial>().changeColor);
            if (GetComponent<PlayerColorChangerTutorial>().changeColor)
            {
                GetComponent<PlayerColorChangerTutorial>().changeColor = false;
            }
            else if(!GetComponent<PlayerColorChangerTutorial>().changeColor)
            {
                GetComponent<PlayerColorChangerTutorial>().changeColor = true;
            }

            GetComponent<ChangeColorLvlTutorial>().play = true;
        }
        once = true;
        isDestinazioneRaggiunta = false;
    }


    //Se l'oggetto viene disattivato resetto la posizione così riparte dall'inizio
    private void OnDisable()
    {
        if (morto)
        {
            anim.CrossFade("Idle", 0.1f);
            morto = false;
        }
        
        ResetPosizione();
    }


    public void Morte()
    {
        isDestinazioneRaggiunta = true;
        morto = true;
        anim.CrossFade("Morte", 0.1f);

    }
}
