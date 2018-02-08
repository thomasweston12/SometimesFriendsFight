using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerG : MonoBehaviour {
    
    public float speed = 0.3f;
    Animator anim;
    private bool isJumping;
    public Rigidbody rb;
    public Vector3 jumpForce;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        Invoke("resetIsJumping", 0);


	}
	
	// Update is called once per frame
	void Update () {

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
        anim.SetFloat("speed", strafe*6);
        anim.SetFloat("strafeSpeed", ForwardBack*6);
        Debug.Log(ForwardBack);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButton("Jump") && isJumping == false)
        {
            rb.AddForce(0.0f, 600.0f, 0.0f, ForceMode.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            Invoke("resetIsJumping", 1.0f);

        }

        Debug.Log(isJumping);


        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;		
	}

    private void resetIsJumping()
    {
        isJumping = false;
        anim.SetBool("isJumping", false);
    }
}
