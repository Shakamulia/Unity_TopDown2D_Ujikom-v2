using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab;

    public UnityEvent<Transform> OnPlayerSpawned = new UnityEvent<Transform>();
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject); // Opsional, kalau kamu ingin GameManager tetap hidup


            if (PlayerController.Instance == null)
        {
            GameObject playerObj = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerObj);
        }
    }
    public void PlayerSpawned(Transform playerTransform)
    {
        OnPlayerSpawned.Invoke(playerTransform);
    }
    public void RetryLevel()
    {
        DestroyPersistentObjects();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        DestroyPersistentObjects();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    private void DestroyPersistentObjects()
    {
        if (PlayerHealth.Instance) Destroy(PlayerHealth.Instance.gameObject);
        if (ActiveWeapon.Instance) Destroy(ActiveWeapon.Instance.gameObject);
        //if (InventoryManager.Instance) Destroy(InventoryManager.Instance.gameObject);
        // Tambahkan lainnya jika perlu
    }
}
