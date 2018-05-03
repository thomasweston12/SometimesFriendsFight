using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour {

    public float objectHealth;
    public float objectDmg;
    public int playerNumThrown;
    public GameObject playerHit;
    public float totalDmg;
    public Rigidbody rb;
    public float playerHealth; 
    

	// Use this for initialization
	void Start () {
        
	}

    void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(playerNumThrown);
            playerHit = collision.gameObject;
            switch (playerNumThrown)
            {
                case 1:
                    switch (playerHit.GetComponent<PlayerManager>().getPlayerNumber())
                    {
                        case 2:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP2_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 3:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP3_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 4:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP4_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                    }
                    break;
                case 2:
                    switch (playerHit.GetComponent<PlayerManager>().getPlayerNumber())
                    {
                        case 1:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP1_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 3:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP3_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 4:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP4_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                    }
                    break;
                case 3:
                    switch (playerHit.GetComponent<PlayerManager>().getPlayerNumber())
                    {
                        case 1:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP1_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 2:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP2_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 4:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP4_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                    }
                    break;
                case 4:
                    switch (playerHit.GetComponent<PlayerManager>().getPlayerNumber())
                    {
                        case 1:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP1_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 2:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP2_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                        case 3:
                            totalDmg = rb.velocity.z / objectDmg;
                            playerHealth = playerHit.GetComponent<FPSControllerP3_Script>().playerHealth;
                            playerHealth = playerHealth - totalDmg;
                            break;
                    }
                    break;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
