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
    [SerializeField] private Image loadingBar;
    [SerializeField] private Image loadingBarGlow;
    [SerializeField] private Transform personTransform;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private TextMeshProUGUI percentageText;
    [SerializeField] private Image loadingIcon;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image logoImage;

    [Header("Animation Settings")]
    [SerializeField] private float iconRotationSpeed = 200f;
    [SerializeField] private float dotAnimationSpeed = 0.5f;
    [SerializeField] private float glowPulseSpeed = 2f;
    [SerializeField] private float glowMinAlpha = 0.3f;
    [SerializeField] private float glowMaxAlpha = 0.8f;

    private float elapsedTime = 0f;
    private string baseLoadingText = "Đang tải";
    private int dotCount = 0;
    private float dotTimer = 0f;

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
        if (personTransform != null && startTransform != null)
        {
            personTransform.position = startTransform.position;
        }

        StartCoroutine(LoadingSequence());
    }

    private void Update()
    {
        // Rotate loading icon
        if (loadingIcon != null)
        {
            loadingIcon.transform.Rotate(0f, 0f, -iconRotationSpeed * Time.deltaTime);
        }

        // Animate loading text dots
        if (loadingText != null)
        {
            dotTimer += Time.deltaTime;
            if (dotTimer >= dotAnimationSpeed)
            {
                dotTimer = 0f;
                dotCount = (dotCount + 1) % 4;
                string dots = new string('.', dotCount);
                loadingText.text = baseLoadingText + dots;
            }
        }

        // Pulse glow effect
        if (loadingBarGlow != null)
        {
            float alpha = Mathf.Lerp(glowMinAlpha, glowMaxAlpha, 
                (Mathf.Sin(Time.time * glowPulseSpeed) + 1f) / 2f);
            Color glowColor = loadingBarGlow.color;
            glowColor.a = alpha;
            loadingBarGlow.color = glowColor;
        }
    }

    private IEnumerator LoadingSequence()
    {
        // Fade in
        yield return StartCoroutine(FadeIn());

        // Loading progress
        yield return StartCoroutine(LoadAsynchronously());

        // Fade out
        yield return StartCoroutine(FadeOut());

        // Go to next scene
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

            // Update loading bar
            if (loadingBar != null)
            {
                loadingBar.fillAmount = progress;
            }

            // Update glow bar (slightly ahead for effect)
            if (loadingBarGlow != null)
            {
                loadingBarGlow.fillAmount = Mathf.Clamp01(progress + 0.1f);
            }

            // Update person position
            if (personTransform != null && startTransform != null && endTransform != null)
            {
                personTransform.position = Vector3.Lerp(startTransform.position, endTransform.position, progress);
            }

            // Update percentage text
            if (percentageText != null)
            {
                percentageText.text = Mathf.RoundToInt(progress * 100f) + "%";
            }

            // Scale logo slightly for breathing effect
            if (logoImage != null)
            {
                float scale = 1f + Mathf.Sin(Time.time * 2f) * 0.05f;
                logoImage.transform.localScale = Vector3.one * scale;
            }

            yield return null;
        }

        // Ensure everything is at 100%
        if (loadingBar != null) loadingBar.fillAmount = 1f;
        if (loadingBarGlow != null) loadingBarGlow.fillAmount = 1f;
        if (percentageText != null) percentageText.text = "100%";
        if (loadingText != null) loadingText.text = "Hoàn thành!";
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
