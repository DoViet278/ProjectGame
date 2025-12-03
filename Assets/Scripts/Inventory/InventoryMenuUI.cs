using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryMenuUI : MonoBehaviour
{
    public Image[] slots;
    public TMP_Text[] qtyTexts;
    private void OnEnable()
    {
        Render(InventoryManager.Instance.inventory);
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
}
