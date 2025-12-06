using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tutorial
{
    /// <summary>
    /// Manages particle effects for tutorial interactions.
    /// Provides completion bursts, celebration effects, and key press feedback.
    /// </summary>
    public class TutorialParticleController : MonoBehaviour
    {
        [Header("Particle Systems")]
        [SerializeField] private ParticleSystem stepCompletionPrefab;
        [SerializeField] private ParticleSystem celebrationPrefab;
        [SerializeField] private ParticleSystem keyPressPrefab;

        [Header("Pool Settings")]
        [SerializeField] private int poolSize = 10;

        [Header("Effect Settings")]
        [SerializeField] private float completionBurstDuration = 1f;
        [SerializeField] private int celebrationParticleCount = 50;
        [SerializeField] private float celebrationDuration = 3f;

        private Queue<ParticleSystem> particlePool;
        private List<ParticleSystem> activeParticles;

        #region Unity Lifecycle

        private void Awake()
        {
            InitializePool();
        }

        #endregion

        #region Initialization

        private void InitializePool()
        {
            particlePool = new Queue<ParticleSystem>();
            activeParticles = new List<ParticleSystem>();

            // Create pool of particle systems
            if (stepCompletionPrefab != null)
            {
                for (int i = 0; i < poolSize; i++)
                {
                    ParticleSystem ps = Instantiate(stepCompletionPrefab, transform);
                    ps.gameObject.SetActive(false);
                    particlePool.Enqueue(ps);
                }
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Play particle effect when a step is completed
        /// </summary>
        public void PlayStepCompletionEffect(Vector3 position, Color color)
        {
            ParticleSystem ps = GetPooledParticle();
            if (ps == null) return;

            // Position the particle system
            ps.transform.position = position;
            ps.gameObject.SetActive(true);

            // Set color
            var main = ps.main;
            main.startColor = new ParticleSystem.MinMaxGradient(color);

            // Play
            ps.Play();

            // Return to pool after duration
            StartCoroutine(ReturnToPoolAfterDelay(ps, completionBurstDuration));
        }

        /// <summary>
        /// Play celebration effect when tutorial is completed
        /// </summary>
        public void PlayFinalCelebration()
        {
            if (celebrationPrefab != null)
            {
                // Create multiple bursts for dramatic effect
                StartCoroutine(CelebrationSequence());
            }
        }

        /// <summary>
        /// Play subtle effect when key is pressed
        /// </summary>
        public void PlayKeyPressEffect(Vector3 position, Color color)
        {
            if (keyPressPrefab == null) return;

            ParticleSystem ps = Instantiate(keyPressPrefab, position, Quaternion.identity, transform);
            
            var main = ps.main;
            main.startColor = new ParticleSystem.MinMaxGradient(color);
            
            ps.Play();
            
            Destroy(ps.gameObject, 2f);
        }

        /// <summary>
        /// Stop all particle effects
        /// </summary>
        public void StopAllEffects()
        {
            foreach (var ps in activeParticles)
            {
                if (ps != null && ps.isPlaying)
                {
                    ps.Stop();
                }
            }
        }

        #endregion

        #region Particle Pool Management

        private ParticleSystem GetPooledParticle()
        {
            if (particlePool.Count > 0)
            {
                ParticleSystem ps = particlePool.Dequeue();
                activeParticles.Add(ps);
                return ps;
            }

            // Pool is empty, create new one
            if (stepCompletionPrefab != null)
            {
                ParticleSystem ps = Instantiate(stepCompletionPrefab, transform);
                activeParticles.Add(ps);
                return ps;
            }

            return null;
        }

        private IEnumerator ReturnToPoolAfterDelay(ParticleSystem ps, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (ps != null)
            {
                ps.Stop();
                ps.gameObject.SetActive(false);
                activeParticles.Remove(ps);
                particlePool.Enqueue(ps);
            }
        }

        #endregion

        #region Celebration Sequence

        private IEnumerator CelebrationSequence()
        {
            // Create celebration particles at random positions
            Vector3 screenCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
            
            for (int i = 0; i < 5; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-3f, 3f),
                    Random.Range(-2f, 2f),
                    0f
                );

                ParticleSystem celebration = Instantiate(
                    celebrationPrefab, 
                    screenCenter + randomOffset, 
                    Quaternion.identity, 
                    transform
                );

                // Randomize colors for confetti effect
                var main = celebration.main;
                Color randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
                main.startColor = new ParticleSystem.MinMaxGradient(randomColor);

                celebration.Play();
                Destroy(celebration.gameObject, celebrationDuration);

                yield return new WaitForSeconds(0.2f);
            }
        }

        #endregion

        #region Cleanup

        private void OnDestroy()
        {
            StopAllEffects();
        }

        #endregion
    }
}
