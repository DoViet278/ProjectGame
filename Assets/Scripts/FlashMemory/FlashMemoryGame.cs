using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Flash Memory Game - Simple Version
/// 
/// Game Flow:
/// 1. Show random number sequence for a few seconds
/// 2. Player types it back using number buttons
/// 3. DEL = delete last digit, SKIP = next sequence, OK = submit
/// 4. Get 3 correct = win and exit scene
/// 5. Wrong = just continue playing
/// </summary>
public class FlashMemoryGame : MonoBehaviour
{
    [Header("=== TEXT DISPLAYS ===")]
    public TextMeshProUGUI displayText;      // Shows sequence and input
    public TextMeshProUGUI instructionText;  // Shows "Remember!" or "Enter!"
    public TextMeshProUGUI scoreText;        // Shows "Score: X"
    public TextMeshProUGUI progressText;     // Shows "0/3"
    public TextMeshProUGUI titleText;        // Shows "FLASH MEMORY"
    public TextMeshProUGUI descriptionText;  // Shows game description
    
    [Header("=== BUTTONS ===")]
    public Button[] numButtons = new Button[10];  // Buttons 0-9
    public Button delButton;    // Delete last digit
    public Button skipButton;   // Skip to next sequence
    public Button okButton;     // Submit answer
    public Button startButton;  // Start game
    
    [Header("=== SETTINGS ===")]
    public float showTime = 2f;           // How long to show sequence
    public int maxAttempts = 3;           // Max wrong attempts before new sequence
    public string exitScene = "Menu";     // Scene to load when done
    
    [Header("=== FADE (Optional) ===")]
    public Image fadeImage;               // Black image for fade effect
    
    // Private state
    string answer = "";      // The correct sequence
    string input = "";       // What player typed
    int score = 0;
    int wrongAttempts = 0;   // Wrong attempts on current sequence
    bool waitingForInput = false;
    
    void Start()
    {
        Debug.Log("=== FlashMemoryGame Start() called ===");
        
        // Hide everything, show start button
        if (displayText) displayText.text = "";
        if (instructionText) instructionText.text = "Press START!";
        if (progressText) progressText.text = "";
        if (scoreText) scoreText.text = "";
        
        // Setup button listeners
        SetupButtons();
        
        // Disable input buttons until game starts
        SetInputEnabled(false);
        
        Debug.Log($"Start button assigned: {startButton != null}");
        Debug.Log("=== Ready! Click START ===");
    }
    
    void SetupButtons()
    {
        // Number buttons 0-9
        for (int i = 0; i < numButtons.Length; i++)
        {
            if (numButtons[i] != null)
            {
                int digit = i;
                numButtons[i].onClick.RemoveAllListeners();
                numButtons[i].onClick.AddListener(() => OnNumPress(digit));
            }
        }
        
        // Control buttons
        if (delButton)
        {
            delButton.onClick.RemoveAllListeners();
            delButton.onClick.AddListener(OnDelPress);
        }
        
        if (skipButton)
        {
            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(OnSkipPress);
        }
        
        if (okButton)
        {
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(OnOkPress);
        }
        
        if (startButton)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartPress);
        }
    }
    
    void SetInputEnabled(bool enabled)
    {
        foreach (var btn in numButtons)
            if (btn) btn.interactable = enabled;
        if (delButton) delButton.interactable = enabled;
        if (skipButton) skipButton.interactable = enabled;
        if (okButton) okButton.interactable = enabled;
    }
    
    // ========== BUTTON HANDLERS ==========
    
    public void OnStartPress()
    {
        Debug.Log(">>> START BUTTON CLICKED! <<<");
        // Hide start button
        if (startButton) startButton.gameObject.SetActive(false);
        
        // Hide title and description
        if (titleText) titleText.gameObject.SetActive(false);
        if (descriptionText) descriptionText.gameObject.SetActive(false);
        
        // Reset game
        wrongAttempts = 0;
        score = 0;
        
        // Start first round
        StartCoroutine(PlayRound());
    }
    
    void OnNumPress(int digit)
    {
        if (!waitingForInput) return;
        if (input.Length >= answer.Length) return;
        
        input += digit.ToString();
        ShowInput();
    }
    
    void OnDelPress()
    {
        if (!waitingForInput) return;
        if (input.Length == 0) return;
        
        input = input.Substring(0, input.Length - 1);
        ShowInput();
    }
    
    void OnSkipPress()
    {
        if (!waitingForInput) return;
        
        waitingForInput = false;
        SetInputEnabled(false);
        
        if (instructionText) instructionText.text = "Skipped!";
        
        StartCoroutine(NextRoundDelay(0.5f));
    }
    
    void OnOkPress()
    {
        if (!waitingForInput) return;
        if (input.Length != answer.Length) return; // Must enter full sequence
        
        waitingForInput = false;
        SetInputEnabled(false);
        
        if (input == answer)
        {
            // CORRECT! WIN immediately!
            score += 100;
            
            if (instructionText) instructionText.text = "CORRECT! YOU WIN!";
            if (displayText) displayText.color = Color.green;
            if (scoreText) scoreText.text = "Score: " + score;
            
            // Start win sequence
            StartCoroutine(WinSequence());
        }
        else
        {
            // WRONG
            wrongAttempts++;
            
            if (wrongAttempts >= maxAttempts)
            {
                // Too many wrong attempts, new sequence
                if (instructionText) instructionText.text = $"Wrong {maxAttempts}x! New sequence...";
                if (displayText) displayText.color = Color.red;
                
                wrongAttempts = 0;
                StartCoroutine(NextRoundDelay(1.5f));
            }
            else
            {
                // Can try again
                int remaining = maxAttempts - wrongAttempts;
                if (instructionText) instructionText.text = $"Wrong! {remaining} tries left";
                if (displayText) displayText.color = Color.red;
                
                // Retry same sequence
                StartCoroutine(RetrySequence());
            }
        }
    }
    
    // ========== GAME LOGIC ==========
    
    IEnumerator PlayRound()
    {
        waitingForInput = false;
        input = "";
        wrongAttempts = 0;  // Reset attempts for new sequence
        
        // Reset text color
        if (displayText) displayText.color = Color.white;
        
        // Generate random sequence (3-5 digits)
        int length = Random.Range(3, 6);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
            sb.Append(Random.Range(0, 10));
        answer = sb.ToString();
        
        // Show attempts
        if (progressText) progressText.text = $"Attempts: 0/{maxAttempts}";
        
        // Show sequence
        if (instructionText) instructionText.text = "Remember!";
        if (displayText) displayText.text = answer;
        
        yield return new WaitForSeconds(showTime);
        
        // Hide sequence, enable input
        if (instructionText) instructionText.text = "Enter the sequence!";
        ShowInput();
        
        SetInputEnabled(true);
        waitingForInput = true;
    }
    
    IEnumerator RetrySequence()
    {
        // Short delay then show same sequence again
        yield return new WaitForSeconds(1f);
        
        // Reset display
        if (displayText) displayText.color = Color.white;
        input = "";
        
        // Update attempts display
        if (progressText) progressText.text = $"Attempts: {wrongAttempts}/{maxAttempts}";
        
        // Show sequence again
        if (instructionText) instructionText.text = "Remember!";
        if (displayText) displayText.text = answer;
        
        yield return new WaitForSeconds(showTime);
        
        // Hide and enable input
        if (instructionText) instructionText.text = "Try again!";
        ShowInput();
        
        SetInputEnabled(true);
        waitingForInput = true;
    }
    
    IEnumerator NextRoundDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Reset color
        if (displayText) displayText.color = Color.white;
        
        StartCoroutine(PlayRound());
    }
    
    IEnumerator WinSequence()
    {
        if (instructionText) instructionText.text = "YOU WIN!";
        
        yield return new WaitForSeconds(2f);
        
        // Fade to black
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color c = Color.black;
            c.a = 0;
            fadeImage.color = c;
            
            for (float t = 0; t < 1f; t += Time.deltaTime * 2f)
            {
                c.a = t;
                fadeImage.color = c;
                yield return null;
            }
        }
        
        yield return new WaitForSeconds(0.5f);
        
        // Exit scene
        SceneManager.LoadScene(exitScene);
    }
    
    // ========== UI HELPERS ==========
    
    void ShowInput()
    {
        // Show what user typed + underscores for remaining
        if (displayText == null) return;
        
        string s = input;
        for (int i = input.Length; i < answer.Length; i++)
            s += "_";
        displayText.text = s;
    }
    
}
