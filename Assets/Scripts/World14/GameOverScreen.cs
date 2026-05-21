using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class GameOverScreen : MonoBehaviour {
	private GameStateManager t_GameStateManager;

	public Text WorldTextHUD;
	public Text ScoreTextHUD;
	public Text CoinTextHUD;
	public Text MessageText;

	public AudioSource gameOverMusicSource;


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		t_GameStateManager = FindObjectOfType<GameStateManager> ();

		string worldName = t_GameStateManager != null ? t_GameStateManager.sceneToLoad : null;
		if (worldName == null && GameManager.Instance != null)
			worldName = GameManager.Instance.World + "-" + GameManager.Instance.Stage;
		if (worldName == null) worldName = "1-1";

		string displayName = worldName.Contains("World ") ? Regex.Split (worldName, "World ")[1] : worldName;
		WorldTextHUD.text = displayName;

		int coins  = t_GameStateManager != null ? t_GameStateManager.coins  : (GameManager.Instance != null ? GameManager.Instance.Coins  : 0);
		int scores = t_GameStateManager != null ? t_GameStateManager.scores : 0;
		ScoreTextHUD.text = scores.ToString ("D6");
		CoinTextHUD.text  = "x" + coins.ToString ("D2");

		bool timeup = t_GameStateManager != null && t_GameStateManager.timeup;
		if (!timeup) {
			MessageText.text = "GAME OVER";
		} else {
			StartCoroutine (ChangeMessageCo ());
		}

		gameOverMusicSource.volume = PlayerPrefs.GetFloat("musicVolume", 1f);
		if (gameOverMusicSource.clip != null) {
			gameOverMusicSource.Play ();
			LoadMainMenu (gameOverMusicSource.clip.length);
		} else {
			LoadMainMenu (3f);
		}

		Debug.Log (this.name + " Start: current scene is " + SceneManager.GetActiveScene ().name);
	}

	IEnumerator LoadSceneDelayCo(string sceneName, float delay = 0) {
		yield return new WaitForSecondsRealtime (delay);
		SceneManager.LoadScene (sceneName);
	}

	IEnumerator ChangeMessageCo() { // TIME UP to GAME OVER
		MessageText.text = "TIME UP";
		yield return new WaitForSecondsRealtime (1f);
		MessageText.text = "GAME OVER";
	}

	void Update() {
		if (Input.GetButton("Pause")) {
			LoadMainMenu ();
		}
	}

	void LoadMainMenu(float delay = 0) {
		StartCoroutine (LoadSceneDelayCo ("Title", delay));
	}
}
