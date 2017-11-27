using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectG : MonoBehaviour {

    public Transform player;
    public Transform playerCam;
    public float throwforce = 800;
    bool hasPlayer = false;
    bool beingCarried = false;
    public int dmg;
    private bool touched = false;
	
	// Update is called once per frame
	void Update ()
    {

        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if(touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if(Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwforce);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
	}


    void OnTriggerEnter()
    {
        if(beingCarried)
        {
            touched = true;
        }
    }
}
