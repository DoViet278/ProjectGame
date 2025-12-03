using NUnit.Framework.Interfaces;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool picked = HotbarManager.Instance.AddItemToHotbar(item,1);

            if (picked)
            {
                Destroy(gameObject);
                UiInventoryInGame.Instance.Refresh(HotbarManager.Instance.hotbar);
            }
            else
            {
                
            }
        }
    }
}
