using System;
using UnityEngine;

[Serializable]
public class ItemStack 
{
    public ItemData item;
    public int quantity;

    public ItemStack(ItemData item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
