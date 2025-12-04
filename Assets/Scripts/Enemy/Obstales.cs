using UnityEngine;

public class Obstales : MonoBehaviour
{
    private EntityHealth entityHealth;
    private Animator animator;
    private BoxCollider2D box;
    private void Start()
    {
        entityHealth = GetComponent<EntityHealth>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (entityHealth.isDead)
        {
            animator.SetBool("break", true);
            box.isTrigger = true;
        }
    }
}
