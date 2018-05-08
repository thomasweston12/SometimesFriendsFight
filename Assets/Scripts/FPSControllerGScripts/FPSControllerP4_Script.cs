using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerP4_Script : MonoBehaviour
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
    public AudioSource playerSounds;

    public AudioClip footstepSound;
    public AudioClip pickupSound;
    public AudioClip dropSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip throwSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    private SphereCollider leftFoot;
    private SphereCollider rightFoot;
    private Component[] feet;


    public float rbOfObject;
    public float playerStrengthValue;
    public float throwForce;

    public int score;
    public int rageValue;
    public bool rageModeOn;

    // Use this for initialization
    void Start()
    {
        playerSounds = this.gameObject.GetComponent<AudioSource>();

        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        Invoke("ResetIsJumping", 0);
        Invoke("ResetIsPickingUp", 0);
        speed = defaultSpeed;
        cam = GetComponentInChildren<Camera>();

        playerHealth = gm.players[3].getMaxHealth();
        playerStrengthValue = 500.0f;
        rageModeOn = false;
        rageValue = 0;

        InvokeRepeating("RageDegrade", 1.0f, 15.0f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (rageValue >= 100)
        {
            Debug.Log(rageValue);
            playerStrengthValue = 1000.0f;
            rageModeOn = true;
            StartCoroutine(ResetRage());
        }

        if (isTimerRunning == true)
        {
            PickupTimer();
        }

        float ForwardBack = Input.GetAxis("P4GameForwardsBackwards") * speed;
        float strafe = Input.GetAxis("P4GameStrafeRightLeft") * speed;
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

        if (Input.GetButton("P4GameJump") && anim.GetBool("isJumping") == false)
        {
            playerSounds.clip = jumpSound;
            playerSounds.PlayOneShot(jumpSound);

            rb.AddForce(0.0f, 600.0f, 0.0f, ForceMode.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", true);
            Invoke("ResetIsJumping", 1.0f);

        }

        if (Input.GetButton("P4GamePickup") && anim.GetBool("isPickingUp") == false && anim.GetBool("isJumping") == false && anim.GetBool("hasItem") == false)
        {
            PickUpItem();
        }

        if (Input.GetButton("P4GameDropItem") && anim.GetBool("isPickingUp") == false)
        {
            DropItem();
        }

        if (Input.GetAxisRaw("P4GameThrowItem") > 0.1 && anim.GetBool("hasItem") == true && anim.GetBool("isPickingUp") == false)
        {
            ThrowObject();
        }

        if (anim.GetBool("hasItem") == true)
            HoldingItem();

    }

    private void ResetIsJumping()
    {
        playerSounds.clip = landSound;
        playerSounds.PlayOneShot(landSound);

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

        if (gm.players.Count == 4)
            ray = cam.ScreenPointToRay(new Vector3((Screen.width / 4) * 3, Screen.height / 4));

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.gameObject.tag == "PhysicsObject") //hit.collider.GetComponent<Transform>().parent.tag
            {
                Debug.Log("found an object!");
                if (hit.collider.gameObject.GetComponentInParent<Rigidbody>().mass < 100)
                {
                    playerSounds.clip = pickupSound;
                    playerSounds.PlayOneShot(pickupSound);

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
                else if (hit.collider.GetComponentInParent<Rigidbody>().mass >= 100 && rageModeOn == true)
                {
                    playerSounds.clip = pickupSound;
                    playerSounds.PlayOneShot(pickupSound);

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
        {
            itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            playerSounds.clip = dropSound;
            playerSounds.PlayOneShot(dropSound);

        }
        else if (itemPickedUp.GetComponent<Rigidbody>() != false)
        {
            itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            playerSounds.clip = dropSound;
            playerSounds.PlayOneShot(dropSound);
            itemPickedUp.GetComponent<Rigidbody>().isKinematic = false;

        }

    }

    private void ThrowObject()
    {
        anim.SetTrigger("throw");
        anim.SetBool("hasItem", false);
        playerSounds.clip = throwSound;
        playerSounds.PlayOneShot(throwSound);

        if (itemPickedUp.GetComponentInParent<Rigidbody>() != null)
        {
            itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            itemPickedUp.transform.parent = null;
            rbOfObject = itemPickedUp.GetComponentInParent<Rigidbody>().mass;
            throwForce = rbOfObject * playerStrengthValue;
            itemPickedUp.GetComponentInParent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
            itemPickedUp.GetComponentInParent<objectManager>().playerNumThrown = 4;
        }
        else if (itemPickedUp.GetComponent<Rigidbody>() != false)
        {
            itemPickedUp.GetComponent<Rigidbody>().isKinematic = false;
            itemPickedUp.transform.parent = null;
            rbOfObject = itemPickedUp.GetComponentInParent<Rigidbody>().mass;
            throwForce = rbOfObject * playerStrengthValue;
            itemPickedUp.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
            itemPickedUp.GetComponentInParent<objectManager>().playerNumThrown = 4;
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

    void RageDegrade()
    {
        rageValue -= 5;
    }

    IEnumerator ResetRage()
    {
        yield return new WaitForSeconds(15);
        rageValue = 0;
        rageModeOn = false;
        playerStrengthValue = 500.0f;
        Debug.Log("Rage over");
    }

    private void OnEnable()
    {
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
        this.playerHealth = gm.players[3].getMaxHealth();
        Debug.Log(this.playerHealth);
    }

}