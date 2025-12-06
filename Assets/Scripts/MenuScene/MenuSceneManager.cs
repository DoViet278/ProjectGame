using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button tutorialBtn;
    [SerializeField] private GameObject invetoryUI;
    [SerializeField] private GameObject selectItemPopup;
    [SerializeField] private GameObject selectLevelPopup;
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject menuButtonsContainer; // Container chứa 3 nút menu
    [SerializeField] private GameObject popupPlayerName;
    
    public static MenuSceneManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        if (DataManager.IsFirstPlayTime)
        {
            popupPlayerName.SetActive(true);
        }
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        playBtn.onClick.AddListener(onClickPlay);
        settingBtn.onClick.AddListener(onClickSetting);
        tutorialBtn.onClick.AddListener(onClickTutorial);
    }

    private void RemoveListeners()
    {
        playBtn.onClick.RemoveListener(onClickPlay);
        settingBtn.onClick.RemoveListener(onClickSetting);
        tutorialBtn.onClick.RemoveListener(onClickTutorial);
    }

    private void onClickPlay()
    {
        if (selectLevelPopup != null)
            selectLevelPopup.SetActive(true);
    }
    private void onClickTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    private void onClickSetting()
    {
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
