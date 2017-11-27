using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerG : MonoBehaviour {

    public float speed = 2.0f;

	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

	}
	
	// Update is called once per frame
	void Update () {

        float ForwardBack = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        ForwardBack *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, ForwardBack);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;		
	}
}
