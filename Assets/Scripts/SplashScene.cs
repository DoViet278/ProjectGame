using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SplashScene : MonoBehaviour
{
    [Header("Loading Settings")]
    [SerializeField] private float timeToLoad = 5.0f;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    [Header("UI Elements")]
    [SerializeField] private Image loadingBar;          // Thanh loading (Image Type: Filled)
    [SerializeField] private TextMeshProUGUI percentText; // Text hiển thị "Loading XX%"
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation Settings")]
    [SerializeField] private float glowPulseSpeed = 2f;
    [SerializeField] private Color glowColor = new Color(0.3f, 0.9f, 1f, 1f); // Cyan glow

    private float elapsedTime = 0f;

    private void Awake()
    {
        // Initialize canvas group if not assigned
        if (canvasGroup == null)
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
            if (canvasGroup == null)
            {
                GameObject canvas = GameObject.Find("Canvas");
                if (canvas != null)
                {
                    canvasGroup = canvas.GetComponent<CanvasGroup>();
                    if (canvasGroup == null)
                    {
                        canvasGroup = canvas.AddComponent<CanvasGroup>();
                    }
                }
            }
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }

    private void Start()
    {
        StartCoroutine(LoadingSequence());
    }

    private void Update()
    {
        // Pulse glow effect on loading bar
        if (loadingBar != null)
        {
            float pulse = (Mathf.Sin(Time.time * glowPulseSpeed) + 1f) / 2f;
            Color barColor = glowColor;
            barColor.a = Mathf.Lerp(0.8f, 1f, pulse);
            loadingBar.color = barColor;
        }
    }

    private IEnumerator LoadingSequence()
    {
        // Hiển thị ngay lập tức (không fade)
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }

        // Loading progress
        yield return StartCoroutine(LoadAsynchronously());

        // Go to next scene (không fade out)
        GotoScene();
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);
            }
            yield return null;
        }
        
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }

    private IEnumerator LoadAsynchronously()
    {
        while (elapsedTime < timeToLoad)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / timeToLoad);

            // Update loading bar fill
            if (loadingBar != null)
            {
                loadingBar.fillAmount = progress;
            }

            // Update percentage text "Loading XX%"
            if (percentText != null)
            {
                int percent = Mathf.RoundToInt(progress * 100f);
                percentText.text = "Loading " + percent + "%";
            }

            yield return null;
        }

        // Ensure everything is at 100%
        if (loadingBar != null) loadingBar.fillAmount = 1f;
        if (percentText != null) percentText.text = "Loading 100%";
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);
            }
            yield return null;
        }
        
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }

    private void GotoScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
