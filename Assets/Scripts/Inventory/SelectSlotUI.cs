using UnityEngine;
using UnityEngine.UI;

public class SelectSlotUI : MonoBehaviour
{
    public Image icon;
    public Button removeBtn;

    [HideInInspector] public ItemData item;
    [HideInInspector] public bool isUsed = false;
    private SelectItemPopup selectItemPopup;

    private void Awake()
    {
        removeBtn.onClick.AddListener(Remove);
        selectItemPopup = gameObject.transform.parent.GetComponentInParent<SelectItemPopup>();
    }

    public void SetItem(ItemData newItem)
    {
        item = newItem;
        isUsed = true;

        icon.sprite = Resources.Load<Sprite>(item.iconPath);
        icon.enabled = true;
        removeBtn.gameObject.SetActive(true);
    }

    public void Clear()
    {
        item = null;
        isUsed = false;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.gameObject.SetActive(false);
    }

    void Remove()
    {
        if (!isUsed) return;

        InventoryManager.Instance.AddItem(item, 1);

        selectItemPopup.Populate();
        selectItemPopup.selectedCount--;
        Clear();
    }
}
