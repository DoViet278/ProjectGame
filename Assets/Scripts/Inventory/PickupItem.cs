using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryRuntime.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
