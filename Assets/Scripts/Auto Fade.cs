using UnityEngine;
using TMPro;
public class AutoFade : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    [Header("Text Settings")]
    public TMP_Text targetText; // Drag TextMeshPro object ke sini di Inspector
    [TextArea] public string customText; // Input custom text di Inspector
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void OnEnable()
    {
        if(targetText != null) targetText.text = customText;
        StartCoroutine(FadeIn());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0; // Mulai dari transparan
        
        // Fade-in: 0 → 1
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(displayDuration); // Tahan tampil
        StartCoroutine(FadeOut());
    }

    private System.Collections.IEnumerator FadeOut()
    {
        // Fade-out: 1 → 0
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        gameObject.SetActive(false); // Matikan panel setelah selesai
    }
}