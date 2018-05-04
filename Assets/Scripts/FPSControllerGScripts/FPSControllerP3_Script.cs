using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerP3_Script : MonoBehaviour
{

    public float defaultSpeed = 0.09f;
    private float speed;
    Animator anim;
    private bool isJumping;
    public Rigidbody rb;
    public Vector3 jumpForce;
    private float waitTime = 0.9f;
    float timer;
    bool isTimerRunning;
    public float dist = 25.0f;
    private bool isNear;
    private Camera cam;
    Vector3 screenCentre = new Vector3(Screen.width / 2, Screen.height / 2);
    public int playerHealth = 100;
    private GameObject currentPlayer;
    private GameObject itemPickedUp;
    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        Invoke("ResetIsJumping", 0);
        Invoke("ResetIsPickingUp", 0);
        speed = defaultSpeed;
        cam = GetComponentInChildren<Camera>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isTimerRunning == true)
        {
            PickupTimer();
        }

        float ForwardBack = Input.GetAxis("P3GameForwardsBackwards") * speed;
        float strafe = Input.GetAxis("P3GameStrafeRightLeft") * speed;
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

        anim.SetFloat("speed", strafe * 6);
        anim.SetFloat("strafeSpeed", ForwardBack * 6);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButton("P3GameJump") && anim.GetBool("isJumping") == false)
        {
            rb.AddForce(0.0f, 600.0f, 0.0f, ForceMode.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            Invoke("ResetIsJumping", 1.0f);

        }

        if (Input.GetButton("P3GamePickup") && anim.GetBool("isPickingUp") == false && anim.GetBool("isJumping") == false && anim.GetBool("hasItem") == false)
        {
            PickUpItem();
        }

        if (Input.GetButton("P3GameDropItem") && anim.GetBool("isPickingUp") == false)
        {
            DropItem();
        }

        if (Input.GetAxisRaw("P3GameThrowItem") > 0.1 && anim.GetBool("hasItem") == true && anim.GetBool("isPickingUp") == false)
        {
            ThrowObject();
        }

        if (anim.GetBool("hasItem") == true)
            HoldingItem();


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
        RaycastHit hit;
        Ray ray;
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 4));

        if (gm.players.Count == 3)
            ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 4));
        if (gm.players.Count > 3)
            ray = cam.ScreenPointToRay(new Vector3((Screen.width / 4) * 3, Screen.height / 4));

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.gameObject.tag == "PhysicsObject") //hit.collider.GetComponent<Transform>().parent.tag
            {
                Debug.Log("found an object!");

                anim.SetBool("isPickingUp", true);
                anim.SetBool("hasItem", true);
                Invoke("ResetIsPickingUp", 0.4f);
                //hit.collider.GetComponent<ThrowObjectG>().PickedUp();
                if (hit.collider.GetComponentInParent<Rigidbody>() != null)
                    hit.collider.GetComponentInParent<Rigidbody>().isKinematic = true;
                else if (hit.collider.GetComponent<Rigidbody>() != null)
                    hit.collider.GetComponent<Rigidbody>().isKinematic = true;

                itemPickedUp = hit.collider.gameObject.transform.parent.gameObject;
            }
            else
            {
                Debug.Log("Found a non physics object");
            }
        }
        else
        {
            Debug.Log("Found nothing!");
        }
    }

    private void HoldingItem()
    {
        itemPickedUp.transform.parent = cam.transform;
    }

    private void DropItem()
    {
        anim.SetBool("hasItem", false);

        if (itemPickedUp.GetComponentInParent<Rigidbody>() != null)
            itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
        else if (itemPickedUp.GetComponent<Rigidbody>() != false)
            itemPickedUp.GetComponent<Rigidbody>().isKinematic = false;

        itemPickedUp.transform.parent = null;
        itemPickedUp = null;


    }

    private void ThrowObject()
    {
        anim.SetTrigger("throw");
        anim.SetBool("hasItem", false);

        if (itemPickedUp.GetComponentInParent<Rigidbody>() != null)
        {
            itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            itemPickedUp.transform.parent = null;
            itemPickedUp.GetComponentInParent<Rigidbody>().AddForce(cam.transform.forward * 5000);

        }
        else if (itemPickedUp.GetComponent<Rigidbody>() != false)
        {
            itemPickedUp.GetComponent<Rigidbody>().isKinematic = false;
            itemPickedUp.transform.parent = null;
            itemPickedUp.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 5000);

        }

        itemPickedUp = null;

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