using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryMenuUI : MonoBehaviour
{
    public Image[] slots;

    private void OnEnable()
    {
        List<ItemData> items = InventorySaveSystem.Load();
        Render(items);
    }

    void Render(List<ItemData> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].sprite = Resources.Load<Sprite>(items[i].iconPath);
                slots[i].color = Color.white;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
