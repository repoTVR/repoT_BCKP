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
    void FixedUpdate()
    {
        if (!isDestinazioneRaggiunta)
        {
            movimento = transform.forward * speed;
            anim.SetBool("run", true);
            characterController.Move(movimento * Time.deltaTime);
        }
        else
        {
            anim.SetBool("run", false);
            movimento = Vector3.zero;
            StartCoroutine("WaitBeforeNextAction");
        }
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
        yield return new WaitForSeconds(0.5f);
        ResetPosizione();
        yield return new WaitForSeconds(0.5f);
        isDestinazioneRaggiunta = false;
    }

    private void ResetPosizione()
    {
        gameObject.transform.position = posPartenza;
    }
}
