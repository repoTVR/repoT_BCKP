using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoSingolo : MonoBehaviour
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

        if (play)
        {
            anim.SetBool("is_in_air", true);
            movimento = transform.forward * speed;
            movimento.y = jumpForce;
            play = false;
        }

        if (isDestinazioneRaggiunta)
        {
            anim.SetBool("is_in_air", false);
            movimento = Vector3.zero;
            StartCoroutine("WaitBeforeNextAction");
        }

        //Applicazione gravità
        movimento.y -= gravity * Time.deltaTime;

        //Applicazione movimento
        characterController.Move(movimento * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CuboFinale"))
        {
            Debug.Log("Ho toccato il cubo finale");
            isDestinazioneRaggiunta = true;
        }
    }

    private IEnumerator WaitBeforeNextAction()
    {
        yield return new WaitForSeconds(0.2f);
        ResetPosizione();

    }

    private void ResetPosizione()
    {
        play = true;
        isDestinazioneRaggiunta = false;
        gameObject.transform.position = posPartenza;
    }

}
