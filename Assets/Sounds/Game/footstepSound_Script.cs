using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSound_Script : MonoBehaviour {
    SphereCollider footCollider;
    AudioSource footStepSounds;
    public AudioClip footsteps;
    public float stepInterval = 0.25f;
    private float nextStep = 0f;

    // Use this for initialization
    void Start () {
        footStepSounds = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (Time.time > nextStep)
        {
            nextStep = Time.time + stepInterval;

            footStepSounds.clip = footsteps;
            footStepSounds.pitch = Random.Range(0.8f, 1f);
            footStepSounds.volume = Random.Range(0.2f, 0.4f);

            footStepSounds.PlayOneShot(footsteps);
            //Debug.Log("Footstep!");

        }

    }

}
