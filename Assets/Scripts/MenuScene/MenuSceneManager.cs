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
    }

    private void RemoveListeners()
    {
        playBtn.onClick.RemoveListener(onClickPlay);
        inventoryBtn.onClick.RemoveListener(onClickShowInventory);
    }

    private void onClickPlay()
    {
        selectItemPopup.SetActive(true);
    }

    private void onClickShowInventory()
    {
        invetoryUI.SetActive(true);
    }
    private void OnDisable()
    {
        RemoveListeners();
    }





}
