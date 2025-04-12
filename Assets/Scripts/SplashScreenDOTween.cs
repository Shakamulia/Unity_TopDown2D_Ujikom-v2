using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening; // Make sure to include the DOTween namespace
using UnityEngine.UI; // Include the UI namespace for Image component
using TMPro; // Include the TextMeshPro namespace if you're using TextMeshPro for text

public class SplashScreenDOTween : MonoBehaviour
{
    public Image[] logoImages; // Reference to the logo images
    public TextMeshProUGUI[] logoTexts; // Reference to the logo texts (if any)
    public float fadeDuration = 1f; // Duration of the fade effect
    public float waitBeforeFadeOut = 1f; // Time to wait before starting the fade
    public string nextSceneName = "MainMenu"; // Name of the next scene to load

    // Start is called before the first frame update
    void Start()
    {
        foreach (var logoImage in logoImages)
        {
            logoImage.color = new Color(1, 1, 1, 0);

            logoImage.DOFade(1, fadeDuration).OnComplete(() =>
            {
                // Wait for a moment before fading out
                DOVirtual.DelayedCall(waitBeforeFadeOut, () =>
                {
                    logoImage.DOFade(0, fadeDuration).OnComplete(() =>
                    {
                        // Load the next scene after the fade out
                        SceneManager.LoadScene(nextSceneName);
                    });
                });
            });
        }

        foreach (var logoText in logoTexts)
        {
            logoText.color = new Color(1, 1, 1, 0);
            logoText.DOFade(1, fadeDuration).OnComplete(() =>
            {
                // Wait for a moment before fading out
                DOVirtual.DelayedCall(waitBeforeFadeOut, () =>
                {
                    logoText.DOFade(0, fadeDuration);
                });
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
