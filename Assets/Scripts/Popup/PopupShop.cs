using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupShop : MonoBehaviour
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnInventory;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private GameObject popupInventory;

    private void OnEnable()
    {
        txtCoin.text = $"{DataManager.CoinInGame}";
        txtName.text = DataManager.PlayerName;

        AddListeners();
    }

    private void AddListeners()
    {
        btnHome.onClick.AddListener(OnClickHome);
        btnInventory.onClick.AddListener(onClickInventory);  
    }

    private void OnClickHome()
    {
        gameObject.SetActive(false);
    }

    private void onClickInventory()
    {
        popupInventory.SetActive(true);
    }

    private void RemoveListeners()
    {
        btnHome.onClick.RemoveListener(OnClickHome);
        btnInventory.onClick.RemoveListener(onClickInventory);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
}
