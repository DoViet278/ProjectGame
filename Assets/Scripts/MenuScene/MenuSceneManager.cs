using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private GameObject invetoryUI;
    [SerializeField] private GameObject selectItemPopup;
    [SerializeField] private GameObject selectLevelPopup;
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject menuButtonsContainer; // Container chứa 3 nút menu
    
    public static MenuSceneManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        playBtn.onClick.AddListener(onClickPlay);
        inventoryBtn.onClick.AddListener(onClickShowInventory);
        settingBtn.onClick.AddListener(onClickSetting);
    }

    private void RemoveListeners()
    {
        playBtn.onClick.RemoveListener(onClickPlay);
        inventoryBtn.onClick.RemoveListener(onClickShowInventory);
        settingBtn.onClick.RemoveListener(onClickSetting);
    }

    private void onClickPlay()
    {
        if (selectLevelPopup != null)
            selectLevelPopup.SetActive(true);
    }

    private void onClickShowInventory()
    {
        if (invetoryUI != null)
            invetoryUI.SetActive(true);
    }

    private void onClickSetting()
    {
        // Support cả 2 version: settingsPopup (mới) và settings (cũ)
        if (settingsPopup != null) 
        {
            settingsPopup.SetActive(true);
            if (menuButtonsContainer != null) 
                menuButtonsContainer.SetActive(false);
        }
        else if (settings != null)
        {
            settings.SetActive(true);
        }
    }
    
    public void ShowMenuButtons()
    {
        if (menuButtonsContainer != null) 
            menuButtonsContainer.SetActive(true);
    }
    
    private void OnDisable()
    {
        RemoveListeners();
    }
}
