using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pauseMenu_Script : MonoBehaviour {

    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public EventSystem pauseEventSystem;

    private void Awake()
    {
        Resume();
    }

    private void Start()
    {
        //resumeButton = GameObject.Find("btnResume").GetComponent<Button>();
        //pauseEventSystem.SetSelectedGameObject(resumeButton);
        
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetButtonDown("P1GamePauseButton"))
        {
            if (gamePaused)
            {
                Resume();
            }
            else if (!gamePaused)
            {
                Pause();
            }
        }
	}

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        resumeButton.Select();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeButtonOnClick()
    {
        this.Resume();
    }

    public void QuitGameButtonOnClick()
    {
        this.Resume();
        SceneManager.LoadScene("mainMenu_Scene", LoadSceneMode.Single);
    }
    
}
