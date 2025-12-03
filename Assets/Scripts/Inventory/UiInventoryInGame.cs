using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryInGame : MonoBehaviour
{
    public static UiInventoryInGame Instance;

    public Image[] slots;
    public TMP_Text[] qtyTexts;

    private void Awake()
    {
        Instance = this;
        Refresh(HotbarManager.Instance.hotbar);
    }

    public void Refresh(List<ItemStack> items)
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
}
