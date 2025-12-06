using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    /// <summary>
    /// Manages the tutorial flow and progression.
    /// Handles step-by-step tutorial with input detection and visual feedback.
    /// </summary>
    public class TutorialManager : MonoBehaviour
    {
        [Header("Tutorial Steps")]
        [SerializeField] private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
        
        [Header("UI References")]
        [SerializeField] private List<TutorialUI> stepUIElements = new List<TutorialUI>();
        [SerializeField] private CanvasGroup tutorialPanel;
        
        [Header("Visual Effects")]
        [SerializeField] private TutorialParticleController particleController;
        [SerializeField] private TutorialBackgroundController backgroundController;
        [SerializeField] private bool enableScreenShake = true;
        [SerializeField] private float shakeIntensity = 0.1f;
        
        [Header("Settings")]
        [SerializeField] private float delayBetweenSteps = 0.3f;
        [SerializeField] private float completionDelay = 2f;
        [SerializeField] private string nextSceneName = "MainScene";
        [SerializeField] private bool autoStartTutorial = true;
        
        [Header("Audio")]
        [SerializeField] private bool playTutorialMusic = true;
        [SerializeField] private bool playSoundEffects = true;

        private int currentStepIndex = 0;
        private bool tutorialActive = false;
        private bool allStepsCompleted = false;
        private Camera mainCamera;
        private Vector3 originalCameraPosition;

        #region Unity Lifecycle

        private void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                originalCameraPosition = mainCamera.transform.position;
            }
            
            InitializeTutorial();
            
            if (autoStartTutorial)
            {
                StartCoroutine(StartTutorialSequence());
            }
        }

        private void Update()
        {
            if (!tutorialActive || allStepsCompleted)
                return;

            CheckCurrentStepInput();
        }

        #endregion

        #region Tutorial Initialization

        /// <summary>
        /// Initialize tutorial with default steps if none configured
        /// </summary>
        private void InitializeTutorial()
        {
            // If no steps configured, create default ones
            if (tutorialSteps.Count == 0)
            {
                tutorialSteps.Add(new TutorialStep("A", "Left", KeyCode.A));
                tutorialSteps.Add(new TutorialStep("D", "Right", KeyCode.D));
                tutorialSteps.Add(new TutorialStep("S", "Crouch", KeyCode.S));
                tutorialSteps.Add(new TutorialStep("W", "Stand", KeyCode.W));
                tutorialSteps.Add(new TutorialStep("Space", "Jump", KeyCode.Space));
            }

            // Initialize UI elements
            if (stepUIElements.Count >= tutorialSteps.Count)
            {
                for (int i = 0; i < tutorialSteps.Count; i++)
                {
                    stepUIElements[i].Initialize(tutorialSteps[i]);
                }
            }
            else
            {
                Debug.LogWarning("TutorialManager: Not enough UI elements for all tutorial steps!");
            }
        }

        /// <summary>
        /// Start the tutorial sequence with fade in
        /// </summary>
        private IEnumerator StartTutorialSequence()
        {
            // Fade in tutorial panel
            if (tutorialPanel != null)
            {
                tutorialPanel.alpha = 0f;
                yield return FadeCanvasGroup(tutorialPanel, 0f, 1f, 0.5f);
            }

            // Fade in all step UI elements
            foreach (var stepUI in stepUIElements)
            {
                stepUI.FadeIn(0.5f);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);

            // Start first step
            tutorialActive = true;
            ActivateStep(0);
        }

        #endregion

        #region Step Management

        /// <summary>
        /// Activate a specific tutorial step
        /// </summary>
        private void ActivateStep(int stepIndex)
        {
            if (stepIndex >= tutorialSteps.Count)
            {
                CompleteTutorial();
                return;
            }

            currentStepIndex = stepIndex;
            TutorialStep currentStep = tutorialSteps[currentStepIndex];
            TutorialUI currentUI = stepUIElements[currentStepIndex];

            // Highlight current step
            currentUI.SetActive(currentStep.activeColor);

            Debug.Log($"Tutorial: Active step {currentStepIndex} - Press {currentStep.keyName}");
        }

        /// <summary>
        /// Check if player pressed the correct key for current step
        /// </summary>
        private void CheckCurrentStepInput()
        {
            if (currentStepIndex >= tutorialSteps.Count)
                return;

            TutorialStep currentStep = tutorialSteps[currentStepIndex];

            if (Input.GetKeyDown(currentStep.requiredKey))
            {
                // Play key press effect
                if (particleController != null && stepUIElements.Count > currentStepIndex)
                {
                    Vector3 uiPosition = stepUIElements[currentStepIndex].transform.position;
                    particleController.PlayKeyPressEffect(uiPosition, currentStep.activeColor);
                }
                
                // Play sound effect
                if (playSoundEffects)
                {
                    PlayKeyPressSound();
                }
                
                CompleteCurrentStep();
            }
        }

        /// <summary>
        /// Mark current step as completed and move to next
        /// </summary>
        private void CompleteCurrentStep()
        {
            TutorialStep completedStep = tutorialSteps[currentStepIndex];
            TutorialUI completedUI = stepUIElements[currentStepIndex];

            // Mark as completed
            completedStep.Complete();
            completedUI.SetCompleted(completedStep.completedColor);

            // Play completion particle effect
            if (particleController != null)
            {
                Vector3 uiPosition = completedUI.transform.position;
                particleController.PlayStepCompletionEffect(uiPosition, completedStep.completedColor);
            }
            
            // Play completion sound
            if (playSoundEffects)
            {
                PlayStepCompleteSound();
            }
            
            // Subtle screen shake
            if (enableScreenShake)
            {
                StartCoroutine(ScreenShake(0.1f, shakeIntensity * 0.5f));
            }
            
            // Background pulse effect
            if (backgroundController != null)
            {
                backgroundController.PlayPulseEffect(completedStep.completedColor, 0.3f);
            }

            Debug.Log($"Tutorial: Completed step {currentStepIndex} - {completedStep.keyName}");

            // Move to next step after delay
            StartCoroutine(MoveToNextStepAfterDelay());
        }

        /// <summary>
        /// Move to next step after a small delay
        /// </summary>
        private IEnumerator MoveToNextStepAfterDelay()
        {
            yield return new WaitForSeconds(delayBetweenSteps);

            int nextStepIndex = currentStepIndex + 1;
            
            if (nextStepIndex < tutorialSteps.Count)
            {
                ActivateStep(nextStepIndex);
            }
            else
            {
                CompleteTutorial();
            }
        }

        #endregion

        #region Tutorial Completion

        /// <summary>
        /// Complete the entire tutorial
        /// </summary>
        private void CompleteTutorial()
        {
            allStepsCompleted = true;
            tutorialActive = false;

            Debug.Log("Tutorial: All steps completed!");

            StartCoroutine(CompletionSequence());
        }

        /// <summary>
        /// Handle tutorial completion with animations and scene transition
        /// </summary>
        private IEnumerator CompletionSequence()
        {
            // Play celebration effects!
            if (particleController != null)
            {
                particleController.PlayFinalCelebration();
            }
            
            // Screen shake for impact
            if (enableScreenShake)
            {
                StartCoroutine(ScreenShake(0.5f, shakeIntensity));
            }
            
            // Play celebration sound
            if (playSoundEffects)
            {
                PlayCelebrationSound();
            }
            
            // Wait a moment to enjoy the celebration
            yield return new WaitForSeconds(completionDelay);

            // Fade out tutorial panel
            if (tutorialPanel != null)
            {
                yield return FadeCanvasGroup(tutorialPanel, 1f, 0f, 0.5f);
            }

            // Load next scene
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Manually start the tutorial
        /// </summary>
        public void StartTutorial()
        {
            if (!tutorialActive)
            {
                StartCoroutine(StartTutorialSequence());
            }
        }

        /// <summary>
        /// Skip tutorial and go to next scene immediately
        /// </summary>
        public void SkipTutorial()
        {
            StopAllCoroutines();
            
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        /// <summary>
        /// Reset tutorial to beginning
        /// </summary>
        public void ResetTutorial()
        {
            StopAllCoroutines();
            
            // Reset all steps
            foreach (var step in tutorialSteps)
            {
                step.Reset();
            }

            // Reset UI
            foreach (var stepUI in stepUIElements)
            {
                stepUI.SetDimmed();
            }

            currentStepIndex = 0;
            allStepsCompleted = false;
            tutorialActive = false;

            // Restart if auto-start
            if (autoStartTutorial)
            {
                StartCoroutine(StartTutorialSequence());
            }
        }

        #endregion

        #region Utilities

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
        
        private IEnumerator ScreenShake(float duration, float magnitude)
        {
            if (mainCamera == null) yield break;
            
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                
                mainCamera.transform.position = originalCameraPosition + new Vector3(x, y, 0f);
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            mainCamera.transform.position = originalCameraPosition;
        }

        #endregion
        
        #region Audio Helpers
        
        private void PlayKeyPressSound()
        {
            // Play subtle click sound
            // AudioManager integration would go here
        }
        
        private void PlayStepCompleteSound()
        {
            // Play success sound
            // AudioManager integration would go here
        }
        
        private void PlayCelebrationSound()
        {
            // Play celebration fanfare
            // AudioManager integration would go here
        }
        
        #endregion

        #region Gizmos (Editor Only)

        #if UNITY_EDITOR
        private void OnValidate()
        {
            // Ensure we have enough UI elements for all steps
            if (stepUIElements.Count < tutorialSteps.Count)
            {
                Debug.LogWarning($"TutorialManager: Only {stepUIElements.Count} UI elements for {tutorialSteps.Count} steps!");
            }
        }
        #endif

        #endregion
    }
}
