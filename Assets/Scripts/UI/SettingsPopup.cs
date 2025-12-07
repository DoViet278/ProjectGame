using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPopup : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button closeBtn;

    [Header("Visuals")]
    [SerializeField] private Image musicIcon;
    [SerializeField] private Image soundIcon;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    
    [Header("Music Selector")]
    [SerializeField] private TMP_Dropdown musicDropdown;

    private void Start()
    {
        Debug.Log("=== SettingsPopup Start() ===");
        
        // Tắt Raycast Target
        if (musicIcon != null) musicIcon.raycastTarget = false;
        if (soundIcon != null) soundIcon.raycastTarget = false;
        
        // Add listeners
        if (musicBtn != null)
        {
            musicBtn.onClick.RemoveAllListeners();
            musicBtn.onClick.AddListener(OnMusicClick);
            Debug.Log("Added OnMusicClick listener to " + musicBtn.gameObject.name);
        }
        
        if (soundBtn != null)
        {
            soundBtn.onClick.RemoveAllListeners();
            soundBtn.onClick.AddListener(OnSoundClick);
            Debug.Log("Added OnSoundClick listener to " + soundBtn.gameObject.name);
        }
        
        // Add closeBtn listener
        if (closeBtn != null)
        {
            closeBtn.onClick.RemoveAllListeners();
            closeBtn.onClick.AddListener(OnCloseClick);
        }
        
        // Setup music dropdown
        if (musicDropdown != null)
        {
            PopulateMusicDropdown();
            musicDropdown.onValueChanged.AddListener(OnMusicDropdownChanged);
        }
        
        // Tắt Raycast Target của icons để chúng không chặn click vào buttons
    }
    
    private void OnEnable()
    {
        UpdateUI();
        UpdateMusicDropdown(); // Update dropdown to show current track
    }

    private void OnMusicClick()
    {
        Debug.Log("!!! OnMusicClick() CALLED !!!");
        if (AudioManager.Instance != null)
        {
            Debug.Log($"Before toggle: IsMusicOn = {AudioManager.Instance.IsMusicOn}");
            AudioManager.Instance.ToggleMusic();
            Debug.Log($"After toggle: IsMusicOn = {AudioManager.Instance.IsMusicOn}");
            UpdateUI();
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null!");
        }
    }

    private void OnSoundClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToogleSound();
            UpdateUI();
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null!");
        }
    }

    private void OnCloseClick()
    {
        if(MenuSceneManager.Instance != null) 
            MenuSceneManager.Instance.ShowMenuButtons();
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager.Instance is null in UpdateUI!");
            return;
        }
        
        bool isMusicOn = AudioManager.Instance.IsMusicOn;
        if (musicIcon != null)
        {
            if (musicOnSprite != null && musicOffSprite != null)
                musicIcon.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
            
            musicIcon.color = isMusicOn ? Color.white : Color.gray;
        }

        if (soundIcon != null)
        {
            if (soundOnSprite != null && soundOffSprite != null)
                soundIcon.sprite = SoundManager.Instance.IsSoundOn ? soundOnSprite : soundOffSprite;

            soundIcon.color = SoundManager.Instance.IsSoundOn? Color.white : Color.gray;
        }
    }
    
    private void PopulateMusicDropdown()
    {
        if (musicDropdown == null || AudioManager.Instance == null) return;
        
        musicDropdown.ClearOptions();
        string[] musicNames = AudioManager.Instance.GetMusicClipNames();
        musicDropdown.AddOptions(new System.Collections.Generic.List<string>(musicNames));
    }
    
    private void UpdateMusicDropdown()
    {
        if (musicDropdown == null || AudioManager.Instance == null) return;
        
        int currentIndex = AudioManager.Instance.GetCurrentMusicIndex();
        musicDropdown.SetValueWithoutNotify(currentIndex);
    }
    
    private void OnMusicDropdownChanged(int index)
    {
        if (AudioManager.Instance != null)
        {
            Debug.Log($"Dropdown changed to index {index}");
            AudioManager.Instance.PlayMusicByIndex(index);
        }
    }
}
