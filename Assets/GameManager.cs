using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// PlayerManager class. This class handles all of the information about a player when they are created.
// PlayerNumber denotes where on the split-screen this player should be. Max health shows how much health
// this player should start with. Useful if a "handicap" system were put in place or for players to create
// their own style of game. Same goes for damageModifier, this is the variable all damage given is multiplied by,
// This has been placed into the game ready for the "rage meter" (working title) feature.

public class PlayerManager {
    private int playerNumber = 0;
    private int maxHealth = 100;
    private int damageModifier = 1;
    private int currentScore = 0;
    private string playerName = "tERRance";
    private Color playerColour = new Color(1, 1, 1);

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

    public void SetPlayerColour(Color newColour)
    {
        this.playerColour = newColour;
    }

    public Color GetPlayerColour()
    {
        return this.playerColour;
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

    public TextMeshProUGUI p1MaxHealthLabel;
    public TextMeshProUGUI p2MaxHealthLabel;
    public TextMeshProUGUI p3MaxHealthLabel;
    public TextMeshProUGUI p4MaxHealthLabel;

    public Scene testScene;
    private Scene currentScene;

    public Dropdown mapDropdown;
    public Dropdown timerDropdown;
    public Dropdown scoreDropdown;

    public Button startButton;

    public Button playGameButton;
    public Button settingsButton;
    public Button creditsButton;

    public Button p1JoinButton;
    public Button p2JoinButton;
    public Button p3JoinButton;
    public Button p4JoinButton;

    public Slider p1ColourSlider;
    public Slider p2ColourSlider;
    public Slider p3ColourSlider;
    public Slider p4ColourSlider;

    public Slider p1MaxHealthSlider;
    public Slider p2MaxHealthSlider;
    public Slider p3MaxHealthSlider;
    public Slider p4MaxHealthSlider;

    public UnityEngine.UI.Image p1SliderHandle;
    public UnityEngine.UI.Image p2SliderHandle;
    public UnityEngine.UI.Image p3SliderHandle;
    public UnityEngine.UI.Image p4SliderHandle;


    public CanvasGroup playGameMenuGroup;
    public CanvasGroup mainMenuMenuGroup;
    public CanvasGroup p1ControlsGroup;
    public CanvasGroup p2ControlsGroup;
    public CanvasGroup p3ControlsGroup;
    public CanvasGroup p4ControlsGroup;

    public enum States {MainMenu, PlayGameMenu, SettingsMenu, CreditsMenu, P1Joined, P2Joined, P3Joined, P4Joined, GameStart, GameEnd }
    States currentState = States.MainMenu;

	// Use this for initialization
    void Start ()
    {
        if (currentState != States.MainMenu)
            currentState = States.MainMenu;
        mainMenuMenuGroup = GameObject.Find("MainMenuScreen").GetComponent<CanvasGroup>();
        playGameMenuGroup = GameObject.Find("PlayGameScreen").GetComponent<CanvasGroup>();
        p1ControlsGroup = GameObject.Find("P1Controls").GetComponent<CanvasGroup>();
        p2ControlsGroup = GameObject.Find("P2Controls").GetComponent<CanvasGroup>();
        p3ControlsGroup = GameObject.Find("P3Controls").GetComponent<CanvasGroup>();
        p4ControlsGroup = GameObject.Find("P4Controls").GetComponent<CanvasGroup>();
        playGameMenuGroup.alpha = 0;
        p1ControlsGroup.alpha = 0;
        p2ControlsGroup.alpha = 0;
        p3ControlsGroup.alpha = 0;
        p4ControlsGroup.alpha = 0;

        currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;


        Debug.Log(currentState.ToString());
    }

	void Awake () {
        // Don't let this script (inside a game object) get destroyed when other scenes are loaded.
		DontDestroyOnLoad(this.gameObject);
	}
	
    void Update ()
    {

        if (currentScene.name != "mainMenu_Scene")
        {
            switch (players.Count)
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
    public void SetMapChosen(string mapChosen)
    {
        this.mapChosen = mapChosen;
    }

    public string GetMapChosen()
    {
        return this.mapChosen;
    }

    public void setIsGameTimed(bool gameTimed)
    {
        this.isGameTimed = gameTimed;
    }

    public bool GetIsGameTimed()
    {
        return this.isGameTimed;
    }

    public void SetGameTimer(int timer)
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
        Debug.Log(GetMapChosen());
        switch (mapDropdown.value)
        {
            case 0:
                SetMapChosen("Map 1");
                Debug.Log(GetMapChosen());
                break;
            case 1:
                SetMapChosen("Map 2");
                Debug.Log(GetMapChosen());
                break;
            case 2:
                SetMapChosen("Map 3");
                Debug.Log(GetMapChosen());
                break;
            default:
                SetMapChosen("ERROR");
                Debug.Log(GetMapChosen());
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
                SetGameTimer(0);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;
            case 1:
                setIsGameTimed(true);
                SetGameTimer(60);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;
            case 2:
                setIsGameTimed(true);
                SetGameTimer(120);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;
            case 3:
                setIsGameTimed(true);
                SetGameTimer(180);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;
            case 4:
                setIsGameTimed(true);
                SetGameTimer(240);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;
            case 5:
                setIsGameTimed(true);
                SetGameTimer(300);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;
            case 6:
                setIsGameTimed(true);
                SetGameTimer(600);
                Debug.Log(GetIsGameTimed() + ", " + getGameTimer());
                break;

            default:
                setIsGameTimed(true);
                SetGameTimer(0);
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
        if (GetMapChosen() == "Map 1")
            SceneManager.LoadScene("test_level_with_assets", LoadSceneMode.Single);

        if (GetMapChosen() == "Map 2")
            SceneManager.LoadScene("gameManagerTestLevel_Scene", LoadSceneMode.Single);
        
    }

    public void playGameButtonOnClick()
    {
        currentState = States.PlayGameMenu;
        mainMenuMenuGroup.alpha = 0;
        playGameMenuGroup.alpha = 100;
        p1JoinButton.gameObject.SetActive(true);
        p1JoinButton.Select();
    }

    public void settingsButtonOnClick()
    {
        currentState = States.SettingsMenu;
    }

    public void creditsButtonOnClick()
    {
        currentState = States.CreditsMenu;
    }

    public void colourSliderOnValueChanged()
    {
        p1SliderHandle.color = Color.HSVToRGB(p1ColourSlider.value, 1, 1);
        players[0].SetPlayerColour(p1SliderHandle.color);
    }

    public void P2colourSliderOnValueChanged()
    {
        p2SliderHandle.color = Color.HSVToRGB(p2ColourSlider.value, 1, 1);
        players[1].SetPlayerColour(p2SliderHandle.color);
    }

    public void P3colourSliderOnValueChanged()
    {
        p3SliderHandle.color = Color.HSVToRGB(p3ColourSlider.value, 1, 1);
        players[2].SetPlayerColour(p3SliderHandle.color);
    }

    public void P4colourSliderOnValueChanged()
    {
        p4SliderHandle.color = Color.HSVToRGB(p4ColourSlider.value, 1, 1);
        players[3].SetPlayerColour(p4SliderHandle.color);
    }


    public void P1MaxHealthSliderOnValueChanged()
    {
        p1MaxHealthLabel.SetText(p1MaxHealthSlider.value.ToString());
        players[0].setMaxHealth((int)p1MaxHealthSlider.value);
    }

    public void P2MaxHealthSliderOnValueChanged()
    {
        p2MaxHealthLabel.SetText(p2MaxHealthSlider.value.ToString());
        players[1].setMaxHealth((int)p2MaxHealthSlider.value);
    }

    public void P3MaxHealthSliderOnValueChanged()
    {
        p3MaxHealthLabel.SetText(p3MaxHealthSlider.value.ToString());
        players[2].setMaxHealth((int)p3MaxHealthSlider.value);
    }

    public void P4MaxHealthSliderOnValueChanged()
    {
        p4MaxHealthLabel.SetText(p4MaxHealthSlider.value.ToString());
        players[3].setMaxHealth((int)p4MaxHealthSlider.value);
    }

    public void p1JoinButtonOnClick()
    {
        p1JoinButton.gameObject.SetActive(false);
        p1ControlsGroup.alpha = 100;
        p1ControlsGroup.interactable = true;
        AddPlayer();
        p1MaxHealthSlider.Select();
        players[0].setPlayerName("Player 1");
        Debug.Log(players.Count.ToString());
        Debug.Log(currentState.ToString());
    }

    public void p2JoinButtonOnClick()
    {
        p2JoinButton.gameObject.SetActive(false);
        p2ControlsGroup.alpha = 100;
        p2ControlsGroup.interactable = true;
        AddPlayer();
        p2MaxHealthSlider.Select();
        players[1].setPlayerName("Player 2");
        Debug.Log(players.Count.ToString());
        Debug.Log(currentState.ToString());
    }

    public void p3JoinButtonOnClick()
    {
        p3JoinButton.gameObject.SetActive(false);
        p3ControlsGroup.alpha = 100;
        p3ControlsGroup.interactable = true;
        AddPlayer();
        p3MaxHealthSlider.Select();
        players[2].setPlayerName("Player 3");
        Debug.Log(players.Count.ToString());
        Debug.Log(currentState.ToString());
    }

    public void p4JoinButtonOnClick()
    {
        p4JoinButton.gameObject.SetActive(false);
        p4ControlsGroup.alpha = 100;
        p4ControlsGroup.interactable = true;
        AddPlayer();
        p4MaxHealthSlider.Select();
        players[3].setPlayerName("Player 4");
        Debug.Log(players.Count.ToString());
        Debug.Log(currentState.ToString());
    }

    public void doneButtonOnClick()
    {
        switch(currentState)
        {
            case (States.PlayGameMenu):
                currentState = States.P1Joined;
                p1ControlsGroup.interactable = false;
                p2JoinButton.Select();
                break;
            case (States.P1Joined):
                currentState = States.P2Joined;
                p2ControlsGroup.interactable = false;
                p3JoinButton.Select();
                break;
            case (States.P2Joined):
                currentState = States.P3Joined;
                p3ControlsGroup.interactable = false;
                p4JoinButton.Select();
                break;
            case (States.P3Joined):
                currentState = States.P4Joined;
                p4ControlsGroup.interactable = false;
                startButton.Select();

                break;
            default:
                //currentState = States.PlayGameMenu;
                p1JoinButton.Select();
                break;
        }
            
    }
}
