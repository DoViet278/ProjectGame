using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryMenuUI : MonoBehaviour
{
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnShop;
    [SerializeField] private GameObject popupShop;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private TextMeshProUGUI txtName;

    public Image[] slots;
    public TMP_Text[] qtyTexts;
    private void OnEnable()
    {
        Render(InventoryManager.Instance.inventory);
        AddListeners();
    }

    private void Update()
    {
        txtCoin.text = $"{DataManager.CoinInGame}";
        txtName.text = DataManager.PlayerName;
    }
    void Render(List<ItemStack> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].sprite = Resources.Load<Sprite>(items[i].item.iconPath);
                slots[i].color = Color.white;
                qtyTexts[i].text = items[i].quantity.ToString();
            }
            else
            {
                slots[i].sprite = null;
                slots[i].color = new Color(1, 1, 1, 0);
                qtyTexts[i].text = "";
            }
        }
    }

    private void AddListeners()
    {
        btnExit.onClick.AddListener(OnClickExit);
        btnHome.onClick.AddListener(OnClickHome);   
        btnShop.onClick.AddListener(OnClickShop);
    }

    private void RemoveListeners()
    {
        btnExit.onClick.RemoveListener(OnClickExit);
        btnHome.onClick.RemoveListener(OnClickHome);
        btnShop.onClick.RemoveListener(OnClickShop);

    }
    private void OnDisable()
    {
        RemoveListeners();
    }
    private void OnClickExit()
    {
        gameObject.SetActive(false);    
    }

    private void OnClickShop()
    {
        popupShop.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnClickHome()
    {
        gameObject.SetActive(false);
    }
}
