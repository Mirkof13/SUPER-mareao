using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Global UI manager singleton.
/// Updates counters (coins, world/stage, lives) and handles pause text visibility.
/// </summary>
public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Displays the number of collected coins.")]
    public TextMeshProUGUI coinsCounter;

    [Tooltip("Displays the current world and stage.")]
    public TextMeshProUGUI worldStage;

    [Tooltip("Displays the remaining lives.")]
    public TextMeshProUGUI livesCounter;

    [Header("Pause UI")]
    [SerializeField] private GameObject pauseText;

    #region Unity Lifecycle

    private static readonly System.Collections.Generic.HashSet<string> _world14Scenes
        = new System.Collections.Generic.HashSet<string>
        { "1-2", "1-4", "1-2-Underground", "1-2-Castle", "LevelStart", "GameOver" };

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        if (transform.parent != null)
            transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.enabled = !_world14Scenes.Contains(scene.name);
    }

    private void OnEnable()
    {
        GameManager.OnPause += UpdatePauseText;
    }

    private void OnDisable()
    {
        GameManager.OnPause -= UpdatePauseText;
    }

    #endregion

    #region Event Handlers

    private void UpdatePauseText()
    {
        if (GameManager.Instance != null)
        {
            pauseText.SetActive(GameManager.Instance.IsPaused);
        }
        else
        {
            pauseText.SetActive(false);
            Debug.LogWarning("[UI] GameManager.Instance is null — cannot update pause text.");
        }
    }

    #endregion

    #region Public UI Update Methods

    /// <summary> Updates the world-stage text (e.g., "1-1"). </summary>
    public void UpdateWorldStage()
    {
        if (GameManager.Instance != null)
        {
            worldStage.text = $"{GameManager.Instance.World}-{GameManager.Instance.Stage}";
        }
    }

    /// <summary> Updates the coins counter (always two digits, e.g., 03). </summary>
    public void UpdateCoinsCounter()
    {
        if (GameManager.Instance != null)
        {
            coinsCounter.text = $"{GameManager.Instance.Coins:D2}";
        }
    }

    /// <summary> Updates the lives counter (always two digits, e.g., 05). </summary>
    public void UpdateLivesCounter()
    {
        if (GameManager.Instance != null)
        {
            livesCounter.text = $"{GameManager.Instance.Lives:D2}";
        }
    }

    #endregion
}
