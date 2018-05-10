using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerP1_Script : MonoBehaviour {

    public float defaultSpeed = 0.09f;
    private float speed;
    Animator anim;
    private bool isJumping;
    public Rigidbody rb;
    public Vector3 jumpForce;
    private float waitTime = 0.9f;
    float timer;
    float rageTimer;
    bool isTimerRunning;
    public float dist = 25.0f;
    private bool isNear;
    private Camera cam;
    Vector3 screenCentre = new Vector3(Screen.width / 2, Screen.height / 2);
    public int playerHealth;
    private GameObject currentPlayer;
    private GameObject itemPickedUp;
    private GameManager gm;
	private ScoreManager scoreManager;
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
        this.rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        this.anim = GetComponent<Animator>();
        Invoke("ResetIsJumping", 0);
        Invoke("ResetIsPickingUp", 0);
        this.speed = defaultSpeed;
        this.cam = GetComponentInChildren<Camera>();
		this.playerHealth = gm.players[0].getMaxHealth();
        this.rageModeOn = false;
        //throw stuff -G
        this.playerStrengthValue = 500.0f;
        this.rageValue = 0;

        feet = this.gameObject.GetComponentsInChildren<SphereCollider>();

        for (int i = 0; i < feet.Length; i++)
        {
            if (feet[i].gameObject.name == "foot_L")
            {
                leftFoot = feet[i].GetComponent<SphereCollider>();
            }
            if (feet[i].gameObject.name == "foot_R")
            {
                rightFoot = feet[i].GetComponent<SphereCollider>();
            }
        }

        InvokeRepeating("RageDegrade", 1.0f, 15.0f);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(playerStrengthValue);

        if (this.rageValue >= 100)
        {
            Debug.Log(rageValue);
            this.playerStrengthValue = 1000.0f;
            this.rageModeOn = true;
            StartCoroutine(ResetRage());
        }


        if (this.isTimerRunning == true)
        {
            this.PickupTimer();
        }

        float ForwardBack = Input.GetAxis("P1GameForwardsBackwards") * speed;
        float strafe = Input.GetAxis("P1GameStrafeRightLeft") * speed;
        //ForwardBack *= Time.deltaTime;
        //strafe *= Time.deltaTime;

        if (ForwardBack > speed)
            ForwardBack = speed;

        if (strafe > speed)
            strafe = speed;

        this.transform.Translate(strafe, 0, ForwardBack);

        // Multiplied the second argument as a quick fix to solve the difference between the speeds in the Animator Controller,
        // and the speed of the actual character model. Makes the transition between different animations actually work. - Thomas Weston.

        // Also I messed something up in the animator and I don't know how to fix it, so assume speed is strafe speed and
        // strafeSpeed is forwards and backwards speed, sorry! - Thomas Weston.

        this.anim.SetFloat("speed", strafe * 6);
        this.anim.SetFloat("strafeSpeed", ForwardBack * 6);
        AnimatorStateInfo stateInfo = this.anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButton("P1GameJump") && this.anim.GetBool("isJumping") == false)
        {
            this.playerSounds.clip = jumpSound;
            this.playerSounds.PlayOneShot(jumpSound);
            this.rb.AddForce(0.0f, 600.0f, 0.0f, ForceMode.Impulse);
            this.isJumping = true;
            this.anim.SetBool("isJumping", true);
            Invoke("ResetIsJumping", 1.0f);
            
        }

        if (Input.GetButton("P1GamePickup") && this.anim.GetBool("isPickingUp") == false && this.anim.GetBool("isJumping") == false && anim.GetBool("hasItem") == false)
        {
            this.PickUpItem();
        }

        if (Input.GetButton("P1GameDropItem") && this.anim.GetBool("isPickingUp") == false)
        {

            this.DropItem();
        }

        if (Input.GetAxisRaw("P1GameThrowItem") > 0.1 && this.anim.GetBool("hasItem") == true && this.anim.GetBool("isPickingUp") == false)
        {
            this.ThrowObject();
        }


        //if (Input.GetKeyDown("escape"))
        //    Cursor.lockState = CursorLockMode.None;

        if (this.anim.GetBool("hasItem") == true)
            this.HoldingItem();

    }

    private void ResetIsJumping()
    {
        this.playerSounds.clip = landSound;
		this.playerSounds.PlayOneShot(landSound);


        this.anim.SetBool("isJumping", false);
    }

    private void ResetIsPickingUp()
    {
        this.anim.SetBool("isPickingUp", false);


    }

    private void PickUpItem()
    {
        RaycastHit hit;
        Ray ray = ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (gm.players.Count == 1)
            ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        else if (gm.players.Count == 2)
            ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, (Screen.height / 4)* 3));
        else if (gm.players.Count > 2)
            ray = cam.ScreenPointToRay(new Vector3(Screen.width / 4, (Screen.height / 4) * 3));


        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 20, true);
        //Debug.Log("drawing ray!");

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.gameObject.tag == "PhysicsObject") //hit.collider.GetComponent<Transform>().parent.tag
            {
                Debug.Log("found an object!");
                if (hit.collider.gameObject.GetComponentInParent<Rigidbody>().mass < 100)
                {
                    this.playerSounds.clip = pickupSound;
                    this.playerSounds.PlayOneShot(pickupSound);

                    this.anim.SetBool("isPickingUp", true);
                    this.anim.SetBool("hasItem", true);
                    Invoke("ResetIsPickingUp", 0.4f);
                    //hit.collider.GetComponent<ThrowObjectG>().PickedUp();
                    if (hit.collider.GetComponentInParent<Rigidbody>() != null)
                        hit.collider.GetComponentInParent<Rigidbody>().isKinematic = true;
                    else if (hit.collider.GetComponent<Rigidbody>() != null)
                        hit.collider.GetComponent<Rigidbody>().isKinematic = true;

                    this.itemPickedUp = hit.collider.gameObject.transform.parent.gameObject;
                }
                else if (hit.collider.GetComponentInParent<Rigidbody>().mass >= 100 && this.rageModeOn == true)
                {
                    this.playerSounds.clip = pickupSound;
                    this.playerSounds.PlayOneShot(pickupSound);

                    this.anim.SetBool("isPickingUp", true);
                    this.anim.SetBool("hasItem", true);
                    Invoke("ResetIsPickingUp", 0.4f);
                    //hit.collider.GetComponent<ThrowObjectG>().PickedUp();
                    if (hit.collider.GetComponentInParent<Rigidbody>() != null)
                        hit.collider.GetComponentInParent<Rigidbody>().isKinematic = true;
                    else if (hit.collider.GetComponent<Rigidbody>() != null)
                        hit.collider.GetComponent<Rigidbody>().isKinematic = true;

                    this.itemPickedUp = hit.collider.gameObject.transform.parent.gameObject;
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
        this.itemPickedUp.transform.parent = cam.transform;
    }

    private void DropItem()
    {
        this.anim.SetBool("hasItem", false);

        if (this.itemPickedUp.GetComponentInParent<Rigidbody>() != null)
        {
            this.itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            this.playerSounds.clip = dropSound;
            this.playerSounds.PlayOneShot(dropSound);

        }
        else if (this.itemPickedUp.GetComponent<Rigidbody>() != false)
        {
            this.itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            this.playerSounds.clip = dropSound;
            this.playerSounds.PlayOneShot(dropSound);
            this.itemPickedUp.GetComponent<Rigidbody>().isKinematic = false;

        }

        this.itemPickedUp.transform.parent = null;
        this.itemPickedUp = null;
    }

    private void ThrowObject()
    {
        this.anim.SetTrigger("throw");
        this.anim.SetBool("hasItem", false);

        this.playerSounds.clip = throwSound;
        this.playerSounds.PlayOneShot(throwSound);

        if (this.itemPickedUp.GetComponentInParent<Rigidbody>() != null)
        {
            this.itemPickedUp.GetComponentInParent<Rigidbody>().isKinematic = false;
            this.itemPickedUp.transform.parent = null;
            this.rbOfObject = itemPickedUp.GetComponentInParent<Rigidbody>().mass;
            this.throwForce = rbOfObject * playerStrengthValue;
            this.itemPickedUp.GetComponentInParent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
            this.itemPickedUp.GetComponentInParent<objectManager>().playerNumThrown = 1;
         
        }
        else if (this.itemPickedUp.GetComponent<Rigidbody>() != false)
        {
            this.itemPickedUp.GetComponent<Rigidbody>().isKinematic = false;
            this.itemPickedUp.transform.parent = null;
            this.rbOfObject = itemPickedUp.GetComponentInParent<Rigidbody>().mass;
            this.throwForce = rbOfObject * playerStrengthValue;
            this.itemPickedUp.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
            this.itemPickedUp.GetComponentInParent<objectManager>().playerNumThrown = 1;

        }

        this.itemPickedUp = null;

    }
     
    void PickupTimer()
    {
        this.timer += Time.deltaTime;
        if (timer > waitTime)
        {

            this.timer = 0f;
            this.speed = defaultSpeed;
            this.anim.SetBool("isPickingUp", false);
            this.isTimerRunning = false;
        }


    }

    void RageDegrade()
    {
        this.rageValue -= 5;
    }

    IEnumerator ResetRage()
    {
        yield return new WaitForSeconds(15);
        this.rageValue = 0;
        this.rageModeOn = false;
        this.playerStrengthValue = 500.0f;
        //Debug.Log("Rage over");
    }

    private void OnEnable()
    {
		// Magic score manager - tom
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
        this.playerHealth = gm.players[0].getMaxHealth();
        Debug.Log(this.playerHealth);
    }
}