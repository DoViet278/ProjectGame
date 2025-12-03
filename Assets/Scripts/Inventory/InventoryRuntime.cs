using System.Collections.Generic;
using UnityEngine;

public class InventoryRuntime : MonoBehaviour
{
    public static InventoryRuntime Instance;

    public int maxSlots = 3;
    public List<ItemStack> items = new List<ItemStack>();

    private void Awake()
    {
        Instance = this;
    }

    public bool AddItem(ItemData item)
    {
        ItemStack stack = items.Find(i => i.item.id == item.id);

        if (stack != null)
        {
            stack.quantity++;
        }
        else
        {
            if (items.Count >= maxSlots-1) return false;

            items.Add(new ItemStack(item, 1));
        }

        UiInventoryInGame.Instance.Refresh(items);
        return true;
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count)
            return;

        ItemStack stack = items[index];

        stack.quantity--;

        if (stack.quantity <= 0)
        {
            items.RemoveAt(index);
        }

        UiInventoryInGame.Instance.Refresh(items);
    }
}
