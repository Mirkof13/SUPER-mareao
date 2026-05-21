using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class TimeUpScreen : MonoBehaviour {
	private GameStateManager t_GameStateManager;

	public Text WorldTextHUD;
	public Text ScoreTextHUD;
	public Text CoinTextHUD;

	private float loadScreenDelay = 2;


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		t_GameStateManager = FindObjectOfType<GameStateManager> ();

		string worldName = t_GameStateManager != null ? t_GameStateManager.sceneToLoad : null;
		if (worldName == null && GameManager.Instance != null)
			worldName = GameManager.Instance.World + "-" + GameManager.Instance.Stage;
		if (worldName == null) worldName = "1-1";

		string displayName = worldName.Contains("World ") ? Regex.Split(worldName, "World ")[1] : worldName;
		WorldTextHUD.text = displayName;
		ScoreTextHUD.text = (t_GameStateManager != null ? t_GameStateManager.scores : 0).ToString("D6");
		CoinTextHUD.text = "x" + (t_GameStateManager != null ? t_GameStateManager.coins : 0).ToString("D2");

		StartCoroutine (LoadSceneDelayCo ("LevelStart", loadScreenDelay));
		Debug.Log (this.name + " Start: current scene is " + SceneManager.GetActiveScene ().name);
	}

	IEnumerator LoadSceneDelayCo(string sceneName, float delay = 0) {
		yield return new WaitForSecondsRealtime (delay);
		SceneManager.LoadScene (sceneName);
	}

}
