using System.Collections;
using UnityEngine;

public class UseItemInGame : MonoBehaviour
{
    private Player player;
    private EntityHealth health;
    private SpriteRenderer sr;

    private void Awake()
    {
        player = GetComponent<Player>();
        health = GetComponent<EntityHealth>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(HotbarManager.Instance.hotbar[0]);
            HotbarManager.Instance.UseItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(HotbarManager.Instance.hotbar[1]);
            HotbarManager.Instance.UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(HotbarManager.Instance.hotbar[2]);
            HotbarManager.Instance.UseItem(2);
        }
    }

    private void UseItem(ItemStack stack)
    {
        if(stack.item.id == "cloak")
        {
            Debug.LogError("use cloak");
            GamePlayController.Instance.EnterToInvisible();
            StartCoroutine(InvisibleRoutine());
        }
        else if (stack.item.id == "health")
        {
            health.currentHp += 10;
            health.UpdateHealthBar();
        }
        else if (stack.item.id == "power_up")
        {
            StartCoroutine(IncreaseSpeed());
        }
    }

    private IEnumerator InvisibleRoutine()
    {
        Color c = sr.color;
        c.a = 0.5f;
        Debug.LogError("alpha: ");
        yield return new WaitForSeconds(3f);
        c.a = 1f;
    }

    private IEnumerator IncreaseSpeed()
    {
        player.moveSpeed++;
        yield return new WaitForSeconds(2f);
        player.moveSpeed--;
    }
}
