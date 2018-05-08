using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRemover : MonoBehaviour {

    GameManager gm;

    public Material player1Material;
    public Material player2Material;
    public Material player3Material;
    public Material player4Material;

    public TextMeshProUGUI p1Score;
    public TextMeshProUGUI p2Score;
    public TextMeshProUGUI p3Score;
    public TextMeshProUGUI p4Score;

    Color p1Color;
    Color p2Color;
    Color p3Color;
    Color p4Color;

	// Use this for initialization
	void Awake () {

        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
        p1Score = GameObject.Find("P1Score").GetComponent<TextMeshProUGUI>();
        p2Score = GameObject.Find("P2Score").GetComponent<TextMeshProUGUI>();
        p3Score = GameObject.Find("P3Score").GetComponent<TextMeshProUGUI>();
        p4Score = GameObject.Find("P4Score").GetComponent<TextMeshProUGUI>();


        p1Color = new Color(1, 1, 1);
        p2Color = new Color(1, 1, 1);
        p3Color = new Color(1, 1, 1);
        p4Color = new Color(1, 1, 1);
       
        switch (gm.players.Count)
        {
            case 1:
                Destroy(GameObject.Find("Player 2"));
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                Debug.Log("Destroying 2, 3, 4!");

                p1Color = gm.players[0].GetPlayerColour();
                player1Material.color = p1Color;

                p2Score.gameObject.SetActive(false);
                p3Score.gameObject.SetActive(false);
                p4Score.gameObject.SetActive(false);


                break;
            case 2:
                Destroy(GameObject.Find("Player 3"));
                Destroy(GameObject.Find("Player 4"));
                Debug.Log("Destroying 3, 4!");

                p1Color = gm.players[0].GetPlayerColour();
                player1Material.color = p1Color;

                p2Color = gm.players[1].GetPlayerColour();
                player2Material.color = p2Color;

                p3Score.gameObject.SetActive(false);
                p4Score.gameObject.SetActive(false);


                break;
            case 3:
                Destroy(GameObject.Find("Player 4"));
                Debug.Log("Destroying 4!");

                p1Color = gm.players[0].GetPlayerColour();
                player1Material.color = p1Color;
                
                p2Color = gm.players[1].GetPlayerColour();
                player2Material.color = p2Color;
                
                p3Color = gm.players[2].GetPlayerColour();
                player3Material.color = p3Color;

                p4Score.gameObject.SetActive(false);


                break;
            case 4:
                p1Color = gm.players[0].GetPlayerColour();
                player1Material.color = p1Color;
                
                p2Color = gm.players[1].GetPlayerColour();
                player2Material.color = p2Color;
                
                p3Color = gm.players[2].GetPlayerColour();
                player3Material.color = p3Color;
                
                p4Color = gm.players[3].GetPlayerColour();
                player4Material.color = p4Color;

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
