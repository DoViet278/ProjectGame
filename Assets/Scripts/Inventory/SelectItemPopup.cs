using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectItemPopup : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnBack;
    public Transform contentParent; 
    public GameObject itemButtonPrefab;
    public int selectedCount = 0;
    public List<SelectSlotUI> slots;
    public int maxSelect = 3;
    private List<ItemStack> tempInventoryBackup;

    private void OnEnable()
    {
        BackupInventoryTemp();
        Populate();
        ClearAllSelectedSlots();
        selectedCount = 0;
        AddListeners();
    }

    private void AddListeners()
    {
        btnContinue.onClick.AddListener(OnClickPlay);
        btnBack.onClick.AddListener(OnClickBack);
    }

    private void RemoveListeners()
    {
        btnContinue?.onClick.RemoveListener(OnClickPlay);
        btnBack?.onClick.RemoveListener(OnClickBack);   
    }

    private void OnClickPlay()
    {
        foreach (var stack in GetSelectedItems())
        {
            HotbarManager.Instance.AddItemToHotbar(stack.item, 1);
            InventoryManager.Instance.RemoveItem(stack.item, 1);
        }
        InventoryManager.Instance.BackupInventoryBeforeMatch();
        HotbarManager.Instance.hotbar.Clear();
        SceneManager.LoadScene(DataManager.LevelPlaying + 2);
    }

    private void OnClickBack()
    {
        RestoreInventoryTemp();
        ClearAllSelectedSlots();
        selectedCount = 0;

        Populate();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        RemoveListeners();
    }
    public void Populate()
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
                if (selectedCount > maxSelect) return;
                selectedCount++;
                TrySelectItem(stack, ui.qtyText);
            });
        }
    }

    void TrySelectItem(ItemStack stack, TextMeshProUGUI qtyText)
    {
        if (stack.quantity <= 0) return;

        foreach (var slot in slots)
        {
            if (!slot.isUsed)
            {
                slot.SetItem(stack.item); 
                stack.quantity--;
                qtyText.text = stack.quantity.ToString();
                return;
            }
        }
    }

    void ClearAllSelectedSlots()
    {
        foreach (var slot in slots)
        {
            slot.Clear();
        }
    }

    public List<ItemStack> GetSelectedItems()
    {
        List<ItemStack> result = new List<ItemStack>();

        foreach (var slot in slots)
        {
            if (slot.isUsed)
                result.Add(new ItemStack(slot.item, 1)); 
        }

        return result;
    }

    void BackupInventoryTemp()
    {
        tempInventoryBackup = new List<ItemStack>();
        foreach (var stack in InventoryManager.Instance.inventory)
        {
            tempInventoryBackup.Add(new ItemStack(stack.item, stack.quantity));
        }
    }
    void RestoreInventoryTemp()
    {
        InventoryManager.Instance.inventory.Clear();

        foreach (var stack in tempInventoryBackup)
        {
            InventoryManager.Instance.inventory.Add(new ItemStack(stack.item, stack.quantity));
        }
    }
}
