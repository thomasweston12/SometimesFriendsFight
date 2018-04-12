using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playGameButton_Script : MonoBehaviour {
	private GameObject playButton;
	private GameObject optionsButton;
	private GameObject creditsButton;

	public void playButton_onClick () {
		playButton = GameObject.Find ("PlayGameButton");
		optionsButton = GameObject.Find ("SettingsButton");
		creditsButton = GameObject.Find ("CreditsButton");

		playButton.SetActive (false);
		optionsButton.SetActive (false);
		creditsButton.SetActive (false);
	}
}
