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

        AddListeners();
    }

    private void Update()
    {
        txtName.text = DataManager.PlayerName;
        txtCoin.text = $"{DataManager.CoinInGame}";
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
