using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRemover : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        GameManager gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
       
        switch (gm.players.Count)
        {
            case 1:
                Destroy(GameObject.Find("Player 2"));
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                Debug.Log("Destroying 2, 3, 4!");
                break;
            case 2:
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                Debug.Log("Destroying 3, 4!");
                break;
            case 3:
                Destroy(GameObject.Find("Player 4"));
                Debug.Log("Destroying 4!");
                break;
            default:
                Debug.Log("Destroying none because this is an error!");
                break;
        
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
