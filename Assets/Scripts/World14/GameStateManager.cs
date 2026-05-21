using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStateManager : MonoBehaviour {
	public bool spawnFromPoint;
	public int spawnPointIdx;
	public int spawnPipeIdx;

	public int marioSize;
	public int lives;
	public int coins;
	public int scores;
	public float timeLeft;
	public bool hurryUp;

	public string sceneToLoad; // what scene to load after level start screen finishes?
	public bool timeup;

	void Awake () {
		if (FindObjectsOfType (GetType ()).Length == 1) {
			DontDestroyOnLoad (gameObject);
			ConfigNewGame ();
			if (GameManager.Instance != null) {
				lives = GameManager.Instance.Lives;
				coins = GameManager.Instance.Coins;
			}
		} else {
			// Find the persistent instance (not this one) and reset it for the new level
			foreach (var gsm in FindObjectsOfType<GameStateManager>()) {
				if (gsm != this) {
					gsm.ConfigNewLevel();
					if (GameManager.Instance != null) {
						gsm.lives = GameManager.Instance.Lives;
						gsm.coins = GameManager.Instance.Coins;
					}
					break;
				}
			}
			Destroy (gameObject);
		}
	}
	
	public void ResetSpawnPosition() {
		spawnFromPoint = true;
		spawnPointIdx = 0;
		spawnPipeIdx = 0;
	}

	public void SetSpawnPipe(int idx) {
		spawnFromPoint = false;
		spawnPipeIdx = idx;
	}

	public void ConfigNewGame() {
		marioSize = 0;
		lives = 3;
		coins = 0;
		scores = 0;
		timeLeft = 400.5f;
		hurryUp = false;
		ResetSpawnPosition ();
		sceneToLoad = null;
		timeup = false;
	}

	public void ConfigNewLevel() {
		timeLeft = 400.5f;
		hurryUp = false;
		ResetSpawnPosition ();
	}

	public void ConfigReplayedLevel() { // e.g. Mario respawns
		timeLeft = 400.5f;
		hurryUp = false;
	}

	public void SaveGameState() {
		LevelManager t_LevelManager = FindObjectOfType<LevelManager> ();
		marioSize = t_LevelManager.marioSize;
		lives = t_LevelManager.lives;
		coins = t_LevelManager.coins;
		scores = t_LevelManager.scores;
		timeLeft = t_LevelManager.timeLeft;
		hurryUp = t_LevelManager.hurryUp;
	}

}
