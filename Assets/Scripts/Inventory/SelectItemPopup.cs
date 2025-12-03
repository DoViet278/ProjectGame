using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectItemPopup : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    public Transform contentParent; 
    public GameObject itemButtonPrefab;
    private int selectedCount = 0;
    private int maxSelect = 5;

    private void OnEnable()
    {
        Populate();
        AddListeners();
    }

    private void AddListeners()
    {
        btnContinue.onClick.AddListener(OnClickPlay);
    }

    private void RemoveListeners()
    {
        btnContinue?.onClick.RemoveListener(OnClickPlay);   
    }

    private void OnClickPlay()
    {
        InventoryManager.Instance.BackupInventoryBeforeMatch();
        SceneManager.LoadScene("MainScene");
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
    void Populate()
    {
        foreach (Transform child in contentParent) Destroy(child.gameObject);

        foreach (var stack in InventoryManager.Instance.inventory)
        {
            GameObject btnObj = Instantiate(itemButtonPrefab, contentParent);
            ItemButtonSelect ui = btnObj.GetComponent<ItemButtonSelect>();

            ui.qtyText.text = stack.quantity.ToString();
            ui.iconImage.sprite = Resources.Load<Sprite>(stack.item.iconPath);

            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                if (selectedCount >= maxSelect) return;
                HotbarManager.Instance.AddItemToHotbar(stack.item, 1);
                InventoryManager.Instance.RemoveItem(stack.item, 1);
                selectedCount++;
                btn.interactable = false;
            });
        }
    }
}
