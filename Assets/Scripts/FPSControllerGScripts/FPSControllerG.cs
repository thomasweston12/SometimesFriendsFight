using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerG : MonoBehaviour {
    
    public float defaultSpeed = 0.09f;
    private float speed;
    Animator anim;
    private bool isJumping;
    public Rigidbody rb;
    public Vector3 jumpForce;
    private float waitTime = 0.9f;
    float timer;
    bool isTimerRunning;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        Invoke("ResetIsJumping", 0);
        Invoke("ResetIsPickingUp", 0);
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update() {

        if (isTimerRunning == true) { 
            PickupTimer();
        }

        
        float ForwardBack = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        //ForwardBack *= Time.deltaTime;
        //strafe *= Time.deltaTime;

        if (ForwardBack > speed)
            ForwardBack = speed;

        if (strafe > speed)
            strafe = speed;

        transform.Translate(strafe, 0, ForwardBack);

        // Multiplied the second argument as a quick fix to solve the difference between the speeds in the Animator Controller,
        // and the speed of the actual character model. Makes the transition between different animations actually work. - Thomas Weston.

        // Also I messed something up in the animator and I don't know how to fix it, so assume ForwardBack is strafe speed and
        // strafe is forwards and backwards speed, sorry! - Thomas Weston.

        anim.SetFloat("speed", strafe*6);
        anim.SetFloat("strafeSpeed", ForwardBack*6);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButton("Jump") && anim.GetBool("isJumping") == false)
        {
            rb.AddForce(0.0f, 600.0f, 0.0f, ForceMode.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            Invoke("ResetIsJumping", 1.0f);

        }

        if (Input.GetButton("Action") && anim.GetBool("isPickingUp") == false && anim.GetBool("isJumping") == false && anim.GetBool("hasItem") == false)
        { 
            PickUpItem();   
        }

        if (Input.GetButton("Drop") && anim.GetBool("isPickingUp") == false)
        {
            DropItem();
        }

        if (Input.GetButton("Fire1") && anim.GetBool("hasItem") == true && anim.GetBool("isPickingUp") == false)
        {
            ThrowObject();
        }


        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;		
	}

    private void ResetIsJumping()
    {
        anim.SetBool("isJumping", false);
    }

    private void ResetIsPickingUp()
    {
        anim.SetBool("isPickingUp", false);


    }

    private void PickUpItem()
    {
        anim.SetBool("isPickingUp", true);
        anim.SetBool("hasItem", true);
        Invoke("ResetIsPickingUp", 0.4f);
    }

    private void DropItem()
    {
        anim.SetBool("hasItem", false);
    }

    private void ThrowObject()
    {
        anim.SetTrigger("throw");
        anim.SetBool("hasItem", false);
    }

    void PickupTimer()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {

            timer = 0f;
            speed = defaultSpeed;
            anim.SetBool("isPickingUp", false);
            isTimerRunning = false;
        }


    }

}