using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;


    [SerializeField] private GameObject LevelCompletedPanel;
    [SerializeField] private TextMeshProUGUI coinRewardText;
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button menuButton;
    [SerializeField] private int coinReward = 50;
    private float waitToLoadTime = 1f;

    private void Start()
    {
        nextLevel.onClick.AddListener(LoadNextLevel);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            //SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            //UIFade.Instance.FadeToBlack();
            //StartCoroutine(LoadSceneRoutine());
            ShowLevelCompletePanel();
            GiveCoinReward();
            UnlockNextLevel();
        }
    }
    private void ShowLevelCompletePanel()
        {
            Time.timeScale = 0f;
            LevelCompletedPanel.SetActive(true);
            coinRewardText.text = $"+{coinReward} Coins";
        
            // Cek apakah ada level berikutnya
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            nextLevel.interactable = (nextLevelIndex < SceneManager.sceneCountInBuildSettings);
        }
    private void GiveCoinReward()
    {
        // Menggunakan EconomyManager yang sudah ada
        for(int i = 0; i < coinReward; i++)
        {
            EconomyManager.Instance.UpdateCurrentGold();
        }
    }
    private void UnlockNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        
        // Save menggunakan PlayerPrefs
        if(nextLevelIndex > PlayerPrefs.GetInt("UnlockedLevel", 1))
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevelIndex);
            PlayerPrefs.Save();
        }
    }
    private void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    //private IEnumerator LoadSceneRoutine()
    //{
    //    while (waitToLoadTime >= 0)
    //    {
    //        waitToLoadTime -= Time.deltaTime;
    //        yield return null;
    //    }

    //    SceneManager.LoadScene(sceneToLoad);
    //}
}
