using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component để tạo hiệu ứng shimmer/wave cho loading bar
/// Attach vào LoadingBar để có hiệu ứng sóng ánh sáng chạy qua
/// </summary>
public class LoadingBarShimmer : MonoBehaviour
{
    [Header("Shimmer Settings")]
    [SerializeField] private Image shimmerImage;
    [SerializeField] private float shimmerSpeed = 1f;
    [SerializeField] private float shimmerWidth = 0.3f;
    [SerializeField] private Color shimmerColor = new Color(1f, 1f, 1f, 0.5f);
    
    [Header("Wave Effect")]
    [SerializeField] private bool enableWave = true;
    [SerializeField] private float waveAmplitude = 5f;
    [SerializeField] private float waveFrequency = 2f;

    private RectTransform rectTransform;
    private Material shimmerMaterial;
    private float shimmerPosition = -1f;
    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;

        // Create shimmer overlay if not assigned
        if (shimmerImage == null)
        {
            CreateShimmerOverlay();
        }
    }

    private void Update()
    {
        UpdateShimmer();
        
        if (enableWave)
        {
            UpdateWaveEffect();
        }
    }

    private void CreateShimmerOverlay()
    {
        // Create a child image for shimmer effect
        GameObject shimmerObj = new GameObject("Shimmer");
        shimmerObj.transform.SetParent(transform);
        
        shimmerImage = shimmerObj.AddComponent<Image>();
        shimmerImage.color = shimmerColor;
        
        RectTransform shimmerRect = shimmerImage.GetComponent<RectTransform>();
        shimmerRect.anchorMin = new Vector2(0, 0);
        shimmerRect.anchorMax = new Vector2(0, 1);
        shimmerRect.pivot = new Vector2(0, 0.5f);
        shimmerRect.anchoredPosition = Vector2.zero;
        shimmerRect.sizeDelta = new Vector2(100, 0);
    }

    private void UpdateShimmer()
    {
        if (shimmerImage == null) return;

        // Move shimmer across the bar
        shimmerPosition += shimmerSpeed * Time.deltaTime;
        
        if (shimmerPosition > 1f + shimmerWidth)
        {
            shimmerPosition = -shimmerWidth;
        }

        // Update shimmer position and alpha based on position
        RectTransform shimmerRect = shimmerImage.GetComponent<RectTransform>();
        float barWidth = rectTransform.rect.width;
        shimmerRect.anchoredPosition = new Vector2(shimmerPosition * barWidth, 0);
        shimmerRect.sizeDelta = new Vector2(barWidth * shimmerWidth, 0);

        // Fade in/out effect for shimmer
        float alpha = Mathf.Clamp01(1f - Mathf.Abs((shimmerPosition - 0.5f) * 2f));
        Color color = shimmerColor;
        color.a = shimmerColor.a * alpha;
        shimmerImage.color = color;
    }

    private void UpdateWaveEffect()
    {
        // Create subtle wave motion
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        rectTransform.localPosition = originalPosition + new Vector3(0, wave, 0);
    }

    public void SetShimmerSpeed(float speed)
    {
        shimmerSpeed = speed;
    }

    public void SetWaveEnabled(bool enabled)
    {
        enableWave = enabled;
        if (!enabled)
        {
            rectTransform.localPosition = originalPosition;
        }
    }
}
