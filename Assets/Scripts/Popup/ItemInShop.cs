using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInShop : MonoBehaviour
{
    public ItemData item;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Button btnBuy;
    [SerializeField] private int price;
    private void Awake()
    {
        txtPrice.text = $"{price}";
    }
    private void Update()
    {
        if(DataManager.CoinInGame < price)
        {
            btnBuy.enabled = false;
        }   
    }
    
    private void onClickBuy()
    {
        if (DataManager.CoinInGame < price) return;
        else
        {
            DataManager.CoinInGame -= price;
            InventoryManager.Instance.AddItem(item);
        }
    }

    private void OnEnable()
    {
        btnBuy.onClick.AddListener(onClickBuy);
    }

    private void OnDisable()
    {
        btnBuy.onClick.RemoveListener(onClickBuy); 
    }
}
