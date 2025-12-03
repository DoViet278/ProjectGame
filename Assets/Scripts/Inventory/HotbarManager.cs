using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    public List<ItemStack> hotbar = new List<ItemStack>();
    public int maxSlots = 5; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool AddItemToHotbar(ItemData item, int quantity = 1)
    {
        var stack = hotbar.Find(x => x.item.id == item.id);
        if (stack != null)
        {
            stack.quantity += quantity;
        }
        else
        {
            if (hotbar.Count < maxSlots - 1)
            {
                hotbar.Add(new ItemStack(item, quantity));
                return true;
            }
            else
                return false;
        }
        return true;
    }

    public void UseItem(int slot)
    {
        if (slot < 0 || slot >= hotbar.Count) return;

        var stack = hotbar[slot];
        if (stack != null)
        {
            stack.quantity--;
            if (stack.quantity <= 0)
                hotbar.RemoveAt(slot);
        }
        UiInventoryInGame.Instance.Refresh(hotbar);
    }
}
