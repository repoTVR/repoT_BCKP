using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    float movementH, movementV;
    Vector3 motion;
    public float speed;
    public float jumpSpeed = 8.0f;
    public float gravity = 9.8f;
    private Animator anim;
    public AnimationClip animClip;
    private AnimationEvent animEvent;
    // Start is called before the first frame update
    void Start()
    {
        animEvent = new AnimationEvent();
        animEvent.functionName = "Jump";
        animEvent.time = (0.20f);
        animClip.AddEvent(animEvent);
        cc = GetComponent<CharacterController>();
        speed = 3.0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementH = Input.GetAxis("Horizontal");
        movementV = Input.GetAxis("Vertical");
        Debug.Log("Is grounded = " + cc.isGrounded);
        if (cc.isGrounded)
        {
            anim.SetBool("isJumping", false);
            motion = new Vector3(movementH, 0.0f, movementV);
            if(movementH != 0.0f || movementV != 0.0f)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            if (Input.GetButton("Jump"))
            {
                //StartCoroutine("Jump");
                anim.SetBool("isJumping", true);
                //Debug.Log("Salto\n");
                ////motion = new Vector3(movementH, jumpSpeed, movementV);
            }
        }
        else
        {
            motion.y -= gravity * Time.deltaTime;

            // Move the controller
        }
        cc.Move(motion * speed * Time.deltaTime);
    }

    //public void jump()
    //{
    //    transform.Translate(new Vector3(0f, jumpSpeed, 4.0f));
    //}

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.2f);
        motion = new Vector3(movementH, jumpSpeed, movementV);
        yield return null;
    }
}
