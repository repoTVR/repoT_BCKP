using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoSingolo : MonoBehaviour
{
    public GameObject cuboIniziale;
    public GameObject cuboFinale;
    private Vector3 distanzaCubi;
    public bool isDestinazioneRaggiunta;
    private Animator anim;
    private Vector3 movimento = Vector3.zero;
    [SerializeField] private float speed = 2f; //velocità camminata
    [SerializeField] private float jumpForce = 5f; //forza di salto
    private GameObject player;
    private Vector3 posPartenza;
    private CharacterController characterController;
    private float gravity = 9.8f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        posPartenza = player.transform.position;
        anim = GetComponent<Animator>();
        distanzaCubi = cuboFinale.transform.position - cuboIniziale.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDestinazioneRaggiunta)
        {
            anim.SetBool("is_in_air", true);
            movimento = transform.forward * speed;
            movimento.y = jumpForce;
        }
        else
        {
            anim.SetBool("is_in_air", false);
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
        yield return new WaitForSeconds(0.5f);
        ResetPosizione();
    }

    private void ResetPosizione()
    {
        player.transform.position = posPartenza;
    }
}
