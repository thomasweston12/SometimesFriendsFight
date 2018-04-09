using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// PlayerManager class. This class handles all of the information about a player when they are created.
// PlayerNumber denotes where on the split-screen this player should be. Max health shows how much health
// this player should start with. Useful if a "handicap" system were put in place or for players to create
// their own style of game. Same goes for damageModifier, this is the variable all damage given is multiplied by,
// This has been placed into the game ready for the "rage meter" (working title) feature.

public class PlayerManager : MonoBehaviour {
	private int playerNumber = 0;
	private int maxHealth = 100;
	private int damageModifier = 1;
    private int currentScore = 0;
    private string playerName = "tERRance";

    public PlayerManager(int playerNo, int newMaxHealth, int damageMod)
    {
        this.playerNumber = playerNo;
        this.maxHealth = newMaxHealth;
        this.damageModifier = damageMod;
    }

    public void setPlayerNumber(int playerNo)
    {
        this.playerNumber = playerNo;
    }

    public int getPlayerNumber()
    {
        return this.playerNumber;
    }

    public void setMaxHealth(int newMaxHealth)
    {
        this.maxHealth = newMaxHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setPlayerScore(int score)
    {
        this.currentScore += score;
    }

    public int getPlayerScore()
    {
        return this.currentScore;
    }

    public void resetScore()
    {
        this.currentScore = 0;
    }

    public void setPlayerName(string newName)
    {
        this.playerName = newName;
    }

    public string getPlayerName()
    {
        return playerName;
    }
}


// Game Manager class. This is attached to a game object which never unloads, even when the scene it is attached to
// is switched. This class handles all game functionality for setting up a game, starting the game, and ending the game 
// appropriately. This class also handles adding players to the game up to a maximum of four players split-screen.

public class GameManager : MonoBehaviour {

	public List<PlayerManager> players = new List<PlayerManager>();

    private int numberOfPlayers = 0;
	private string mapChosen = "Map 1";
    private bool isGameTimed = true;
    private int gameTimer = 120;
    private bool isScoreLimited = false;
    private int scoreLimit = 0;
    public Dropdown mapDropdown;
    public Dropdown timerDropdown;
    public Dropdown scoreDropdown;
    public Button startButton;


	// Use this for initialization
    void Start ()
    {
    }

	void Awake () {
        // Don't let this script (inside a game object) get destroyed when other scenes are loaded.
		DontDestroyOnLoad(this.gameObject);
	}
	
    void Update ()
    {
        // Place here the functionality for each player adding themselves to the game.
        // Make X button on controller switch player 1 to changing game settings.
    }

    // Used to decide how many players to add to the game when starting the game.
	public void AddPlayer() {
        players.Add(new PlayerManager(players.Count, 100, 1));  
	}

    // Removes the last player who joined the game.
    public void RemoveLastPlayer()
    {
        players.RemoveAt(players.Count - 1);
    }

    // Changes the map for the game.
    public void setMapChosen(string mapChosen)
    {
        this.mapChosen = mapChosen;
    }

    public string getMapChosen()
    {
        return this.mapChosen;
    }

    public void setIsGameTimed(bool gameTimed)
    {
        this.isGameTimed = gameTimed;
    }

    public bool getIsGameTimed()
    {
        return this.isGameTimed;
    }

    public void setGameTimer(int timer)
    {
        this.gameTimer = timer;
    }

    public int getGameTimer()
    {
        return this.gameTimer;
    }
    
    public void setIsScoreLimited(bool scoreLimited)
    {
        this.isScoreLimited = scoreLimited;
    }

    public bool getIsScoreLimited()
    {
        return this.isScoreLimited;
    }

    public void setScoreLimit(int scoreLimit)
    {
        this.scoreLimit = scoreLimit;
    }

    public int getScoreLimit()
    {
        return this.scoreLimit;
    }

    // Events for dropdown menu items being changed
    public void mapDropDownIndexChanged()
    {
        Debug.Log(getMapChosen());
        switch (mapDropdown.value)
        {
            case 0:
                setMapChosen("Map 1");
                Debug.Log(getMapChosen());
                break;
            case 1:
                setMapChosen("Map 2");
                Debug.Log(getMapChosen());
                break;
            case 2:
                setMapChosen("Map 3");
                Debug.Log(getMapChosen());
                break;
            default:
                setMapChosen("ERROR");
                Debug.Log(getMapChosen());
                break;
        }
    }

    public void timerDropDownIndexChanged()
    {
        Debug.Log(getGameTimer());
        switch (timerDropdown.value)
        {
            case 0:
                setIsGameTimed(false);
                setGameTimer(0);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;
            case 1:
                setIsGameTimed(true);
                setGameTimer(60);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;
            case 2:
                setIsGameTimed(true);
                setGameTimer(120);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;
            case 3:
                setIsGameTimed(true);
                setGameTimer(180);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;
            case 4:
                setIsGameTimed(true);
                setGameTimer(240);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;
            case 5:
                setIsGameTimed(true);
                setGameTimer(300);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;
            case 6:
                setIsGameTimed(true);
                setGameTimer(600);
                Debug.Log(getIsGameTimed() + ", " + getGameTimer());
                break;

            default:
                setIsGameTimed(true);
                setGameTimer(0);
                Debug.Log("ERROR");
                break;
        }
    }

    public void scoreDropDownIndexChanged()
    {
        Debug.Log(getIsScoreLimited());
        Debug.Log(getScoreLimit());
        switch (scoreDropdown.value)
        {
            case 0:
                setIsScoreLimited(false);
                setScoreLimit(0);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;
            case 1:
                setIsScoreLimited(true);
                setScoreLimit(100);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;
            case 2:
                setIsScoreLimited(true);
                setScoreLimit(200);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;
            case 3:
                setIsScoreLimited(true);
                setScoreLimit(500);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;
            case 4:
                setIsScoreLimited(true);
                setScoreLimit(1000);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;
            case 5:
                setIsScoreLimited(true);
                setScoreLimit(2000);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;
            case 6:
                setIsScoreLimited(true);
                setScoreLimit(5000);
                Debug.Log(getIsScoreLimited() + ", " + getScoreLimit());
                break;

            default:
                setIsScoreLimited(true);
                setScoreLimit(0);
                Debug.Log("ERROR");
                break;
        }

    }

    public void startButtonOnClick()
    {
        if (getMapChosen() == "Map 1")
        {
            SceneManager.LoadScene("test_level_with_assets", LoadSceneMode.Single);
        }
    }

}
