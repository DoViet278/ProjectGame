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
        btnBack.onClick.AddListener(OnClickBack);
    }

    private void RemoveListeners()
    {
        btnContinue?.onClick.RemoveListener(OnClickPlay);
        btnBack?.onClick.RemoveListener(OnClickBack);   
    }

    private void OnClickPlay()
    {
        InventoryManager.Instance.BackupInventoryBeforeMatch();
        SceneManager.LoadScene(DataManager.LevelPlaying + 2);
    }

    private void OnClickBack()
    {
        gameObject.SetActive(false);
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
