using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FlashMemoryGame : MonoBehaviour
{
    [Header("=== TEXT DISPLAYS ===")]
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    
    [Header("=== BUTTONS ===")]
    public Button[] numButtons = new Button[10];
    public Button delButton;
    public Button skipButton;
    public Button okButton;
    public Button startButton;
    
    [Header("=== SETTINGS ===")]
    public float showTime = 2f;
    public int maxAttempts = 3;
    public string exitScene = "Menu";
    
    [Header("=== FADE (Optional) ===")]
    public Image fadeImage;
    
    string answer = "";
    string input = "";
    int score = 0;
    int wrongAttempts = 0;
    bool waitingForInput = false;
    
    void Start()
    {
        if (displayText) displayText.text = "";
        if (instructionText) instructionText.text = "Press START!";
        // Initialize with default values so they are visible
        if (progressText) progressText.text = "0/" + maxAttempts;
        if (scoreText) scoreText.text = "Score: 0";
        
        SetupButtons();
        SetInputEnabled(false);
    }
    
    void SetupButtons()
    {
        for (int i = 0; i < numButtons.Length; i++)
        {
            if (numButtons[i] != null)
            {
                int digit = i;
                numButtons[i].onClick.RemoveAllListeners();
                numButtons[i].onClick.AddListener(() => OnNumPress(digit));
            }
        }
        
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
    
    public void OnStartPress()
    {
        if (startButton) startButton.gameObject.SetActive(false);
        
        wrongAttempts = 0;
        score = 0;
        
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
        if (input.Length != answer.Length) return;
        
        waitingForInput = false;
        SetInputEnabled(false);
        
        if (input == answer)
        {
            score += 100;
            
            if (instructionText) instructionText.text = "CORRECT! YOU WIN!";
            if (displayText) displayText.color = Color.green;
            if (scoreText) scoreText.text = "Score: " + score;
            
            StartCoroutine(WinSequence());
        }
        else
        {
            wrongAttempts++;
            
            if (wrongAttempts >= maxAttempts)
            {
                if (instructionText) instructionText.text = $"Wrong {maxAttempts}x! New sequence...";
                if (displayText) displayText.color = Color.red;
                
                wrongAttempts = 0;
                StartCoroutine(NextRoundDelay(1.5f));
            }
            else
            {
                int remaining = maxAttempts - wrongAttempts;
                if (instructionText) instructionText.text = $"Wrong! {remaining} tries left";
                if (displayText) displayText.color = Color.red;
                
                StartCoroutine(RetrySequence());
            }
        }
    }
    
    IEnumerator PlayRound()
    {
        waitingForInput = false;
        input = "";
        wrongAttempts = 0;
        
        if (displayText) displayText.color = Color.white;
        
        int length = Random.Range(6, 9);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
            sb.Append(Random.Range(0, 10));
        answer = sb.ToString();
        
        if (instructionText) instructionText.text = "Remember!";
        if (displayText) displayText.text = answer;
        
        yield return new WaitForSeconds(showTime);
        
        if (instructionText) instructionText.text = "Enter the sequence!";
        ShowInput();
        
        SetInputEnabled(true);
        waitingForInput = true;
    }
    
    IEnumerator RetrySequence()
    {
        yield return new WaitForSeconds(1f);
        
        // Reset display but DON'T show sequence again
        if (displayText) displayText.color = Color.white;
        input = "";
        
        // Update attempts display (Short format)
        if (progressText) progressText.text = $"{wrongAttempts}/{maxAttempts}";
        
        // Show input area (without showing sequence!)
        if (instructionText) instructionText.text = "Try again!";
        ShowInput();
        
        SetInputEnabled(true);
        waitingForInput = true;
    }
    
    IEnumerator NextRoundDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (displayText) displayText.color = Color.white;
        
        StartCoroutine(PlayRound());
    }
    
    IEnumerator WinSequence()
    {
        if (instructionText) instructionText.text = "YOU WIN!";
        
        yield return new WaitForSeconds(2f);
        
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
        
        SceneManager.LoadScene(exitScene);
    }
    
    void ShowInput()
    {
        if (displayText == null) return;
        
        string s = input;
        for (int i = input.Length; i < answer.Length; i++)
            s += "_";
        displayText.text = s;
    }
}
