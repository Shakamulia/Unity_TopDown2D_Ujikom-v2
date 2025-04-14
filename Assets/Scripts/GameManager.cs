using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public GameObject playerPrefab;
    public GameObject uiCanvasPrefab;

    private GameObject playerInstance;
    private GameObject uiCanvasInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnPlayerIfNotExists();
        SpawnUIIfNotExists();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set ulang player posisinya kalau kamu mau atur per scene
        if (playerInstance == null)
        {
            SpawnPlayerIfNotExists();
        }
    }

    void SpawnPlayerIfNotExists()
    {
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance);
        }
    }

    void SpawnUIIfNotExists()
    {
        if (uiCanvasInstance == null)
        {
            uiCanvasInstance = Instantiate(uiCanvasPrefab);
            DontDestroyOnLoad(uiCanvasInstance);
        }
    }
}
