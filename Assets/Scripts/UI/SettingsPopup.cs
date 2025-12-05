using UnityEngine;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        Debug.Log("SettingsPopup OnEnable() được gọi!");
        Debug.Log($"musicBtn = {(musicBtn != null ? "OK" : "NULL")}");
        Debug.Log($"soundBtn = {(soundBtn != null ? "OK" : "NULL")}");
        Debug.Log($"closeBtn = {(closeBtn != null ? "OK" : "NULL")}");
        
        UpdateUI();
        
        if (musicBtn != null)
        {
            musicBtn.onClick.AddListener(OnMusicClick);
            Debug.Log("Đã thêm listener cho musicBtn");
        }
        
        if (soundBtn != null)
        {
            soundBtn.onClick.AddListener(OnSoundClick);
            Debug.Log("Đã thêm listener cho soundBtn");
        }
        
        if (closeBtn != null)
        {
            closeBtn.onClick.AddListener(OnCloseClick);
            Debug.Log("Đã thêm listener cho closeBtn");
        }
    }
    
    private void Start()
    {
        Debug.Log("SettingsPopup Start() được gọi!");
        
        // Tắt raycastTarget cho tất cả children images của musicBtn và soundBtn
        if (musicBtn != null)
        {
            Debug.Log($"musicBtn GameObject name: {musicBtn.gameObject.name}");
            Debug.Log($"musicBtn có {musicBtn.transform.childCount} child objects");
            
            // Duyệt qua tất cả children và tắt raycastTarget
            for (int i = 0; i < musicBtn.transform.childCount; i++)
            {
                Transform child = musicBtn.transform.GetChild(i);
                Debug.Log($"  Child {i}: {child.name}");
                
                UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                {
                    Debug.Log($"    -> Có Image component, raycastTarget = {img.raycastTarget}");
                    img.raycastTarget = false; // TẮT RAYCAST!
                    Debug.Log($"    -> Đã tắt raycastTarget!");
                }
            }
        }
        
        if (soundBtn != null)
        {
            Debug.Log($"soundBtn GameObject name: {soundBtn.gameObject.name}");
            Debug.Log($"soundBtn có {soundBtn.transform.childCount} child objects");
            
            for (int i = 0; i < soundBtn.transform.childCount; i++)
            {
                Transform child = soundBtn.transform.GetChild(i);
                Debug.Log($"  Child {i}: {child.name}");
                
                UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
                if (img != null)
                {
                    Debug.Log($"    -> Có Image component, raycastTarget = {img.raycastTarget}");
                    img.raycastTarget = false; // TẮT RAYCAST!
                    Debug.Log($"    -> Đã tắt raycastTarget!");
                }
            }
        }
        
        // Tắt Raycast Target của icons để chúng không chặn click vào buttons
        if (musicIcon != null)
        {
            musicIcon.raycastTarget = false;
            Debug.Log("Đã tắt raycastTarget cho musicIcon");
        }
        
        if (soundIcon != null)
        {
            soundIcon.raycastTarget = false;
            Debug.Log("Đã tắt raycastTarget cho soundIcon");
        }
        
        // Test xem button có hoạt động không bằng cách thêm một test listener
        if (musicBtn != null)
        {
            Debug.Log($"musicBtn.interactable = {musicBtn.interactable}");
            Debug.Log($"musicBtn.gameObject.activeInHierarchy = {musicBtn.gameObject.activeInHierarchy}");
            
            // Thêm một test listener đơn giản
            musicBtn.onClick.AddListener(() => {
                Debug.Log("TEST: musicBtn được click!");
            });
        }
        
        if (soundBtn != null)
        {
            soundBtn.onClick.AddListener(() => {
                Debug.Log("TEST: soundBtn được click!");
            });
        }
        
        if (closeBtn != null)
        {
            closeBtn.onClick.AddListener(() => {
                Debug.Log("TEST: closeBtn được click!");
            });
        }
    }

    private void OnDisable()
    {
        musicBtn.onClick.RemoveListener(OnMusicClick);
        soundBtn.onClick.RemoveListener(OnSoundClick);
        closeBtn.onClick.RemoveListener(OnCloseClick);
    }

    private void OnMusicClick()
    {
        Debug.Log("OnMusicClick() được gọi!");
        if (AudioManager.Instance != null)
        {
            Debug.Log($"Trước khi toggle: IsMusicOn = {AudioManager.Instance.IsMusicOn}");
            AudioManager.Instance.ToggleMusic();
            Debug.Log($"Sau khi toggle: IsMusicOn = {AudioManager.Instance.IsMusicOn}");
            UpdateUI();
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null! Make sure AudioManager exists in the scene.");
        }
    }

    private void OnSoundClick()
    {
        Debug.Log("OnSoundClick() được gọi!");
        if (AudioManager.Instance != null)
        {
            Debug.Log($"Trước khi toggle: IsSoundOn = {AudioManager.Instance.IsSoundOn}");
            AudioManager.Instance.ToggleSound();
            Debug.Log($"Sau khi toggle: IsSoundOn = {AudioManager.Instance.IsSoundOn}");
            UpdateUI();
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null! Make sure AudioManager exists in the scene.");
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
        bool isSoundOn = AudioManager.Instance.IsSoundOn;

        if (musicIcon != null)
        {
            if (musicOnSprite != null && musicOffSprite != null)
                musicIcon.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
            
            // Luôn thay đổi màu sắc để dễ nhận biết: Sáng (Bật) - Tối (Tắt)
            musicIcon.color = isMusicOn ? Color.white : Color.gray;
        }

        if (soundIcon != null)
        {
            if (soundOnSprite != null && soundOffSprite != null)
                soundIcon.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
            
            // Luôn thay đổi màu sắc
            soundIcon.color = isSoundOn ? Color.white : Color.gray;
        }
    }
}
