using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Tutorial
{
    /// <summary>
    /// Handles visual effects and animations for tutorial UI elements.
    /// Provides fade, pulse, glow, and color transition effects.
    /// </summary>
    public class TutorialUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image keyBackground;
        [SerializeField] private Image shadowImage;
        [SerializeField] private Image completionCheckmark;
        [SerializeField] private Image glowImage;

        [Header("Animation Settings")]
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private float pulseDuration = 1f;
        [SerializeField] private float pulseScale = 1.1f;
        [SerializeField] private float glowIntensity = 1.5f;
        [SerializeField] private float floatAmplitude = 10f;
        [SerializeField] private float floatSpeed = 1f;
        
        [Header("Visual Settings")]
        [SerializeField] private bool enableGradient = true;
        [SerializeField] private bool enableShadow = true;
        [SerializeField] private float shadowOffset = 5f;

        private Vector3 originalScale;
        private Vector3 originalPosition;
        private Coroutine pulseCoroutine;
        private Coroutine glowCoroutine;
        private Coroutine floatCoroutine;

        private void Awake()
        {
            originalScale = transform.localScale;
            originalPosition = transform.localPosition;
            
            if (completionCheckmark != null)
                completionCheckmark.gameObject.SetActive(false);
                
            if (shadowImage != null && enableShadow)
            {
                // Position shadow slightly offset
                RectTransform shadowRect = shadowImage.GetComponent<RectTransform>();
                if (shadowRect != null)
                {
                    shadowRect.anchoredPosition = new Vector2(shadowOffset, -shadowOffset);
                }
            }
            
            if (glowImage != null)
            {
                glowImage.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Initialize UI element with tutorial step data
        /// </summary>
        public void Initialize(TutorialStep step)
        {
            if (keyText != null)
                keyText.text = step.keyName;
            
            if (descriptionText != null)
                descriptionText.text = step.actionDescription;
            
            SetDimmed();
            
            // DISABLED: Float animation causes cards to drift
            // Start subtle floating animation
            // if (floatCoroutine == null)
            // {
            //     floatCoroutine = StartCoroutine(FloatAnimation());
            // }
        }

        /// <summary>
        /// Fade in this UI element
        /// </summary>
        public void FadeIn(float duration = -1)
        {
            if (duration < 0) duration = fadeDuration;
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, duration));
        }

        /// <summary>
        /// Fade out this UI element
        /// </summary>
        public void FadeOut(float duration = -1)
        {
            if (duration < 0) duration = fadeDuration;
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, duration));
        }

        /// <summary>
        /// Set as active step with highlight effects
        /// </summary>
        public void SetActive(Color activeColor)
        {
            StopAllAnimations();
            
            // Set colors with gradient if enabled
            if (keyBackground != null)
            {
                if (enableGradient)
                {
                    // Create gradient effect by setting a lighter color
                    Color lightColor = activeColor * 1.3f;
                    keyBackground.color = activeColor;
                    // Note: For true gradients, you'd need a custom shader or UI Gradient component
                }
                else
                {
                    keyBackground.color = activeColor;
                }
            }
            
            if (keyText != null)
                keyText.color = Color.white;
                
            // Show glow effect
            if (glowImage != null)
            {
                glowImage.gameObject.SetActive(true);
                glowImage.color = activeColor;
            }
            
            // Start pulse animation
            pulseCoroutine = StartCoroutine(PulseAnimation());
            
            // Start glow effect
            if (keyBackground != null)
                glowCoroutine = StartCoroutine(GlowEffect(activeColor));
        }

        /// <summary>
        /// Set as completed with checkmark and color change
        /// </summary>
        public void SetCompleted(Color completedColor)
        {
            StopAllAnimations();
            
            // Set completed color
            if (keyBackground != null)
                keyBackground.color = completedColor;
                
            // Hide glow
            if (glowImage != null)
                glowImage.gameObject.SetActive(false);
            
            // Show checkmark with animation
            if (completionCheckmark != null)
            {
                completionCheckmark.gameObject.SetActive(true);
                StartCoroutine(CheckmarkAnimation());
            }
            
            // Scale down slightly
            StartCoroutine(ScaleTo(originalScale * 0.95f, 0.3f));
        }

        /// <summary>
        /// Set as dimmed/inactive
        /// </summary>
        public void SetDimmed()
        {
            StopAllAnimations();
            
            Color dimColor = new Color(0.5f, 0.5f, 0.5f, 0.6f);
            
            if (keyBackground != null)
                keyBackground.color = dimColor;
            
            if (keyText != null)
                keyText.color = new Color(0.7f, 0.7f, 0.7f);
                
            if (glowImage != null)
                glowImage.gameObject.SetActive(false);
            
            transform.localScale = originalScale;
            transform.localPosition = originalPosition; // Reset to original position
            
            // DISABLED: Float animation causes cards to drift
            // Restart float animation
            // if (floatCoroutine == null)
            // {
            //     floatCoroutine = StartCoroutine(FloatAnimation());
            // }
        }

        #region Animation Coroutines

        private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
                yield return null;
            }
            cg.alpha = end;
        }

        private IEnumerator PulseAnimation()
        {
            while (true)
            {
                // Scale up
                yield return ScaleTo(originalScale * pulseScale, pulseDuration / 2f);
                // Scale down
                yield return ScaleTo(originalScale, pulseDuration / 2f);
            }
        }

        private IEnumerator ScaleTo(Vector3 targetScale, float duration)
        {
            Vector3 startScale = transform.localScale;
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                // Smooth easing
                t = t * t * (3f - 2f * t);
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }
            
            transform.localScale = targetScale;
        }

        private IEnumerator GlowEffect(Color baseColor)
        {
            Material keyMaterial = keyBackground?.material;
            if (keyMaterial == null) yield break;

            while (true)
            {
                // Pulse glow intensity
                float glow = Mathf.PingPong(Time.time, glowIntensity);
                Color glowColor = baseColor * (1f + glow);
                keyMaterial.SetColor("_Color", glowColor);
                yield return null;
            }
        }

        private IEnumerator CheckmarkAnimation()
        {
            if (completionCheckmark == null) yield break;

            // Start small and scale up with bounce
            completionCheckmark.transform.localScale = Vector3.zero;
            
            float duration = 0.5f;
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                // Elastic ease out
                float scale = Mathf.Sin(t * Mathf.PI * 1.5f) * (1f - t) * 0.5f + t;
                completionCheckmark.transform.localScale = Vector3.one * scale;
                
                yield return null;
            }
            
            completionCheckmark.transform.localScale = Vector3.one;
        }
        
        private IEnumerator FloatAnimation()
        {
            float elapsed = 0f;
            
            while (true)
            {
                elapsed += Time.deltaTime * floatSpeed;
                
                float yOffset = Mathf.Sin(elapsed) * floatAmplitude;
                transform.localPosition = originalPosition + new Vector3(0f, yOffset, 0f);
                
                yield return null;
            }
        }

        private void StopAllAnimations()
        {
            if (pulseCoroutine != null)
            {
                StopCoroutine(pulseCoroutine);
                pulseCoroutine = null;
            }
            
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
                glowCoroutine = null;
            }
            
            if (floatCoroutine != null)
            {
                StopCoroutine(floatCoroutine);
                floatCoroutine = null;
            }
            
            StopAllCoroutines();
        }

        #endregion

        private void OnDestroy()
        {
            StopAllAnimations();
        }
    }
}
