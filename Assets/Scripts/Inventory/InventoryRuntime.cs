using System.Collections.Generic;
using UnityEngine;

public class InventoryRuntime : MonoBehaviour
{
    public static InventoryRuntime Instance;

    public int maxSlots = 5;
    public List<ItemData> items = new List<ItemData>();

    private void Awake()
    {
        Instance = this;
    }

    public bool AddItem(ItemData item)
    {
        if (items.Count >= maxSlots)
            return false;

        items.Add(item);
        UiInventoryInGame.Instance.Refresh(items);
        return true;
    }
}
