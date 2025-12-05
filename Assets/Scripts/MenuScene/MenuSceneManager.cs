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
    [SerializeField] private GameObject settingsPopup;
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

    private void Update()
    {
       // txtCoin.text = $"{DataManager.CoinInGame}";
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
        selectItemPopup.SetActive(true);
    }

    private void onClickShowInventory()
    {
        invetoryUI.SetActive(true);
    }

    private void onClickSetting()
    {
        if(settingsPopup != null) 
        {
            settingsPopup.SetActive(true);
            if(menuButtonsContainer != null) menuButtonsContainer.SetActive(false);
        }
    }
    
    public void ShowMenuButtons()
    {
        if(menuButtonsContainer != null) menuButtonsContainer.SetActive(true);
    }
    
    private void OnDisable()
    {
        RemoveListeners();
    }





}
