using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{

    public int objectHealth;
    public int objectDmg;
    public int playerNumThrown;
    public int totalDmg;
    public Rigidbody rb;

    public ScoreManager playerScore;
    

    //needed to stop multiple collisions
    private bool hasHit;
    static float hitDelay = 0.4f;

    void Start()
    {
        playerScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (playerNumThrown)
            {
                case 1:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 2":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 2");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth -= totalDmg;
                                hasHit = true;
                                StartCoroutine("resetHit");
                                Debug.Log(playerNumThrown);
                                if (collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth == 0)
                                {
                                    playerScore.player1Score += 1;
                                    Debug.Log(playerScore.player1Score);
                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                               
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 4":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 4");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth -= totalDmg;
                               
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
                case 2:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 1":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 2");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                               
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 4":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 4");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                               
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
                case 3:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 2":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 1");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 2");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                               
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 4":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 4");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
                case 4:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 1":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 1");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 2":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
            }

        }


    }

    IEnumerator resetHit()
    {
        yield return new WaitForSeconds(hitDelay);
        hasHit = false;
    }



}

