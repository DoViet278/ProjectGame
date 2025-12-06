using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelectLevel : MonoBehaviour
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnShop;
    [SerializeField] private Button btnInventory;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private TextMeshProUGUI txtName;

    [SerializeField] private GameObject popupShop;
    [SerializeField] private GameObject popupInventory;
    private void OnEnable()
    {
        AddListeners();
    }

    private void Update()
    {
        txtCoin.text = $"{DataManager.CoinInGame}";
        txtName.text = DataManager.PlayerName;
    }

    private void AddListeners()
    {
        btnShop.onClick.AddListener(onClickShop);
        btnHome.onClick.AddListener(onClickHome);
        btnInventory.onClick.AddListener(onClickInventory);
    }

    private void RemoveListeners()
    {
        btnInventory.onClick.RemoveListener(onClickInventory);
        btnShop.onClick.RemoveListener(onClickShop);
        btnHome.onClick.RemoveListener(onClickHome);
    }

    private void onClickHome()
    {
        gameObject.SetActive(false);
    }

    private void onClickShop()
    {
        popupShop.SetActive(true);
    }

    private void onClickInventory()
    {
        popupInventory.SetActive(true);
    }

    private void OnDisable()
    {
        RemoveListeners();
        DataManager.IsFirstPlayTime = false;
    }
}
