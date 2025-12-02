using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryInGame : MonoBehaviour
{
    public static UiInventoryInGame Instance;

    public Image[] slots;

    private void Awake()
    {
        Instance = this;
    }

    public void Refresh(List<ItemData> items)
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
