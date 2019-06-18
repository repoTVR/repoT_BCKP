using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamminataSingola : MonoBehaviour
{
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
            StartCoroutine("WaitBeforeNextAction");
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
        yield return new WaitForSeconds(0.5f);
        ResetPosizione();
    }

    private void ResetPosizione()
    {
        gameObject.transform.position = posPartenza;
        isDestinazioneRaggiunta = false;
    }


    //Se l'oggetto viene disattivato resetto la posizione così riparte dall'inizio
    private void OnDisable()
    {
        ResetPosizione();
    }
}
