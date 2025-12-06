using System;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Data class representing a single step in the tutorial.
    /// Each step has a key to press, description, and completion status.
    /// </summary>
    [Serializable]
    public class TutorialStep
    {
        [Header("Key Information")]
        [Tooltip("Display name of the key (e.g., 'A', 'Space', 'Shift')")]
        public string keyName;
        
        [Tooltip("Vietnamese description of the action (e.g., 'Trái', 'Nhảy', 'Cúi')")]
        public string actionDescription;
        
        [Tooltip("The actual KeyCode that needs to be pressed")]
        public KeyCode requiredKey;
        
        [Header("Visual Settings")]
        [Tooltip("Color for this step when active")]
        public Color activeColor = new Color(1f, 0.84f, 0f); // Gold
        
        [Tooltip("Color for this step when completed")]
        public Color completedColor = new Color(0.2f, 0.8f, 0.2f); // Green
        
        [Header("State")]
        [Tooltip("Has this step been completed?")]
        public bool completed = false;

        /// <summary>
        /// Creates a new tutorial step
        /// </summary>
        public TutorialStep(string keyName, string actionDescription, KeyCode requiredKey)
        {
            this.keyName = keyName;
            this.actionDescription = actionDescription;
            this.requiredKey = requiredKey;
            this.completed = false;
        }

        /// <summary>
        /// Mark this step as completed
        /// </summary>
        public void Complete()
        {
            completed = true;
        }

        /// <summary>
        /// Reset this step to incomplete
        /// </summary>
        public void Reset()
        {
            completed = false;
        }

        /// <summary>
        /// Get formatted display text for this step
        /// </summary>
        public string GetDisplayText()
        {
            return $"{keyName}: {actionDescription}";
        }
    }
}
