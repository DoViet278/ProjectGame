using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Tutorial
{
    /// <summary>
    /// Controls animated background effects for the tutorial scene.
    /// Provides gradient transitions and ambient atmosphere.
    /// </summary>
    public class TutorialBackgroundController : MonoBehaviour
    {
        [Header("Background Image")]
        [SerializeField] private Image backgroundImage;
        
        [Header("Gradient Colors")]
        [SerializeField] private Color[] gradientColors = new Color[]
        {
            new Color(0.2f, 0.3f, 0.5f, 0.3f), // Blue tint
            new Color(0.3f, 0.2f, 0.5f, 0.3f), // Purple tint
            new Color(0.2f, 0.4f, 0.4f, 0.3f)  // Teal tint
        };

        [Header("Animation Settings")]
        [SerializeField] private float transitionDuration = 5f;
        [SerializeField] private bool autoTransition = true;

        [Header("Overlay Panel")]
        [SerializeField] private Image overlayPanel;
        [SerializeField] private float overlayAlpha = 0.3f;

        private int currentColorIndex = 0;
        private Coroutine transitionCoroutine;

        #region Unity Lifecycle

        private void Start()
        {
            InitializeBackground();
            
            if (autoTransition)
            {
                StartGradientTransitions();
            }
        }

        #endregion

        #region Initialization

        private void InitializeBackground()
        {
            if (backgroundImage != null && gradientColors.Length > 0)
            {
                backgroundImage.color = gradientColors[0];
            }

            if (overlayPanel != null)
            {
                Color overlayColor = overlayPanel.color;
                overlayColor.a = overlayAlpha;
                overlayPanel.color = overlayColor;
            }
        }

        #endregion

        #region Gradient Transitions

        private void StartGradientTransitions()
        {
            if (gradientColors.Length <= 1) return;
            
            transitionCoroutine = StartCoroutine(GradientTransitionLoop());
        }

        private IEnumerator GradientTransitionLoop()
        {
            while (true)
            {
                int nextColorIndex = (currentColorIndex + 1) % gradientColors.Length;
                
                yield return TransitionToColor(gradientColors[nextColorIndex], transitionDuration);
                
                currentColorIndex = nextColorIndex;
                
                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator TransitionToColor(Color targetColor, float duration)
        {
            if (backgroundImage == null) yield break;

            Color startColor = backgroundImage.color;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                // Smooth easing
                t = Mathf.SmoothStep(0f, 1f, t);
                
                backgroundImage.color = Color.Lerp(startColor, targetColor, t);
                
                yield return null;
            }

            backgroundImage.color = targetColor;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Transition to a specific color
        /// </summary>
        public void TransitionTo(Color color, float duration = -1)
        {
            if (duration < 0) duration = transitionDuration;
            
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
            
            StartCoroutine(TransitionToColor(color, duration));
        }

        /// <summary>
        /// Set background color immediately
        /// </summary>
        public void SetColor(Color color)
        {
            if (backgroundImage != null)
            {
                backgroundImage.color = color;
            }
        }

        /// <summary>
        /// Pulse effect for special moments
        /// </summary>
        public void PlayPulseEffect(Color pulseColor, float duration = 0.5f)
        {
            StartCoroutine(PulseEffect(pulseColor, duration));
        }

        #endregion

        #region Effects

        private IEnumerator PulseEffect(Color pulseColor, float duration)
        {
            if (overlayPanel == null) yield break;

            Color originalColor = overlayPanel.color;
            Color targetColor = pulseColor;
            targetColor.a = overlayAlpha * 1.5f;

            // Pulse to target color
            float halfDuration = duration / 2f;
            yield return TransitionOverlayColor(originalColor, targetColor, halfDuration);

            // Pulse back
            yield return TransitionOverlayColor(targetColor, originalColor, halfDuration);
        }

        private IEnumerator TransitionOverlayColor(Color from, Color to, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                overlayPanel.color = Color.Lerp(from, to, t);
                
                yield return null;
            }

            overlayPanel.color = to;
        }

        #endregion

        #region Cleanup

        private void OnDestroy()
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
        }

        #endregion
    }
}
