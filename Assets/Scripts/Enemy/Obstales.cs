using UnityEngine;

public class Obstales : MonoBehaviour
{
    private EntityHealth entityHealth;

    private void Start()
    {
        entityHealth = GetComponent<EntityHealth>();
    }

    private void Update()
    {
        if (entityHealth.isDead)
        {
           Destroy(gameObject);
        }
    }
}
