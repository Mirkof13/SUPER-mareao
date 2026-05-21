using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class LevelStartScreen : MonoBehaviour {
	private GameStateManager t_GameStateManager;
	private float loadScreenDelay = 2;

	public Text WorldTextHUD;
	public Text ScoreTextHUD;
	public Text CoinTextHUD;
	public Text WorldTextMain;
	public Text livesText;

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

		int lives  = t_GameStateManager != null ? t_GameStateManager.lives  : (GameManager.Instance != null ? GameManager.Instance.Lives  : 3);
		int coins  = t_GameStateManager != null ? t_GameStateManager.coins  : (GameManager.Instance != null ? GameManager.Instance.Coins  : 0);
		int scores = t_GameStateManager != null ? t_GameStateManager.scores : 0;
		ScoreTextHUD.text  = scores.ToString ("D6");
		CoinTextHUD.text   = "x" + coins.ToString ("D2");
		WorldTextMain.text = ("WORLD " + displayName).ToUpper ();
		livesText.text     = lives.ToString ();

		StartCoroutine (LoadSceneDelayCo (worldName, loadScreenDelay));

		Debug.Log (this.name + " Start: current scene is " + SceneManager.GetActiveScene ().name);
	}
	
	IEnumerator LoadSceneDelayCo(string sceneName, float delay) {
		yield return new WaitForSecondsRealtime (delay);
		SceneManager.LoadScene (sceneName);
	}
}
