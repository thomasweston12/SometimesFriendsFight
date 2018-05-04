using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class screenSplitter : MonoBehaviour {

	private Camera pCam1, pCam2, pCam3, pCam4;


	// Use this for initialization
	void Start () {


		GameManager gm = GameObject.Find ("GameManagerObject").GetComponent<GameManager> ();
		pCam1 = GameObject.Find ("playerCharacterCamera1").GetComponent<Camera> ();
		pCam2 = GameObject.Find ("playerCharacterCamera2").GetComponent<Camera> ();
		pCam3 = GameObject.Find ("playerCharacterCamera3").GetComponent<Camera> ();
		pCam4 = GameObject.Find ("playerCharacterCamera4").GetComponent<Camera> ();
        //playerCharacterCamera = GameObject.Find ("platerCharacterCamera").getComponent<playerCharacterCamera> ();

        //cam = Camera.playerCharacterCamera;
        //int numbPlayers = gm.players.Count ();
	
		if (gm.players.Count == 1) {

			Debug.Log ("found playerCamera 1");
			pCam1.rect = new Rect (0, 0, 1, 1);				
		}		
		else if (gm.players.Count  == 2){
			
			Debug.Log ("found playerCamera 1");
			Debug.Log ("found playerCamera 2");
			pCam1.rect = new Rect (0, 0.5f, 1, 1);
			pCam2.rect = new Rect (0, 0, 1, 0.5f);
        }

        else if (gm.players.Count  == 3){

			Debug.Log ("found playerCamera 1");
			Debug.Log ("found playerCamera 2");
			Debug.Log ("found playerCamera 3");
			pCam1.rect = new Rect (0, 0.5f, 0.5f, 1);
			pCam2.rect = new Rect (0.5f, 0.5f, 1, 1);
			pCam3.rect = new Rect (0, 0, 1, 0.5f);

		}

		else if (gm.players.Count  == 4){

			Debug.Log ("found playerCamera 1");
			Debug.Log ("found playerCamera 2");
			Debug.Log ("found playerCamera 3");
			Debug.Log ("found playerCamera 4");
			pCam1.rect = new Rect (0, 0.5f, 0.5f, 1);
			pCam2.rect = new Rect (0.5f, 0.5f, 1, 1);
			pCam3.rect = new Rect (0, 0, 0.5f, 0.5f);
			pCam4.rect = new Rect (0.5f, 0, 0.5f, 0.5f);

		}


			
				

		   


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
