using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        PlayerPrefs.SetInt("Level1_Unlocked", 1); // Set the first level as unlocked by default
        PlayerPrefs.Save(); // Save the PlayerPrefs

        UpdateLevelButtons(); // Update the level buttons based on unlocked levels

        // Load the unlocked level from PlayerPrefs
        //int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        //for (int i = 0; i < levelButtons.Length; i++)
        //{
        //    levelButtons[i].interactable = (i + 1) <= unlockedLevel;
        //}
    }


    void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Level index starts from 1

            bool isUnlocked = PlayerPrefs.GetInt("Level" + levelIndex + "_Unlocked", 0) == 1; // Check if the level is unlocked

            //jika terkunci, nonaktifkan tombol dan beri warna abu abu
            if (!isUnlocked)
            {
                levelButtons[i].interactable = false; // Disable the button
                levelButtons[i].GetComponent<Image>().color = Color.gray; // Change button color to gray
            }
            else
            {
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // Add listener to load the level
            }
        }
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Scene" + levelIndex); // Load the selected level
    }

    public static void UnlockNextLevel (int currentLevelIndex)
    {
        int nextLevel = currentLevelIndex + 1; // Unlock the next level
        PlayerPrefs.SetInt("Level" + nextLevel + "_Unlocked", 1); // Set the next level as unlocked
        PlayerPrefs.Save(); // Save the PlayerPrefs
        levelButtons[i].interactable = true;
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Progress reset. All levels are locked.");
    }
}